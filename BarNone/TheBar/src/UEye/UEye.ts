import { IUtils } from 'UEye/UEye.config';
import Screen from 'UEye/Screen/Screen';
import CallableEvent, { ICallableEvent } from 'UEye/CallableEvent/CallableEvent';

declare global {
	const Utils: IUtils
	interface Array<T> {
		first(): T | undefined;
		first(predicate: (this: void, value: T, index: number, obj: Array<T>) => boolean): T | undefined;
    first(predicate: (this: void, value: T, index: number, obj: Array<T>) => boolean, thisArg: undefined): T | undefined;
	}
}

/**
 * Screen mount definition
 */
class ScreenMountPoint {
	/**
	 * Screen parent
	 */
	public parent: Screen<any>;

	/**
	 * Screen element
	 */
	public element: HTMLElement;

	/**
	 * Child screen.
	 * Undefined if no child attached.
	 */
	private _child?: Screen<any>;

	/**
	 * Initialize screen mount point
	 * @param element - html element
	 * @param parent - screen parent
	 */
	public constructor(element: HTMLElement, parent: Screen<any>) {
		this.parent = parent;
		this.element = element
	}

	/**
	 * Get child
	 */
	public get child(): Screen<any> | undefined {
		return this._child;
	}

	/**
	 * Set child
	 */
	public set child(value: Screen<any> | undefined) {
		this._child = value;
	}

	/**
	 * Is the screen used.
	 * True if child is not undefined, false otherwise.
	 */
	public get isUsed(): boolean {
		return (this._child !== undefined);
	}
}

export default class UEye {
	/**
	 * Base element for application.
	 */
	private static _base: HTMLElement;

	/**
	 * Screen mounting points.
	 */
	private static _screenMountPointList: ScreenMountPoint[];

	/**
	 * Pushed screen objects.
	 */
	private static _screenList: Screen<any>[];

	/**
	 * Back events for back click
	 */
	private static readonly _onBack = new CallableEvent();

	/**
	 * Start the application.
	 */
	public static start(base: HTMLElement): void {
		// var base = document.getElementById("app");
		// if (base !== null) {
		UEye._base = base;
		// }
		UEye._screenList = [];
		UEye._screenMountPointList = [];

		window.onpopstate = (event) => {
			if (UEye.currentScreen !== undefined) {
				history.pushState(null, document.title, location.href);
				this._onBack.trigger();
			}
		};

		window.addEventListener('resize', () => {
			this._screenList.forEach(screen =>
				screen.inflaterData.componentList.forEach(component => component.bindView(screen.view)));
		}, true);
	}

	/**
	 * Expose on back events.
	 */
	public static get onBack(): ICallableEvent {
		return UEye._onBack.expose();
	}

	/**
	 * Push Screen
	 * @param ScreenType - screen builder
	 * @param data - data for screen.
	 */
	public static push(ScreenType: { new(): Screen<any> }, data?: any): Screen<any> {
		var screen = new ScreenType();

		// Handle config.
		if (screen.config.addScreenToHistory) {
			history.pushState(null, document.title, location.href);
		}

		// Find the parent for the next screen.
		var parent = this._base;

		if (this._screenMountPointList.length > 0) {
			var mountPoint = this._screenMountPointList.find(mountPoint => {
				return (mountPoint.isUsed === false);
			});
			if (mountPoint === undefined) {
				console.warn("Could not find screen mounting point. Defaulting to base.");
			} else {
				parent = mountPoint.element;
				mountPoint.child = screen;
			}
		}

		// If full screen override parent.
		if (screen.config.fullScreen) {
			parent = this._base;
		}

		// Inflate screen.
		var inflationData = screen.create(parent);

		// Map the points on a screen where a new screen can be added.
		this._screenMountPointList = this._screenMountPointList
			.concat(inflationData.mountingPoints.map(element => {
				return new ScreenMountPoint(element, screen);
			}));

		UEye._screenList.push(screen);
		screen.onShow(data);

		return screen;
	}

	/**
	 * Remove last screen.
	 */
	public static pop(): void {
		var lastScreen = UEye._screenList.pop();
		if (lastScreen === undefined) return;

		this._screenMountPointList = this._screenMountPointList
			.filter(mount => {
				return (mount.parent !== lastScreen)
			})
			.map(mount => {
				if (mount.child === lastScreen) {
					mount.child = undefined;
					return mount;
				}
				return mount;
			});

		lastScreen.destroy();
	}

	/**
	 * Remove screens up to target screen.
	 * Does not remove target screen.
	 * @param target - screen to find.
	 */
	public static popTo(target: Screen<any>): void {
		while (UEye._screenList.length > 0) {
			var lastIndex = UEye._screenList.length - 1;
			if (UEye._screenList[lastIndex] === target) break;

			UEye.pop();
		}
	}

	public static popAll(): void {
		while (UEye._screenList.length > 0) {
			var lastIndex = UEye._screenList.length - 1;

			UEye.pop();
		}
	}

	/**
	 * Gets current screen.
	 */
	public static currentScreen(): Screen<any> | undefined {
		if (UEye._screenList.length > 0) {
			return UEye._screenList[UEye._screenList.length - 1];
		}
		return undefined;
	}

	public static get screens(): Screen<any>[] {
		return UEye._screenList;
	}
}
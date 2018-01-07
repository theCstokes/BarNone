import { IUtils } from 'UEye/UEye.config';
import { AppScreen } from "UEye/Screen/AppScreen";
import Core from "UEye/Elements/Core/Core";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import { IDataEvent } from "UEye/Core/DataEvent/IDataEvent";
import Inflater from 'UEye/Elements/Core/Inflater/Inflater';

declare global {
	const Utils: IUtils
}

class MountElement {
	public element: HTMLElement;
	public isUsed: boolean;
	public parent: AppScreen;
	public child?: AppScreen;

	public constructor(element: HTMLElement, isUsed: boolean, parent: AppScreen, child?: AppScreen) {
		this.element = element;
		this.isUsed = isUsed;
		this.parent = parent;
		this.child = child;
	}
}

export default class UEye {
	private static readonly _onBack = new DataEvent<void>();

	private static _base: HTMLElement;
	private static _screens: AppScreen[];
	private static _screenMounts: MountElement[];

	public static start(): void {
		var base = document.getElementById("app");
		if (base !== null) {
			UEye._base = base;
		}
		UEye._screens = [];
		UEye._screenMounts = [];

		window.onpopstate = (event) => {
			if (UEye.currentScreen !== undefined) {
				history.pushState(33, document.title, location.href);
				this._onBack.trigger();
			}
		};
	}

	public static get onBack(): IDataEvent<void> {
		return UEye._onBack.expose();
	}

	public static get base(): HTMLElement {
		return this._base;
	}

	public static push(AppScreenType: { new(): AppScreen }, data?: any): AppScreen {
		var screen = new AppScreenType();
		if (screen.isTrackScreen) {
			history.pushState(null, document.title, location.href);
		}

		// Find the parent for the next screen.
		var parent = this._base;
		if (this._screenMounts.length > 0) {
			var screenMount = this._screenMounts.find(screenMount => {
				return (screenMount.isUsed === false);
			});
			if (screenMount === undefined) {
				console.warn("Could not find screen mounting point. Defaulting to base.");
			} else {
				parent = screenMount.element;
				screenMount.isUsed = true;
				screenMount.child = screen;
			}
		}

		// Inflate the screen.
		var results = Inflater.execute(parent, screen.content);
		screen.components = results.components;

		this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
			return new MountElement(element, false, screen);
		}));

		for (var key in results.componentMap) {
			if (!results.componentMap.hasOwnProperty(key)) continue;
			screen.view[key] = results.componentMap[key];
		}

		UEye._screens.push(screen);

		// screen.trigger("onShow", data);
		screen.show(data);
		return screen;
	}

	public static pop(): void {
		var lastScreen = UEye._screens.pop();
		if (lastScreen === undefined) return;

		this._screenMounts = this._screenMounts
			.filter(mount => {
				return (mount.parent !== lastScreen)
			})
			.map(mount => {
				if (mount.child === lastScreen) {
					mount.isUsed = false;
					mount.child = undefined;
					return mount;
				}
				return mount;
			});

		lastScreen.destroy();
	}

	public static popTo(target: AppScreen): void {
		while (UEye._screens.length > 0) {
			var lastIndex = UEye._screens.length - 1;
			if (UEye._screens[lastIndex] === target) break;

			UEye.pop();
		}
	}

	public static currentScreen(): AppScreen | undefined {
		if (UEye._screens.length > 0) {
			return UEye._screens[UEye._screens.length - 1];
		}
		return undefined;
	}
}
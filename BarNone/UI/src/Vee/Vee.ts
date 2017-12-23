import { IUtils } from 'Vee/Vee.config';
import { AppScreen } from "Vee/Screen/AppScreen";
import Core from "Vee/Elements/Core/Core";
import DataEvent from "Vee/Core/DataEvent/DataEvent";
import { IDataEvent } from "Vee/Core/DataEvent/IDataEvent";

declare global {
	const Utils: IUtils
}

// class ScreenMountPoint {
// 	public element: HTMLElement;
// 	public isUsed: boolean;
// 	public screen?: AppScreen;
// }

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

export default class Vee {
	private static readonly _onBack = new DataEvent<void>();

	private static _base: HTMLElement;
	private static _screens: AppScreen[];
	private static _screenMounts: MountElement[];

	public static start(): void {
		var base = document.getElementById("app");
		if (base !== null) {
			Vee._base = base;
		}
		Vee._screens = [];
		Vee._screenMounts = [];

		window.onpopstate = (event) => {
			// console.warn(event);
			if (Vee.currentScreen !== undefined) {
				history.pushState(33, document.title, location.href);
				this._onBack.trigger();
				// for (var i = Vee._screens.length - 1; i > 0; i--) {
				// 	if (!Vee._screens[i].isTrackScreen) {
				// 		this._onBack.trigger();
				// 		Vee.pop();
				// 	} else {
				// 		this._onBack.trigger();
				// 		Vee.pop();
				// 		break;
				// 	}
				// }
			}
		};
	}

	public static get onBack(): IDataEvent<void> {
		return Vee._onBack.expose();
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
		var results = Core.inflate(parent, screen.content);
		screen.components = results.components;

		this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
			return new MountElement(element, false, screen);
		}));

		for (var key in results.componentMap) {
			if (!results.componentMap.hasOwnProperty(key)) continue;
			screen.view[key] = results.componentMap[key];
		}

		Vee._screens.push(screen);

		// screen.trigger("onShow", data);
		screen.show(data);
		return screen;
	}

	public static pop(): void {
		var lastScreen = Vee._screens.pop();
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
		while (Vee._screens.length > 0) {
			var lastIndex = Vee._screens.length - 1;
			if (Vee._screens[lastIndex] === target) break;

			Vee.pop();
		}
	}

	public static currentScreen(): AppScreen | undefined {
		if (Vee._screens.length > 0) {
			return Vee._screens[Vee._screens.length - 1];
		}
		return undefined;
	}
}
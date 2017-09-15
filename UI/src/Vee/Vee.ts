import { IUtils } from 'Vee/Vee.config';
import AppScreen from "Vee/Screen/AppScreen";
import Core from "Vee/Elements/Core/Core";

declare global {
	const Utils: IUtils
}

class ScreenMountPoint {
	public element: HTMLElement;
	public isUsed: boolean;
	public screen: AppScreen;
}

export default class Vee {
	private static _base: HTMLElement;
	private static _screens: AppScreen[];
	private static _screenMounts: ScreenMountPoint[];

	public static start(): void {
		var base = document.getElementById("app");
		if (base !== null) {
			Vee._base = base;
		}
		Vee._screens = [];
		Vee._screenMounts = [];
	}

	public static get base(): HTMLElement {
		return this._base;
	}

	public static async push(AppScreenType: { new(): AppScreen }): Promise<void> {
		var screen = new AppScreenType();

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
			}
		}

		var results = await Core.inflate(parent, screen.screenContent);

		this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
			var screenMount = new ScreenMountPoint();
			screenMount.element = element;
			screenMount.isUsed = false;
			screenMount.screen = screen;
			return screenMount;
		}));

		for(var key in results.map) {
			if (!results.map.hasOwnProperty(key)) continue;
			screen.view[key] = results.map[key];
		}
		screen.screenControl = results.map.screenControl;

		Vee._screens.push(screen);

		screen.trigger("onShow");
	}

	public static pop(): void {
		var lastScreen = Vee._screens.pop();
		if (lastScreen === undefined) return;
		this._screenMounts = this._screenMounts.filter((mount) => {
			return (mount.screen !== lastScreen);
		});
		lastScreen.screenControl.destroy();
	}
}
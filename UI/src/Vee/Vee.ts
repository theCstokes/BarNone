import { IUtils } from 'Vee/Vee.config';
import AppScreen from "Vee/Screen/AppScreen";
import Core from "Vee/Elements/Core/Core";

declare global {
	const Utils: IUtils
}

class ScreenMountPoint {
	public element: HTMLElement;
	public isUsed: boolean;
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

		var results = await Core.inflate(parent, screen.view.content);

		this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
			var screenMount = new ScreenMountPoint();
			screenMount.element = element;
			screenMount.isUsed = false;
			return screenMount;
		}));

		Vee._screens.push(screen);

		if ("onShow" in screen) {
			(screen as any).onShow();
		}
	}
}
import { IUtils } from 'UEye/UEye.config';
import Screen from 'UEye/Screen/Screen';
import DataEvent, { IDataEvent } from 'UEye/DataEvent/DataEvent';
// import { AppScreen } from "UEye/Screen/AppScreen";
// import Core from "UEye/Elements/Core/Core";
// import DataEvent from "UEye/Core/DataEvent/DataEvent";
// import { IDataEvent } from "UEye/Core/DataEvent/IDataEvent";
// import Inflater from 'UEye/Elements/Core/Inflater/Inflater';

declare global {
	const Utils: IUtils
}

// class MountElement {
// 	public element: HTMLElement;
// 	public isUsed: boolean;
// 	public parent: AppScreen;
// 	public child?: AppScreen;

// 	public constructor(element: HTMLElement, isUsed: boolean, parent: AppScreen, child?: AppScreen) {
// 		this.element = element;
// 		this.isUsed = isUsed;
// 		this.parent = parent;
// 		this.child = child;
// 	}
// }

class ScreenMountPoint {
    public parent: Screen<any>;
    public element: HTMLElement;

    private _child?: Screen<any>;

    public constructor(element: HTMLElement, parent: Screen<any>) {
        this.parent = parent;
        this.element = element
    }

    public get child(): Screen<any> | undefined {
        return this._child;
    }
    public set child(value: Screen<any> | undefined) {
        this._child = value;
    }

    public get isUsed(): boolean {
        return (this._child !== undefined);
    }
}

export default class UEye {
    private static _base: HTMLElement;
    private static _screenMountPointList: ScreenMountPoint[];
    private static _screenList: Screen<any>[];

    private static readonly _onBack = new DataEvent();

	public static start(): void {
		var base = document.getElementById("app");
		if (base !== null) {
			UEye._base = base;
		}
		UEye._screenList = [];
		UEye._screenMountPointList = [];

		window.onpopstate = (event) => {
			if (UEye.currentScreen !== undefined) {
				history.pushState(null, document.title, location.href);
				this._onBack.trigger();
			}
		};
	}

	public static get onBack(): IDataEvent {
		return UEye._onBack.expose();
	}

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
        
        // Inflate screen.
        var inflationData = screen.create(parent);

        // Map the points on a screen where a new screen can be added.
        this._screenMountPointList = this._screenMountPointList.concat(inflationData.mountingPoints.map(element => {
			return new ScreenMountPoint(element, screen);
		}));

		// // Inflate the screen.
		// var results = Inflater.execute(parent, screen.content);
		// screen.components = results.components;

		// this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
		// 	return new MountElement(element, false, screen);
		// }));

		// for (var key in results.componentMap) {
		// 	if (!results.componentMap.hasOwnProperty(key)) continue;
		// 	screen.view[key] = results.componentMap[key];
		// }

		UEye._screenList.push(screen);
		screen.onShow();

		// screen.trigger("onShow", data);
		// screen.show(data);
		return screen;
	}

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

	public static popTo(target: Screen<any>): void {
		while (UEye._screenList.length > 0) {
			var lastIndex = UEye._screenList.length - 1;
			if (UEye._screenList[lastIndex] === target) break;

			UEye.pop();
		}
	}

	public static currentScreen(): Screen<any> | undefined {
		if (UEye._screenList.length > 0) {
			return UEye._screenList[UEye._screenList.length - 1];
		}
		return undefined;
	}
}
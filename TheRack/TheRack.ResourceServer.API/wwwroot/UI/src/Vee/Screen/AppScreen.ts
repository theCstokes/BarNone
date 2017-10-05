import { View } from "Vee/View/View";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
import { IScreen } from "Vee/Elements/Core/IScreen";
import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";

export abstract class AppScreen implements IScreen {
	private _view: View;
	private _eventBinds: { [key: string]: any };
	private _screenControl: IScreen;
	private _isTrackScreen: boolean;
	private _components: BaseElement[];

	public constructor(viewType: { new(): View }, isTrackScreen: boolean = false) {
		// this._viewType = viewType;
		this._view = new viewType();
		this._eventBinds = {};
		this._isTrackScreen = isTrackScreen;
	}

	public get view(): View {
		return this._view;
	}

	public abstract get content(): any[];
	// {
	// 	return [
	// 		{
	// 			// id: "screenControl",
	// 			instance: ControlTypes.Screen,
	// 			content: this.view.content,
	// 			// bottomDock: [
	// 			// 	{
	// 			// 		instance: ControlTypes.Button,
	// 			// 		text: "Test"
	// 			// 	}
	// 			// ]
	// 		}
	// 	]
	// }

	public get isTrackScreen(): boolean {
		return this._isTrackScreen;
	}

	// public get screenControl(): IScreen {
	// 	return this.view.screenControl;
	// }

	public bind(name: string, callback: any): void {
		if (this._eventBinds[name] === undefined) {
			this._eventBinds[name] = [];
		}
		this._eventBinds[name].push(callback);
	}

	public trigger(name: string, ...data: any[]): void {
		if (name in this) {
			(this as any)[name].apply(this, data);
		}
		if (this._eventBinds[name] !== undefined) {
			this._eventBinds[name].forEach((callback: any) => {
				callback.apply(this, data);
			});
		}
	}

	public get components(): BaseElement[] {
		return this._components;
	}
	public set components(value: BaseElement[]) {
		this._components = value;
	}

	public destroy(): void {
		if (this._components.length == 0) return;
		this._components.forEach(component => component.destroy());
	}

	// public abstract get screenConfig(): any;
}
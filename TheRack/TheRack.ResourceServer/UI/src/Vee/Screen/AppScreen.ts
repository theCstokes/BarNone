import { View } from "Vee/View/View";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
import { IScreen } from "Vee/Elements/Core/IScreen";

export default class AppScreen {
	private _view: View;
	private _eventBinds: { [key: string]: any };
	private _screenControl: IScreen;

	public constructor(viewType: { new(): View }) {
		// this._viewType = viewType;
		this._view = new viewType();
		this._eventBinds = {};
	}

	public get view(): View {
		return this._view;
	}

	public get screenContent(): any[] {
		return [
			{
				id: "screenControl",
				instance: ControlTypes.Screen,
				content: this.view.content
			}
		]
	}

	public get screenControl(): IScreen {
		return this._screenControl;
	}
	public set screenControl(value: IScreen) {
		this._screenControl = value;
	}

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
}
import { View } from "Vee/View/View";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
import { IScreen } from "Vee/Elements/Core/IScreen";
import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";
import DataEvent from "Vee/Core/DataEvent/DataEvent";
import { IDataEvent } from "Vee/Core/DataEvent/IDataEvent";

export abstract class AppScreen implements IScreen {
	private _view: View;
	private _eventBinds: { [key: string]: any };
	private _screenControl: IScreen;
	private _isTrackScreen: boolean;
	private _components: BaseElement[];
	private _showEvent: DataEvent<any>;
	private _renderEvent: DataEvent<any>;

	public constructor(viewType: { new(): View }, isTrackScreen: boolean = false) {
		this._view = new viewType();
		this._eventBinds = {};
		this._isTrackScreen = isTrackScreen;
		this._showEvent = new DataEvent<any>();
		this._renderEvent = new DataEvent<any>();
	}

	public get view(): View {
		return this._view;
	}

	public abstract get content(): any[];

	public get isTrackScreen(): boolean {
		return this._isTrackScreen;
	}

	public get components(): BaseElement[] {
		return this._components;
	}
	public set components(value: BaseElement[]) {
		this._components = value;
	}

	public get showEvent(): IDataEvent<void> {
		return this._showEvent.expose();
	}

	public get renderEvent(): IDataEvent<void> {
		return this._renderEvent.expose();
	}

	public show(data: any): void {
		this.onShow(data);
		this._showEvent.trigger(data);
	}

	public render(data: any): void {
		this._renderEvent.trigger(data);
	}

	public destroy(): void {
		if (this._components.length == 0) return;
		this._components.forEach(component => component.destroy());
	}

	public abstract onShow(data?: any): void;
}
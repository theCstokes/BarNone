import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import AppScreen from "Vee/Screen/AppScreen";

type OnChangeCallback<TState> = (data: any) => void;
type OnClickCallback<TState> = () => void;
type OnSelectCallback<TState> = (data: any) => void;

type OnRenderCallback<TState> = (original: TState, current: TState) => void;

export default class ScreenBind<TState> {
	private _screen: AppScreen;
	private _controlName: string;
	private _controlCallbacks: { [key: string]: any };
	private _onRenderCallback: OnRenderCallback<TState>;

	public constructor(screen: AppScreen, controlName: string) {
		this._screen = screen;
		this._controlName = controlName;
		this._controlCallbacks = {};

		this._screen.bind("onShow", () => {
			for(var key in this._controlCallbacks) {
				if (!this._controlCallbacks.hasOwnProperty(key)) continue;
				if (this._screen.view[this._controlName] === undefined) continue;
				this._screen.view[this._controlName][key] = this._controlCallbacks[key];
			}
		});

		this._screen.bind("onRender", (original: TState, current: TState) => {
			this._onRenderCallback(original, current);
		});
	}

	public onChange(callback: OnChangeCallback<TState>): ScreenBind<TState> {
		this._controlCallbacks["onChange"] = callback
		return this;
	}

	public onClick(callback: OnClickCallback<TState>): ScreenBind<TState> {
		this._controlCallbacks["onClick"] = callback
		return this;
	}

	public onSelect(callback: OnSelectCallback<TState>): ScreenBind<TState> {
		this._controlCallbacks["onSelect"] = callback
		return this;
	}

	public onRender(callback: OnRenderCallback<TState>): ScreenBind<TState> {
		this._onRenderCallback = callback;
		return this;
	}

	public static create<TState>(screen: AppScreen, controlName: string): ScreenBind<TState> {
		return new ScreenBind(screen, controlName);
	}
}
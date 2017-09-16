import AppScreen from "Vee/Screen/AppScreen";

export default class BaseStateManager<TState> {
	private _currentState: TState;
	private _originalState: TState;
	private _screen: AppScreen;

	public constructor(screen: AppScreen, state: TState) {
		this._screen = screen;
		this._currentState = Utils.clone(state);
		this._originalState = Utils.clone(state);
	}

	public getCurrentState(): TState {
		return this._currentState;
	}

	public getOriginalState(): TState {
		return this._originalState;
	}

	public updateState(state: TState) {
		if (this._currentState !== state) {
			this._currentState = Utils.clone(state);
			this._screen.trigger("onRender", this._originalState, this._currentState);
		}
	}
}
import BaseStateManager from "Vee/StateManager/BaseStateManager";

type OnActionCallback<TState> = (state: TState, data: any) => any;

export default class StateBind<TState> {
	private _callback: OnActionCallback<TState>;
	private _stateManager: BaseStateManager<TState>;

	public constructor(stateManager: BaseStateManager<TState>) {
		this._stateManager = stateManager;
	}

	public onAction(callback: OnActionCallback<TState>): StateBind<TState> {
		this._callback = callback;
		return this;
	}

	public trigger(data: any): void {
		var nextState = this._callback(this._stateManager.getCurrentState(), data);
		this._stateManager.updateState(nextState);
	}

	public static create<TState>(stateManager: BaseStateManager<TState>): StateBind<TState> {
		return new StateBind(stateManager);
	}
}
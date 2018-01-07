import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

type StateAction<TState, TData> = (state: TState, data: TData) => TState;

type StateCallable<TState> = (state: TState) => TState;

export class StateBindConfig {
    public resetState: boolean;
}

export default class StateBind {
    public static onAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAction<TState, TData>,
        config?: Partial<StateBindConfig>): StateActionBind<TState, TData> {
        return new StateActionBind<TState, TData>(stateManager, action, config);
    }

    public static onCallable<TState>(
        stateManager: BaseStateManager<TState>, 
        action: StateCallable<TState>,
        config?: Partial<StateBindConfig>): StateCallableBind<TState> {
        return new StateCallableBind<TState>(stateManager, action, config);
    }
}

export class StateCallableBind<TState> {
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateCallable<TState>,
        private _config?: Partial<StateBindConfig>) {
    }

    public trigger(): void {
        var reset = this._config!.resetState;
        var next_state = this._action(this._stateManager.getCurrentState());
        this._stateManager.updateState(next_state);
    }
}

export class StateActionBind<TState, TData> {

    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAction<TState, TData>,
        private _config?: Partial<StateBindConfig>) {
    }

    public trigger(data: TData): void {
        var reset = this._config!.resetState;
        var next_state = this._action(this._stateManager.getCurrentState(), data);
        this._stateManager.updateState(next_state);
    }
}
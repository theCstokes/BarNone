import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";

export type StateAction<TState, TData> = (state: StateTracker<TState>, data: TData) => StateTracker<TState>;

export type StateAsyncAction<TState, TData> = (state: StateTracker<TState>, data: TData) => Promise<StateTracker<TState>>;

export type StateCallable<TState> = (state: StateTracker<TState>) => StateTracker<TState>;

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

    public static onAsyncAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAsyncAction<TState, TData>,
        config?: Partial<StateBindConfig>): StateAsyncActionBind<TState, TData> {
        return new StateAsyncActionBind<TState, TData>(stateManager, action, config);
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
        var reset = this._config && this._config.resetState;
        var next_state = this._action(this._stateManager.getState());
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
        var reset = this._config && this._config.resetState;
        var next_state = this._action(this._stateManager.getState(), data);
        this._stateManager.updateState(next_state);
    }
}

export class StateAsyncActionBind<TState, TData> {

    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAsyncAction<TState, TData>,
        private _config?: Partial<StateBindConfig>) {
    }

    public async trigger(data: TData): Promise<void> {
        var reset = this._config && this._config.resetState;
        var next_state = await this._action(this._stateManager.getState(), data);
        this._stateManager.updateState(next_state);
    }
}
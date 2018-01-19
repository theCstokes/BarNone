import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";

/**
 * State action function.
 */
export type StateAction<TState, TData> = (state: StateTracker<TState>, data: TData) => StateTracker<TState>;

/**
 * State action async function.
 */
export type StateAsyncAction<TState, TData> = (state: StateTracker<TState>, data: TData) => Promise<StateTracker<TState>>;

/**
 * State callable function.
 */
export type StateCallable<TState> = (state: StateTracker<TState>) => StateTracker<TState>;


// export class StateBindConfig {
//     public resetState: boolean;
// }

/**
 * State Bind Utils.
 */
export default class StateBind {
    public static onAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAction<TState, TData>): StateActionBind<TState, TData> {
        return new StateActionBind<TState, TData>(stateManager, action);
    }

    public static onAsyncAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAsyncAction<TState, TData>): StateAsyncActionBind<TState, TData> {
        return new StateAsyncActionBind<TState, TData>(stateManager, action);
    }

    public static onCallable<TState>(
        stateManager: BaseStateManager<TState>, 
        action: StateCallable<TState>): StateCallableBind<TState> {
        return new StateCallableBind<TState>(stateManager, action);
    }
}

/**
 * State Callable Bind.
 */
export class StateCallableBind<TState> {
    /**
     * Create state callable bind
     * @param _stateManager - state manager
     * @param _action - action
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateCallable<TState>) {
    }

    /**
     * Trigger action.
     */
    public trigger(): void {
        // var reset = this._config && this._config.resetState;
        var next_state = this._action(this._stateManager.getState());
        this._stateManager.updateState(next_state);
    }
}

/**
 * State action bind.
 */
export class StateActionBind<TState, TData> {

    /**
     * Create state action bind.
     * @param _stateManager 
     * @param _action 
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAction<TState, TData>) {
    }

    /**
     * Trigger action.
     * @param data - data
     */
    public trigger(data: TData): void {
        // var reset = this._config && this._config.resetState;
        var next_state = this._action(this._stateManager.getState(), data);
        this._stateManager.updateState(next_state);
    }
}

/**
 * State async bind.
 */
export class StateAsyncActionBind<TState, TData> {

    /**
     * Crate async action bind
     * @param _stateManager 
     * @param _action 
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAsyncAction<TState, TData>) {
    }

    /**
     * Trigger action.
     * @param data - data
     */
    public async trigger(data: TData): Promise<void> {
        // var reset = this._config && this._config.resetState;
        var next_state = await this._action(this._stateManager.getState(), data);
        this._stateManager.updateState(next_state);
    }
}
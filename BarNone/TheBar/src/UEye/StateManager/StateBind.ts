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

/**
 * State async callable function.
 */
export type StateAsyncCallable<TState> = (state: StateTracker<TState>) => Promise<StateTracker<TState>>;


// export class StateBindConfig {
//     public resetState: boolean;
// }

/**
 * State Bind Utils.
 */
export default class StateBind {
    /**
     * Creates action state bind
     * @param stateManager - state manager to bind with
     * @param action - state action
     */
    public static onAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAction<TState, TData>): StateActionBind<TState, TData> {
        return new StateActionBind<TState, TData>(stateManager, action);
    }

    /**
     * Create async action state bind
     * @param stateManager - state manager to bind with
     * @param action - async state action
     */
    public static onAsyncAction<TState, TData>(
        stateManager: BaseStateManager<TState>, 
        action: StateAsyncAction<TState, TData>): StateAsyncActionBind<TState, TData> {
        return new StateAsyncActionBind<TState, TData>(stateManager, action);
    }

    /**
     * Create callable state bind
     * @param stateManager - state manager to bind with
     * @param action - callable state action
     */
    public static onCallable<TState>(
        stateManager: BaseStateManager<TState>, 
        action: StateCallable<TState>): StateCallableBind<TState> {
        return new StateCallableBind<TState>(stateManager, action);
    }

    /**
     * Create async callable state bind
     * @param stateManager - state manager to bind with
     * @param action - async state action
     */
    public static onAsyncCallable<TState>(
        stateManager: BaseStateManager<TState>, 
        action: StateAsyncCallable<TState>): StateAsyncCallableBind<TState> {
        return new StateAsyncCallableBind<TState>(stateManager, action);
    }
}

/**
 * State Callable Bind.
 */
export class StateCallableBind<TState> implements IStateBind {
    public readonly id: string;
    /**
     * Create state callable bind
     * @param _stateManager - state manager
     * @param _action - action
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateCallable<TState>) {
            this.id = Utils.guid();
    }

    /**
     * Trigger action.
     */
    public trigger(): void {
        var originalState = this._stateManager.getState();
        var nextState = this._action(originalState);
        this._stateManager.stateTransition(originalState, nextState, this.id);
    }
}

/**
 * State Callable Bind.
 */
export class StateAsyncCallableBind<TState> implements IStateBind {
    public readonly id: string;
    /**
     * Create state callable bind
     * @param _stateManager - state manager
     * @param _action - action
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAsyncCallable<TState>) {
            this.id = Utils.guid();
    }

    /**
     * Trigger action.
     */
    public async trigger(): Promise<void> {
        var originalState = this._stateManager.getState();
        var nextState = await this._action(originalState);
        this._stateManager.stateTransition(originalState, nextState, this.id);
    }
}

/**
 * State action bind.
 */
export class StateActionBind<TState, TData> implements IStateBind {
    public readonly id: string;
    /**
     * Create state action bind.
     * @param _stateManager 
     * @param _action 
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAction<TState, TData>) {
            this.id = Utils.guid();
    }

    /**
     * Trigger action.
     * @param data - data
     */
    public trigger(data: TData): void {
        var originalState = this._stateManager.getState();
        var nextState = this._action(originalState, data);
        this._stateManager.stateTransition(originalState, nextState, this.id);
    }
}

/**
 * State async bind.
 */
export class StateAsyncActionBind<TState, TData> implements IStateBind {
    public readonly id: string;

    /**
     * Crate async action bind
     * @param _stateManager 
     * @param _action 
     */
    public constructor(
        private _stateManager: BaseStateManager<TState>, 
        private _action: StateAsyncAction<TState, TData>) {
            this.id = Utils.guid();
    }

    /**
     * Trigger action.
     * @param data - data
     */
    public async trigger(data: TData): Promise<void> {
        var originalState = this._stateManager.getState();
        var nextState = await this._action(originalState, data);
        this._stateManager.stateTransition(originalState, nextState, this.id);
    }
}

export interface IStateBind {
    id: string;
}
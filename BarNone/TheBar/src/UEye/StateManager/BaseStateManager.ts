import StateBind, { StateCallableBind, StateCallable } from "UEye/StateManager/StateBind";

/**
 * State render callback function.
 */
type RenderCallback<TState> = (current: TState, original: TState) => void;

/**
 * State tracker for current and original state.
 */
export class StateTracker<TState> {
	/**
	 * State construction function.
	 */
	private _TStateType: { new(): TState };

	/**
	 * Current state.
	 */
	public current: TState;

	/**
	 * Original state.
	 */
	public original: TState;

	/**
	 * Create new StateTracker
	 * @param TStateType - state construction object.
	 */
	public constructor(TStateType: { new(): TState }) {
		this._TStateType = TStateType;
		this.current = new TStateType();
		this.original = new TStateType();
	}

	/**
	 * Create new reset state.
	 * @returns - new empty state.
	 */
	public empty(): StateTracker<TState> {
		return new StateTracker<TState>(this._TStateType);
	}

	/**
	 * Creates new state initialized from current state.
	 * @returns - new state from current.
	 */
	public initialize(): StateTracker<TState> {
		var nextState = Utils.clone(this);
		nextState.original = Utils.clone(this.current);
		return nextState;
	}
}

/**
 * Base state manager.
 */
export abstract class BaseStateManager<TState> {
	/**
	 * render callbacks.
	 */
	private _renderCallbackList: RenderCallback<TState>[];

	/**
	 * state tracker object.
	 */
	protected _stateTracker: StateTracker<TState>;

	/**
	 * Create new Base state manager
	 * @param TStateType - state builder.
	 */
	public constructor(TStateType: { new(): TState }) {
		this._renderCallbackList = [];
		this._stateTracker = new StateTracker(TStateType);
		this.initialize();
	}

	public abstract async initialize(): Promise<void>;

	/**
	 * adds render callback to state manager
	 * @param renderCallback - render callback
	 */
	public bind(renderCallback: RenderCallback<TState>) {
		this._renderCallbackList.push(renderCallback);
	}

	/**
	 * Gets state tracker object.
	 */
	public getState(): StateTracker<TState> {
		return Utils.clone(this._stateTracker);
	}

	/**
	 * Current state from state tracker.
	 */
	public getCurrentState(): TState {
		return Object.freeze(this.getState().current);
	}

	/**
	 * Original state from state tracker.
	 */
	public getOriginalState(): TState {
		return Object.freeze(this.getState().original);
	}

	/**
	 * Update state.
	 * @param state - tacker object
	 */
	public updateState(state: StateTracker<TState>) {
		if (JSON.stringify(state) !== JSON.stringify(this._stateTracker)) {
			this._stateTracker = Utils.clone(state);

			this._renderCallbackList.forEach(rc => rc(
				this.getCurrentState(),
				this.getOriginalState()
			));
		}
	}

	// public abstract init(): void;
}
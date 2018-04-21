import StateBind, { StateCallableBind, StateCallable, StateAsyncActionBind } from "UEye/StateManager/StateBind";
import { ChildStateUpdater } from "UEye/StateManager/ChildStateManager";

/**
 * State render callback function.
 */
export type RenderCallback<TState> = (current: TState, original: TState) => void;

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

	/**
	 * Creates new state where current state is reset from original.
	 * @returns - new state from current.
	 */
	public reset(): StateTracker<TState> {
		var nextState = Utils.clone(this);
		nextState.current = Utils.clone(this.original);
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

		this._resetBind = StateBind
			.onAsyncAction<TState, any>(this, async (state, data) => {
				return state.reset();
			});
	}

	public async initialize(): Promise<void> {
		await this.onInitialize();
	}

	public onReset(bind: StateAsyncActionBind<TState, any>) {
		this._resetBind = bind;
	}

	private _resetBind: StateAsyncActionBind<TState, any>;

	// public get Reset(): StateAsyncActionBind<TState, any> {
	// 	return this._resetBind;
	// }

	public readonly Reset = StateBind
		.onCallable<TState>(this, (state) => {
			return state.reset();
		});

	// public readonly Reset = StateBind
	// 	.onCallable<TState>(this, (state) => {
	// 		return state.reset();
	// 	})

	protected async onInitialize(): Promise<void> { }
	// protected async onReset(): Promise<void> {}

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

	public async save(): Promise<void> {

	}

	/**
	 * Update state.
	 * @param nextState - tacker object
	 */
	public stateTransition(
		originalState: StateTracker<TState>,
		nextState: StateTracker<TState>,
		id?: string
	) {
		// if (JSON.stringify(nextState) !== JSON.stringify(originalState)) {
		if (nextState !== originalState) {
			this._stateTracker = Utils.clone(nextState);

			this._renderCallbackList.forEach(rc => rc(
				this.getCurrentState(),
				this.getOriginalState()
			));
		}
	}

	/**
	 * Updates the state object with the given sub state object.
	 * @param updater - the updater function
	 * @param state - the child state tracker
	 * @param trackChildChanges - flag to trigger render on state update. 
	 * 														if true render may be triggered if changes occurred
	 * 														if false original state will be updated as well as current
	 */
	public updateSubState<TChildState>(
		updater: ChildStateUpdater<TChildState, TState>,
		state: StateTracker<TChildState>,
		trackChildChanges: boolean
	): void {
		updater(this._stateTracker.current, state.current);

		if (trackChildChanges) {
			// this.updateState(this._stateTracker);
			this._renderCallbackList.forEach(rc => rc(
				this.getCurrentState(),
				this.getOriginalState()
			));
		} else {
			updater(this._stateTracker.original, state.original);
		}
	}
}
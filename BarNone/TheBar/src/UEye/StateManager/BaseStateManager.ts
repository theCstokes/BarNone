import StateBind, { StateCallableBind, StateCallable } from "UEye/StateManager/StateBind";

type RenderCallback<TState> = (current: TState, original: TState) => void;

export class StateTracker<TState> {
	private _TStateType: { new(): TState };
    public current: TState;
	public original: TState;
	
	public constructor(TStateType: { new(): TState }) {
		this._TStateType = TStateType;
		this.current = new TStateType();
		this.original = new TStateType();
	}

	public empty(): StateTracker<TState> {
		return new StateTracker<TState>(this._TStateType);
	}

	public initialize(): StateTracker<TState> {
		var nextState = Utils.clone(this);
		nextState.original = Utils.clone(this.current);
		return nextState;
	}
}

export abstract class BaseStateManager<TState> {
    private _renderCallbackList: RenderCallback<TState>[];
    private _stateTracker: StateTracker<TState>;
	// private _currentState: TState;
	// private _originalState: TState;
	// private _screen: AppScreen;
	// private _saveEvent: DataEvent<void>;

	public constructor(TStateType: { new(): TState }) {
        // this.resetState = StateBind.onCallable(this, resetCallable, {
        //     resetState: true
		// });
		this._renderCallbackList = [];
		this._stateTracker = new StateTracker(TStateType);
		// this._currentState = Utils.clone(state);
		// this._originalState = Utils.clone(state);

		// this._saveEvent = new DataEvent<void>();
    }
    
    // public resetState: StateCallableBind<TState>;

	public bind(renderCallback: RenderCallback<TState>) {
		this._renderCallbackList.push(renderCallback);
    }
    
    public getState(): StateTracker<TState> {
		return Utils.clone(this._stateTracker);
	}

	public getCurrentState(): TState {
		return Object.freeze(this._stateTracker.current);
	}

	public getOriginalState(): TState {
		return Object.freeze(this._stateTracker.original);
	}

	public updateState(state: StateTracker<TState>) {
		if (this._stateTracker !== state) {
			this._stateTracker = Utils.clone(state);
			// if (reset) {
			// 	this._originalState = Utils.clone(state);
			// }
			this._renderCallbackList.forEach(rc => rc(
                this.getCurrentState(),
                this.getOriginalState()
			));
		}
	}

	// public get screen(): AppScreen {
	// 	return this._screen;
	// }

	// public get saveEvent(): IDataEvent<void> {
	// 	return this._saveEvent.expose();
	// }

	// public async save(): Promise<void> {
	// 	await this.onSave();
	// 	this._saveEvent.trigger();
	// }

	// public abstract init(): void;

	// public abstract onSave(): void;
}
import { AppScreen } from "Vee/Screen/AppScreen";
import DataEvent from "Vee/Core/DataEvent/DataEvent";
import { IDataEvent } from "Vee/Core/DataEvent/IDataEvent";

export abstract class BaseStateManager<TState> {
	private _currentState: TState;
	private _originalState: TState;
	private _screen: AppScreen;
	private _saveEvent: DataEvent<void>;

	public constructor(screen: AppScreen, state: TState) {
		this._screen = screen;
		this._currentState = Utils.clone(state);
		this._originalState = Utils.clone(state);

		this._saveEvent = new DataEvent();
	}

	public getCurrentState(): TState {
		return this._currentState;
	}

	public getOriginalState(): TState {
		return this._originalState;
	}

	public updateState(state: TState, reset: boolean = false) {
		if (this._currentState !== state) {
			this._currentState = Utils.clone(state);
			if (reset) {
				this._originalState = Utils.clone(state);
			}
			this._screen.render({ original: this._originalState, current: this._currentState });
		}
	}

	public get screen(): AppScreen {
		return this._screen;
	}

	public get saveEvent(): IDataEvent<void> {
		return this._saveEvent.expose();
	}

	public async save(): Promise<void> {
		await this.onSave();
		this._saveEvent.trigger();
	}

	public abstract init(): void;

	public abstract onSave(): void;
}
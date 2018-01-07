import { AppScreen } from "UEye/Screen/AppScreen";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import { IDataEvent } from "UEye/Core/DataEvent/IDataEvent";

type RenderCallback<TState> = (original: TState, current: TState) => void;

export abstract class BaseStateManager<TState> {
	private _renderCallbackList: RenderCallback<TState>[];
	private _currentState: TState;
	private _originalState: TState;
	// private _screen: AppScreen;
	private _saveEvent: DataEvent<void>;

	public constructor(state: TState) {
		this._currentState = Utils.clone(state);
		this._originalState = Utils.clone(state);

		this._saveEvent = new DataEvent<void>();
	}

	public bind(renderCallback: RenderCallback<TState>) {
		this._renderCallbackList.push(renderCallback);
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
			this._renderCallbackList.forEach(rc => rc(
				Object.freeze(this._currentState),
				Object.freeze(this._originalState)
			));
		}
	}

	// public get screen(): AppScreen {
	// 	return this._screen;
	// }

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
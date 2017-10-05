import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";
import DataManager from "Application/Data/DataManager";

export class State {
	public id: number;
	public name: string = "";
	public age: number;
}

export class StateManager extends BaseStateManager<State> {
	private readonly _resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.id = data.id;
			nextState.name = data.name;
			nextState.age = data.age;

			return nextState;
		});

	private readonly _nameChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.name = data as string;

			return nextState;
		});

	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public get resetState(): IDataBind {
		return this._resetState.expose();
	}

	public get nameChange(): IDataBind {
		return this._nameChange.expose();
	}

	public init(): void {
		throw new Error("Method not implemented.");
	}

	public async onSave(): Promise<void> {
		var currentState = this.getCurrentState();
		await DataManager.Users.update(currentState.id, {
			id: currentState.id,
			name: currentState.name,
			userName: currentState.name,
			password: ""
		});
	}
}
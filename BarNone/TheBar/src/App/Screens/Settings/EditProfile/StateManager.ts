import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";

// import { AppScreen } from "UEye/Screen/AppScreen";
// import StateBind from "UEye/Core/DataBind/StateBind";
// import { IDataBind } from "UEye/Core/DataBind/IDataBind";
// import DataManager from "Application/Data/DataManager";

export class State {
	public id: number;
	public name: string = "";
	public age: number;
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}
	
	public readonly ResetState = StateBind
		.onAction<State, {
			id: number,
			name: string,
			age: number
		}>(this, (state, data) => {
			var nextState = state.empty();
			nextState.current.id = data.id;
			nextState.current.name = data.name;
			nextState.current.age = data.age;

			return nextState.initialize();
		});

	public readonly NameChange = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.name = data as string;

			return nextState;
		});

	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	// public get resetState(): IDataBind {
	// 	return this._resetState.expose();
	// }

	// public get nameChange(): IDataBind {
	// 	return this._nameChange.expose();
	// }

	public async onInitialize(): Promise<void> {
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
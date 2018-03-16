import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";

// import { AppScreen } from "UEye/Screen/AppScreen";
// import StateBind from "UEye/Core/DataBind/StateBind";
// import { IDataBind } from "UEye/Core/DataBind/IDataBind";
// import DataManager from "Application/Data/DataManager";

export class State {
	public newPassword: string = "";
	public retypePassword: string = "";
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}

	public async onInitialize(): Promise<void> { }

	public readonly CreateState = StateBind
		.onCallable<State>(this, (state) => {
			var nextState = state.empty();

			return nextState.initialize();
		});

	public readonly NewPassword = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.newPassword = data as string;

			return nextState;
		});

	public readonly RetypePassword = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.retypePassword = data as string;

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

	public init(): void {
		// this.resetState.trigger();
	}

	public async onSave(): Promise<void> {
		var currentState = this.getCurrentState();

		if (currentState.newPassword == currentState.retypePassword) {
			console.log(currentState.newPassword);
		}

		// await DataManager.Users.update(currentState.id, {
		// 	id: currentState.id,
		// 	name: "",
		// 	userName: "",
		// 	password: ""
		// });
	}
}
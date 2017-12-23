import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";
import DataManager from "Application/Data/DataManager";

export class State {
	public userName: string = "";
	public password1: string = "";
	public password2: string = "";
}

export class StateManager extends BaseStateManager<State> {
	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public readonly resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			return nextState;
		}).expose();

	public readonly userNameChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.userName = data as string;

			return nextState;
		}).expose();

	public readonly password1Change = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.password1 = data as string;

			return nextState;
		}).expose();

	public readonly password2Change = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.password2 = data as string;

			return nextState;
		}).expose();

	public init(): void {
		this.resetState.trigger();
	}

	public async onSave(): Promise<void> {
		// var currentState = this.getCurrentState();
		// await DataManager.Users.update(currentState.id, {
		// 	id: currentState.id,
		// 	name: currentState.name,
		// 	userName: currentState.name,
		// 	password: ""
		// });
	}
}
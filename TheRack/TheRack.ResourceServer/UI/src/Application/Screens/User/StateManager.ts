import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import StateBind from "Vee/StateManager/StateBind";
import AppScreen from "Vee/Screen/AppScreen";
import DataManager from "Application/Data/DataManager";
import User from "Application/Data/Models/User/User";

export class State {
	public userList: User[]
	public currentId: number;
}

export class StateManager extends BaseStateManager<State> {
	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public readonly resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.userList = data;
			nextState.currentId = nextState.userList[0].id;

			return nextState;
		});

	public readonly selectionChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.currentId = data.id;

			return nextState;
		});

	public async init(): Promise<void> {
		var data = await DataManager.Users.load();
		this.resetState.trigger(data);
	}
}
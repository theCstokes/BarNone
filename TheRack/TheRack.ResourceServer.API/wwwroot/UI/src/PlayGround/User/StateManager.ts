import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import DataManager from "Application/Data/DataManager";
import User from "Application/Data/Models/User/User";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";

export class State {
	public userList: User[]
	public currentId: number;
}

export class StateManager extends BaseStateManager<State> {
	public readonly _resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.userList = data;
			nextState.currentId = nextState.userList[0].id;

			return nextState;
		});

	public readonly _selectionChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.currentId = data.id;

			return nextState;
		});

	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public get resetState(): IDataBind {
		return this._resetState.expose();
	}

	public get selectionChange(): IDataBind {
		return this._selectionChange.expose();
	}

	public async init(): Promise<void> {
		var data = await DataManager.Users.load();
		this.resetState.trigger(data);
	}
}
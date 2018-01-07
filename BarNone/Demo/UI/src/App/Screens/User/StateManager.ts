import { ISelectionState, SelectionStateManager } from "App/Core/StateManager/SelectionStateManager";
import User from "App/Data/Models/User/User";
import DataManager from "App/Data/DataManager";

export class State implements ISelectionState<User> {
	public selectionList: User[];
	public selectionId: number;
}

export class StateManager extends SelectionStateManager<User, State> {
	public constructor() {
		super(State, () => DataManager.Users.load());
	}
}
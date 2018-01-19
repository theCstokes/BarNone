import { ISelectionState, SelectionStateManager } from "App/Core/StateManager/SelectionStateManager";
import User from "App/Data/Models/User/User";
import DataManager from "App/Data/DataManager";

/**
 * State object for user screen.
 */
export class State implements ISelectionState<User> {
	/**
	 * User selection list.
	 */
	public selectionList: User[];

	/**
	 * User selection id;
	 */
	public selectionId: number;
}

/**
 * User state manager.
 */
export class StateManager extends SelectionStateManager<User, State> {
	public constructor() {
		super(State, () => DataManager.Users.all());
	}
}
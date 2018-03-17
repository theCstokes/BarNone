import User from "App/Data/Models/User/User";
import DataManager from "App/Data/DataManager";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";

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
		super(State);
	}

	public async onInitialize(): Promise<void> { 	}

	protected async listProvider(): Promise<User[]> {
		return await DataManager.Users.all();
	}
}
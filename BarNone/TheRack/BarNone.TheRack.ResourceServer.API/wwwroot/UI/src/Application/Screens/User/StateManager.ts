import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import DataManager from "Application/Data/DataManager";
import User from "Application/Data/Models/User/User";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";
import { SelectionStateManager, ISelectionState } from "Application/Core/StateManager/SelectionStateManager";

export class State implements ISelectionState<User> {
	public selectionList: User[];
	public selectionId: number;
}

export class StateManager extends SelectionStateManager<User, State> {
	public constructor(screen: AppScreen) {
		super(screen, new State(), () => DataManager.Users.load({
			useOverride: true
		}));
	}
}
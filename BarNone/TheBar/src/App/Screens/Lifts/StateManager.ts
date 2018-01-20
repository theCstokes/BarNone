import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { ISelectionState, SelectionStateManager } from "App/Core/StateManager/SelectionStateManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
// import { AppScreen } from "UEye/Screen/AppScreen";
// import DataManager from "Application/Data/DataManager";
// import User from "Application/Data/Models/User/User";
// import StateBind from "UEye/Core/DataBind/StateBind";
// import { IDataBind } from "UEye/Core/DataBind/IDataBind";
// import { SelectionStateManager, ISelectionState } from "Application/Core/StateManager/SelectionStateManager";
// import LiftFolder from "Application/Data/Models/LiftFolder/LiftFolder";
/**
 *  Represents State for Lift Screen.
 */
export class State implements ISelectionState<Lift> {
	public selectionList: Lift[];
	public selectionId: number;
}

export class StateManager extends SelectionStateManager<Lift, State> {
	public constructor() {
		super(State, () => DataManager.Lifts.all());
	}
}
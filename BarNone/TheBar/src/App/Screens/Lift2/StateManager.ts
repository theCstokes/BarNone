import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { ISelectionState, SelectionStateManager } from "App/Core/StateManager/SelectionStateManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftListItem, LiftListType } from "App/Screens/Lift2/Models";
import StateBind from "UEye/StateManager/StateBind";
// import { AppScreen } from "UEye/Screen/AppScreen";
// import DataManager from "Application/Data/DataManager";
// import User from "Application/Data/Models/User/User";
// import StateBind from "UEye/Core/DataBind/StateBind";
// import { IDataBind } from "UEye/Core/DataBind/IDataBind";
// import { SelectionStateManager, ISelectionState } from "Application/Core/StateManager/SelectionStateManager";
// import LiftFolder from "Application/Data/Models/LiftFolder/LiftFolder";



export class State implements ISelectionState<LiftListItem> {
	public selectionList: LiftListItem[];
	public selectionId: number;
	public parentID: number | null;
}

export class StateManager extends SelectionStateManager<LiftListItem, State> {
	public constructor() {
		super(State, StateManager.onLoad);
	}

	public ParentChange = StateBind.onAsyncAction<State, {
		parentID: number | null;
		selectionId?: number
	}>(this, async (state, data) => {
		console.log(data);
		var list = await StateManager.onLoad(data.parentID);
		var nextState = state.empty();
		nextState.current.selectionList = list;

		if (data.selectionId !== undefined) {
			nextState.current.selectionId = data.selectionId;
		} else if (nextState.current.selectionList.length > 0) {
			nextState.current.selectionId = nextState.current.selectionList[0].id;
		}
		
		nextState.current.parentID = data.parentID;

		return nextState.initialize();
	});

	private static async onLoad(parentID: number | null = null): Promise<LiftListItem[]> {
		var results = await Promise.all([
			DataManager.LiftFolders.all({
				filter: {
					property: (l) => l.parentID,
					comparisons: "eq",
					value: parentID
				}
			}
			),
			DataManager.Lifts.all({
				filter: {
					property: (l) => l.parentID,
					comparisons: "eq",
					value: parentID
				}
			})
		]);

		return results[0].map(lf => {
			return {
				id: lf.id,
				name: lf.name,
				type: LiftListType.Folder,
				parentID: lf.parentID
			}
		}).concat(results[1].map(l => {
			return {
				id: l.id,
				name: l.name,
				type: LiftListType.Lift,
				parentID: l.parentID
			}
		}));
	}
}
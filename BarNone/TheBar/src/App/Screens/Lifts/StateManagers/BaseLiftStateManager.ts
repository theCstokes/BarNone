import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftListItem, LiftListType } from "App/Screens/Lifts/Models";
import StateBind from "UEye/StateManager/StateBind";
import { ISelectionState, SelectionStateManager } from "UEye/StateManager/SelectionStateManager";

export class State implements ISelectionState<LiftListItem> {
	public selectionList: LiftListItem[];
	public selectionId: number;
	public parentID: number | null;
}

export enum ELiftType { Lift = "Lift", Shared = "Shared" }

export abstract class BaseLiftStateManager extends SelectionStateManager<LiftListItem, State> {
	
	public constructor() {
		super(State);
	}

	public async onInitialize(): Promise<void> { 	}

	public ParentChange = StateBind.onAsyncAction<State, {
		parentID: number | null;
		selectionId?: number
	}>(this, async (state, data) => {
		console.log(data);
		var list = await this.onLoad(data.parentID);
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

	protected abstract async onLoad(parentID: number | null): Promise<LiftListItem[]>;
}
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import Comment from "App/Data/Models/Comment/Comment";
import LiftType from "App/Data/Models/Lift/LiftType";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import BodyData from "App/Data/Models/BodyData/BodyData";
import { ELiftType } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";
import ParentStateManager from "UEye/StateManager/ParentStateManager";
import { ChartTabState } from "App/Screens/Lifts/ChartTab/ChartTabStateManager";

export class State {
	public id: number;
	public name: string;
	public liftType: LiftType;
	public comments: Comment[];
	public parentID: number;
	public bodyData: BodyData;
	public type: ELiftType;
	public context: ChartTabState;
}

export class StateManager extends ParentStateManager<State> {

	public async onInitialize(): Promise<void> {
		this.s_LiftTypeList = await DataManager.LiftTypes.all();
		this.s_FolderList = await DataManager.LiftFolders.all();
	}

	//#region Public Static State Property(s).
	public s_LiftTypeList: LiftType[];
	public s_FolderList: LiftFolder[];
	//#endregion

	//#region Public Constructor(s).
	public constructor() {
		super(State);
	}
	//#endregion

	//#region State Action(s).
	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string,
			type: ELiftType
		}>(this, async (state, data) => {
			// Setup static data.
			var nextState = state.empty();


			nextState.current.type = data.type;

			var lift = null;
			if (data.type === ELiftType.Lift) {
				lift = await DataManager.Lifts.single(data.id, { includeDetails: true });
			} else if (data.type === ELiftType.Shared) {
				lift = await DataManager.SharedLifts.single(data.id, { includeDetails: true });

			}
			nextState.current.name = lift!.name;
			nextState.current.liftType = lift!.details.liftType;
			nextState.current.parentID = lift!.parentID;
			nextState.current.bodyData = lift!.details.bodyData;

			var comments = await DataManager.LiftComments.all({
				params: {
					liftID: data.id
				}
			});

			nextState.current.comments = comments;

			nextState.current.id = data.id;
			nextState.current.name = data.name;

			return nextState.initialize();
		});

	public readonly NameChange = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.name = data as string;

			return nextState;
		});

	public readonly RefreshComments = StateBind
		.onAsyncCallable<State>(this, async (state) => {
			var nextState = Utils.clone(state);

			nextState.current.comments = await DataManager.LiftComments.all({
				params: {
					liftID: nextState.current.id
				}
			});

			return nextState;
		});

	public readonly ParentChange = StateBind
		.onAction<State, {
			parentID: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.parentID = data.parentID;
			return nextState;
		});

	//#endregion

	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	// public get resetState(): IDataBind {
	// 	return this._resetState.expose();
	// }

	// public get nameChange(): IDataBind {
	// 	return this._nameChange.expose();
	// }

	// public init(): void {
	// 	// var data = await DataManager.Lifts.single()
	// 	this.ResetState.trigger();
	// }

	public async onSave(): Promise<void> {
		var currentState = this.getCurrentState();
		await DataManager.Users.update(currentState.id, {
			id: currentState.id,
			name: currentState.name,
			userName: currentState.name,
			password: ""
		});
	}
}
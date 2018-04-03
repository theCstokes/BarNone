import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import LiftType from "App/Data/Models/Lift/LiftType";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import BodyData from "App/Data/Models/BodyData/BodyData";
import { ELiftType } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";
import { LiftPermissionState } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionStateManager";
import Permission from "App/Data/Models/Lift/Permission";
import { LiftVideoState } from "App/Screens/Lifts/LiftEdit/Tabs/Video/LiftVideoStateManager";
import { ChartTabState } from "App/Screens/Lifts/LiftEdit/Tabs/Charts/ChartTabStateManager";
import { LiftCommentsState } from "App/Screens/Lifts/LiftEdit/Tabs/Comments/LiftCommentsStateManager";
// import ParentStateManager from "UEye/StateManager/ParentStateManager";
// import { ChartTabState } from "App/Screens/Lifts/ChartTab/ChartTabStateManager";

export class State {
	public id: number;
	public name: string;
	public liftType: LiftType;
	// public comments: Comment[];
	public parentID: number;
	public bodyDataID: number;
	public bodyData: BodyData;
	public type: ELiftType;
	public chartState: ChartTabState = new ChartTabState();
	public liftPermissionState: LiftPermissionState = new LiftPermissionState();
	public liftVideoState: LiftVideoState = new LiftVideoState();
	public liftCommentsState: LiftCommentsState = new LiftCommentsState();
}

export class StateManager extends BaseStateManager<State> {

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
		this.onReset(this.ResetState);
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

			var lift = {} as Lift;
			if (data.type === ELiftType.Lift) {
				var lift = await DataManager.Lifts.single(data.id);
			} else if (data.type === ELiftType.Shared) {
				var lift = await DataManager.SharedLifts.single(data.id);
			} else {
				return state;
			}

			nextState.current.liftType = await DataManager.LiftTypes.single(lift.liftTypeID);

			nextState.current.name = lift!.name;
			// nextState.current.liftType = lift!.details!.liftType!;
			nextState.current.parentID = lift!.parentID;
			nextState.current.bodyDataID = lift!.bodyDataID;
			// nextState.current.bodyData = lift!.details!.bodyData!;

			// nextState.current.liftPermissionState.liftID = lift!.id!;
			// nextState.current.liftPermissionState.permissions = lift!.details!.permissions!;

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

	public async save(): Promise<void> {
		let current = this.getCurrentState();
		let permissions: Permission[] = current.liftPermissionState.permissions
			.filter(p => p.isNew)
			.map(p => {
				return {
					// id: p.id,
					liftID: current.id,
					userID: p.userID
				}
			}) as Permission[];

		await DataManager.Lifts.update(current.id, {
			id: current.id,
			name: current.name,
			updateFilter: ["Permissions"],
			details: {
				permissions: permissions
			}
		});

		// await this.Reset.trigger(null);
	}

	// public async onSave(): Promise<void> {
	// 	var currentState = this.getCurrentState();
	// 	await DataManager.Users.update(currentState.id, {
	// 		id: currentState.id,
	// 		name: currentState.name,
	// 		userName: currentState.name,
	// 		password: ""
	// 	});
	// }
}
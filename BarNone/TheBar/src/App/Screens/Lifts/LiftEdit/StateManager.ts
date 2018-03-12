import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import Comment from "App/Data/Models/Comment/Comment";
import { ELiftType } from "App/Screens/Lifts/StateManager";
import LiftType from "App/Data/Models/Lift/LiftType";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import BodyData from "App/Data/Models/BodyData/BodyData";

export class State {
	public id: number;
	public name: string;
	public liftType: LiftType;
	public comments: Comment[];
	public parentID: number;
	public bodyData: BodyData;
}

export class StateManager extends BaseStateManager<State> {
	public async initialize(): Promise<void> {
		this.s_LiftTypeList = await DataManager.LiftTypes.all();
		this.s_FolderList = await DataManager.LiftFolders.all();
	}

	//#region Public Static State Property(s).
	public s_LiftTypeList: LiftType[];
	public s_FolderList: LiftFolder[];
	//#endregion

	//#region Private Field(s).
	private _type: ELiftType;
	//#endregion

	//#region Public Constructor(s).
	public constructor(type: ELiftType) {
		super(State);
		this._type = type;
	}
	//#endregion

	public readonly Create = StateBind
		.onAsyncCallable<State>(this, async (state) => {
			await this.initialize();
			return state;
		});

	//#region State Action(s).
	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string
		}>(this, async (state, data) => {
			// Setup static data.
			var nextState = state.empty();

			var lift = null;
			if (this._type === ELiftType.Lift) {
				lift = await DataManager.Lifts.single(data.id, { includeDetails: true });
			} else if (this._type === ELiftType.Shared) {
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
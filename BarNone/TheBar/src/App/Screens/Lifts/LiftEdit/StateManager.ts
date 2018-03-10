import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import Comment from "App/Data/Models/Comment/Comment";
import { ELiftType } from "App/Screens/Lifts/StateManager";
import LiftType from "App/Data/Models/Lift/LiftType";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";

export class State {
	public id: number;
	public name: string = "";
	public age: number;
	public lift: Lift;
	public comments: Comment[];
	public liftType: LiftType;
}

export class StateManager extends BaseStateManager<State> {

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

	//#region State Action(s).
	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string
		}>(this, async (state, data) => {
			// Setup static data.
			this.s_LiftTypeList = await DataManager.LiftTypes.all();
			this.s_FolderList = await DataManager.LiftFolders.all();

			var nextState = state.empty();

			if (this._type === ELiftType.Lift) {
				nextState.current.lift = await DataManager.Lifts.single(data.id, { includeDetails: true });

			} else if (this._type === ELiftType.Shared) {
				nextState.current.lift = await DataManager.SharedLifts.single(data.id, { includeDetails: true });
			}

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
					liftID: nextState.current.lift.id
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
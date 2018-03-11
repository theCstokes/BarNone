import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import Comment from "App/Data/Models/Comment/Comment";
import { LiftType } from "App/Screens/Lifts/StateManager";

export class State {
	public id: number;
	public name: string = "";
	public age: number;
	public lift: Lift;
	public comments: Comment[];
}

export class StateManager extends BaseStateManager<State> {
	private _type: LiftType;
	public constructor(type: LiftType) {
		super(State);
		this._type = type;
	}

	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string
		}>(this, async (state, data) => {
			var nextState = state.empty();

			if (this._type === LiftType.Lift) {
				nextState.current.lift = await DataManager.Lifts.single(data.id, { includeDetails: true });
				console.log("Lift type: ", nextState.current.lift);
			} else if (this._type === LiftType.Shared) {
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

	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	// public get resetState(): IDataBind {
	// 	return this._resetState.expose();
	// }

	// public get nameChange(): IDataBind {
	// 	return this._nameChange.expose();
	// }

	public init(): void {
		// var data = await DataManager.Lifts.single()
		// this.ResetState.trigger();
	}

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
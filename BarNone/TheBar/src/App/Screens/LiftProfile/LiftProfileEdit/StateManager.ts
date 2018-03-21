import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import LiftAnalysisProfile from "App/Data/Models/LiftAnalysisProfile/LiftAnalysisProfile"



export class State {
	public id: number;
	public name: string = "";
	public age: number;
	public liftProfile: LiftAnalysisProfile;
}

export class StateManager extends BaseStateManager<State> {
	
	public constructor() {
		super(State);
	}

	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string
		}>(this, async (state, data) => {
			var nextState = state.empty();

			//var liftProfile = await DataManager.LiftAnalysisProfile(data.id, { includeDetails: true });
			//console.log(liftProfile);
			//nextState.current.liftProfile = liftProfile;

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
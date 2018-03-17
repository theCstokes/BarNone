import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";

export class State {
	public id: number;
	public name: string = "";
	public age: number;
	public folder: LiftFolder;
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}

	public async onInitialize(): Promise<void> { 	}
	
	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			id: number,
			name: string
			// age: number
		}>(this, async (state, data) => {
			var nextState = state.empty();

			var folder = await DataManager.LiftFolders.single(data.id, { includeDetails: true });
			console.log(folder);
			nextState.current.folder = folder;

			nextState.current.id = data.id;
			nextState.current.name = data.name;
			// nextState.current.age = data.age;

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
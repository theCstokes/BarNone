import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";

export class State {
	public userName: string = "";
	public password1: string = "";
	public password2: string = "";
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}
	
	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	public readonly ResetState = StateBind
		.onCallable<State>(this, (state) => {
			var nextState = Utils.clone(state);
			return nextState;
		}, { resetState: true });

	public readonly UserNameChange = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.userName = data as string;

			return nextState;
		});

	public readonly Password1Change = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.password1 = data as string;

			return nextState;
		});

	public readonly Password2Change = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.password2 = data as string;

			return nextState;
		});

	// public init(): void {
	// 	this.resetState.trigger();
	// }

	public async onSave(): Promise<void> {
		// var currentState = this.getCurrentState();
		// await DataManager.Users.update(currentState.id, {
		// 	id: currentState.id,
		// 	name: currentState.name,
		// 	userName: currentState.name,
		// 	password: ""
		// });
	}
}
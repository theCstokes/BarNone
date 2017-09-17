import BaseStateManager from "Vee/StateManager/BaseStateManager";
import StateBind from "Vee/StateManager/StateBind";
import AppScreen from "Vee/Screen/AppScreen";

export class State {
	public name: string = "";
}

export class StateManager extends BaseStateManager<State> {
	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public readonly nameChange = StateBind
		.create(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.name = data as string;

			return nextState;
		});
}
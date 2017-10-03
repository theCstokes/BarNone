import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";

export class State {
	public name: string = "";
	public age: number;
}

export class StateManager extends BaseStateManager<State> {
	private readonly _resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.name = data.name;
			nextState.age = data.age;

			return nextState;
		});

	private readonly _nameChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.name = data as string;

			return nextState;
		});

	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public get resetState(): IDataBind {
		return this._resetState.expose();
	}

	public get nameChange(): IDataBind {
		return this._nameChange.expose();
	}

	public init(): void {
		throw new Error("Method not implemented.");
	}
}
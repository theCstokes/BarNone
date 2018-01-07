import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { IDataBind } from "UEye/Core/DataBind/IDataBind";
import { AppScreen } from "UEye/Screen/AppScreen";
import StateBind from "UEye/StateManager/StateBind";
import { SelectionStateManager } from "Application/Core/StateManager/SelectionStateManager";

class NavElement {
	public id: number;
	public name: string;
	public screenPath: string;
}

export class State {
	public currentScreenId: number;
	public navHistory: number[] = [];
	public navElementList: NavElement[] = [
		{
			id: 1,
			name: "Videos",
			screenPath: "PlayGround/Screens/Video/VideoScreen"
		}
	]
}

export class StateManager extends BaseStateManager<State> {
	public constructor(screen: AppScreen) {
		super(new State());
	}

	public readonly ResetState = StateBind.onCallable<State>(this, (state) => {
		var nextState = Utils.clone(state);
		nextState.currentScreenId = nextState.navElementList[0].id;
		nextState.navHistory.push(nextState.currentScreenId);

		return nextState;
	});

	public readonly SelectionChange = StateBind.onAction<State, {
		id: number
	}>(this, (state, data) => {
		var nextState = Utils.clone(state);
		nextState.currentScreenId = data.id;
		nextState.navHistory.push(nextState.currentScreenId);

		return nextState;
	});

	public readonly NavigateBack = StateBind.onCallable<State>(this, (state) => {
		var nextState = Utils.clone(state);
		nextState.navHistory.pop();

		if (nextState.navHistory.length > 0) {
			var lastIndex = (nextState.navHistory.length - 1);
			nextState.currentScreenId = nextState.navHistory[lastIndex];
		}

		return nextState;
	});

	public init(): void {
		this.ResetState.trigger();
	}

	public onSave(): void {
		throw new Error("Method not implemented.");
	}
}
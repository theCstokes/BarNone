import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
// import { AppScreen } from "UEye/Screen/AppScreen";
// import DataManager from "Application/Data/DataManager";
// import User from "Application/Data/Models/User/User";
// import StateBind from "UEye/Core/DataBind/StateBind";
// import { IDataBind } from "UEye/Core/DataBind/IDataBind";
// import { SelectionStateManager, ISelectionState } from "Application/Core/StateManager/SelectionStateManager";
// import LiftFolder from "Application/Data/Models/LiftFolder/LiftFolder";

class SettingsElement {
	public id: number;
	public name: string;
}

export class State {
	public selectionId: number;
	public settingsHistory: number[] = [];
	public SettingsElementList: SettingsElement[] = [
		{
			id: 1,
			name: "Edit Profile",
			
		},
		{
			id: 2,
			name: "Change Password",
			
		},
		{
			id: 3,
			name: "Edit Video Settings",
			
		},
		{
			id: 4,
			name: "Manage Tags",
			
		}
	]
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}

	// public constructor() {
	// 	super();
	// }

	// public readonly _resetState = StateBind
	// 	.onCallable<State>(this, (state) => {
	// 		var nextState = Utils.clone(state);

	// 		nextState.current.currentScreenId = nextState.current.navElementList[0].id;
	// 		nextState.current.navHistory.push(nextState.current.currentScreenId);

	// 		return nextState;
	// 	}, { resetState: true });

	public readonly ResetState = StateBind
		.onCallable<State>(this, (state) => {
			var nextState = state.empty();

			nextState.current.selectionId = nextState.current.SettingsElementList[0].id;
			nextState.current.settingsHistory.push(nextState.current.selectionId);

			return nextState.initialize();
		});

	public readonly SelectionChange = StateBind
		.onAction<State, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current. selectionId = data.id;
			nextState.current.settingsHistory.push(nextState.current. selectionId);

			return nextState;
		});

	public readonly NavigateBack = StateBind
		.onAction<State, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.settingsHistory.pop();

			if (nextState.current.settingsHistory.length > 0) {
				var lastIndex = (nextState.current.settingsHistory.length - 1);
				nextState.current. selectionId = nextState.current.settingsHistory[lastIndex];
			}

			return nextState;
		});
	}
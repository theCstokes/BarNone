import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import EditProfile from "App/Screens/Settings/EditProfile/EditProfileScreen";
import ChangePassword from "App/Screens/Settings/ChangePassword/ChangePasswordScreen";
import Screen from "UEye/Screen/Screen";
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
	public icon: string;
	public screen: { new(): Screen<any> };
}

export class State {
	public selectionId: number;
	public settingsHistory: number[] = [];
	public SettingsElementList: SettingsElement[] = [
		{
			id: 1,
			name: "Edit Profile",
			icon: "fa-edit",
			screen: EditProfile	
		},
		{
			id: 2,
			name: "Change Password",
			icon: "fa-key",
			screen: ChangePassword
		}
	]
}

export class StateManager extends BaseStateManager<State> {
	public constructor() {
		super(State);
	}

	public async initialize(): Promise<void> { 	}

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
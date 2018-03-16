import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import EditProfile from "App/Screens/Settings/EditProfile/EditProfileScreen";
import ChangePassword from "App/Screens/Settings/ChangePassword/ChangePasswordScreen";
import Screen from "UEye/Screen/Screen";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import DataManager from "App/Data/DataManager";
import SettingsElement from "App/Data/Models/Settings/SettingsElement";

export class State implements ISelectionState<SettingsElement> {
	public selectionId: number;
	public selectionList: SettingsElement[];
}

export class StateManager extends SelectionStateManager<SettingsElement, State> {
	
	public constructor() {
		super(State);
	}

	public async onInitialize(): Promise<void> { 	}

	protected async listProvider(): Promise<SettingsElement[]> {
		return await DataManager.Settings.all();
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

	// public readonly CreateState = StateBind
	// 	.onCallable<State>(this, (state) => {
	// 		var nextState = state.empty();

	// 		nextState.current.selectionId = nextState.current.selectionList[0].id;

	// 		return nextState.initialize();
	// 	});

	// public readonly SelectionChange = StateBind
	// 	.onAction<State, {
	// 		id: number
	// 	}>(this, (state, data) => {
	// 		var nextState = Utils.clone(state);
	// 		nextState.current. selectionId = data.id;
	// 		nextState.current.settingsHistory.push(nextState.current. selectionId);

	// 		return nextState;
	// 	});

	// public readonly NavigateBack = StateBind
	// 	.onAction<State, {
	// 		id: number
	// 	}>(this, (state, data) => {
	// 		var nextState = Utils.clone(state);
	// 		nextState.current.settingsHistory.pop();

	// 		if (nextState.current.settingsHistory.length > 0) {
	// 			var lastIndex = (nextState.current.settingsHistory.length - 1);
	// 			nextState.current. selectionId = nextState.current.settingsHistory[lastIndex];
	// 		}

	// 		return nextState;
	// 	});
	}
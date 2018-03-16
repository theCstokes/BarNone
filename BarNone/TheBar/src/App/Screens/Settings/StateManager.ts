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

	public async onInitialize(): Promise<void> { }

	protected async listProvider(): Promise<SettingsElement[]> {
		return await DataManager.Settings.all();
	}

}
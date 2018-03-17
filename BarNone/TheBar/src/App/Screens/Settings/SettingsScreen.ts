import Screen from "UEye/Screen/Screen"
import SettingsView from "App/Screens/Settings/SettingsView";
import { StateManager, State } from "App/Screens/Settings/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";
import SettingsElement from "App/Data/Models/Settings/SettingsElement";
import { SelectionListScreen } from "UEye/Screen/SelectionListScreen";
import EditScreen from "UEye/Screen/EditScreen";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { LiftStateManager } from "App/Screens/Lifts/StateManagers/LiftStateManager";

export default class SettingsScreen
	extends SelectionListScreen<SettingsView, StateManager, SettingsElement, State> {

	public constructor() {
		super(SettingsView);
	}

	public onRenderEditScreen(data: SettingsElement): EditScreen<any, any> | undefined {
		return UEye.push(data.screen) as EditScreen<any, any>;
	}
	
	public listTransform(item: SettingsElement): IListItem {
		return {
			selected: (item.id === this.stateManager.getCurrentState().selectionId),
			id: item.id,
			name: item.name,
			icon: item.icon
		}
	}

	public async onShow(): Promise<void> {
		super.onShow();
		this.init(await StateManagerFactory.create(StateManager));

		this.stateManager.ResetState.trigger();
	}
}
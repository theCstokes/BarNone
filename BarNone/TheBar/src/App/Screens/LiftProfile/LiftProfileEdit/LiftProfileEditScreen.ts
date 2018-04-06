import LiftProfileEditView from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import UEye from "UEye/UEye"
import StringUtils from "UEye/Core/StringUtils";
import DataManager from "App/Data/DataManager";
import LiftProfileHelper from "App/Screens/LiftProfile/LiftProfileEdit/Tabs/Profile/LiftProfileHelper";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";

export default class LiftProfileEditScreen extends EditScreen<LiftProfileEditView, StateManager> {
	public constructor() {
		super(LiftProfileEditView);
	}

	private _onRender(current: State, original: State) {
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);
		//this.view.profileList.items
		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}

	public async onShow(data: { liftProfileID: number, liftTypeID: number }): Promise<void> {
		console.log("LiftProfileEditScreen Show.");
		this.init(await StateManagerFactory.create(StateManager));
		this.bindSections(LiftProfileHelper);

		this.stateManager.bind(this._onRender.bind(this));

		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};

		await this.stateManager.ResetState.trigger({ liftTypeID: data.liftTypeID });

		super.onShow({ liftProfileID: data.liftProfileID });
	}

	// public save(): void {

	// }
}
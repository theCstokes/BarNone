import LiftProfileEditView from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import UEye from "UEye/UEye"
import StringUtils from "UEye/Core/StringUtils";
import DataManager from "App/Data/DataManager";
//import { LiftType } from "App/Screens/Lifts/StateManager";
import LiftProfileDialogView from "../LiftProfileDialog/LiftProfileDialogView";




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

	public async onShow(data: {id: number, name: string}): Promise<void> {
		this.init(new StateManager());
		this.stateManager.bind(this._onRender.bind(this));
		await this.stateManager.ResetState.trigger({ id: data.id, name: data.name });
		

		//this.view.addButton.onClick = () => UEye.push(LiftProfileDialogView);
		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};
	}

	

	public save(): void {

	}
}
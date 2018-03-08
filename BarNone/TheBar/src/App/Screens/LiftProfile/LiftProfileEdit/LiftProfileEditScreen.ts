import LiftProfileEditView from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";
import DataManager from "App/Data/DataManager";
import { LiftType } from "App/Screens/Lifts/StateManager";




export default class LiftProfileEditScreen extends EditScreen<LiftProfileEditView, StateManager> {
	public constructor() {
		super(LiftProfileEditView);
	}

	private _onRender(current: State, original: State) {
		console.log(current);
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		//this.view.player.frameData = SkeletonBuilder.build(current.lift.details.bodyData);

	}

	public async onShow(): Promise<void> {
		this.init(new StateManager());
		this.stateManager.bind(this._onRender.bind(this));


		// this.view.player.play();
	}

	

	public save(): void {

	}
}
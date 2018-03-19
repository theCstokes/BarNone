import DialogScreen from "UEye/Screen/DialogScreen";
import LiftProfileDialogView from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LiftProfileScreen from "App/Screens/LiftProfile/LiftProfileScreen";
import { LiftProfileDialogStateManager } from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogStateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";

export default class LiftProfileDialogScreen extends DialogScreen<LiftProfileDialogView> {
	private stateManager: LiftProfileDialogStateManager;

	public constructor() {
		super(LiftProfileDialogView);
	}

	private _pipeline = ScreenPipeLine.create()
	//#region Lift Type Drop Down.
	.onShow(() => {
		this.view.analysisTypeDropDown.items = this.stateManager.s_AnalysisTypeList;
		this.view.jointTypeDropDown.items = this.stateManager.s_JointTypeList;
	});
	//#endregion

	public async onShow(): Promise<void> {
		super.onShow();
		this.stateManager = await StateManagerFactory.create(LiftProfileDialogStateManager);
		this._pipeline.onShowInvokable();
	};
}



import DialogScreen from "UEye/Screen/DialogScreen";
import LiftProfileDialogView from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LiftProfileScreen from "App/Screens/LiftProfile/LiftProfileScreen";
import { LiftProfileDialogStateManager, State } from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogStateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { AnalysisTypeEnum } from "App/Data/DataOverride/api/v1/AnalysisTypes";

export default class LiftProfileDialogScreen extends DialogScreen<LiftProfileDialogView> {
	private stateManager: LiftProfileDialogStateManager;

	public constructor() {
		super(LiftProfileDialogView);
	}

	private _pipeline = ScreenPipeLine.create()
	//#region Lift Type Drop Down.
	.onShow(() => {
		this.view.analysisTypeDropDown.items = this.stateManager.s_AnalysisTypeList;
		this.view.accelerationContainer.visible = false;
		this.view.speedContainer.visible = false;
		this.view.positionContainer.visible = false;
		this.view.angleContainer.visible = false;

		// this.view.jointTypeDropDown.items = this.stateManager.s_JointTypeList;
	})
	.onRender((current: State, original: State) => {
		this.view.accelerationContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Acceleration);
		this.view.speedContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Speed);
		this.view.positionContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Position);
		this.view.angleContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Angle);


	});
	//#endregion

	public async onShow(): Promise<void> {
		super.onShow();
		this.stateManager = await StateManagerFactory.create(LiftProfileDialogStateManager);
		this._pipeline.onShowInvokable();
		this.stateManager.CreateState.trigger();
	};
}



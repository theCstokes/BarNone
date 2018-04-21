import DialogScreen, { OnAcceptCallback } from "UEye/Screen/DialogScreen";
import LiftProfileDialogView from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LiftProfileScreen from "App/Screens/LiftProfile/LiftProfileScreen";
import { LiftProfileDialogStateManager, LiftProfileDialogState } from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogStateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { AnalysisTypeEnum } from "App/Data/DataOverride/api/v1/AnalysisTypes";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";

export default class LiftProfileDialogScreen
	extends DialogScreen<LiftProfileDialogView, LiftProfileDialogStateManager, LiftProfileDialogState> {

	public constructor() {
		super(LiftProfileDialogView);
	}

	private _pipeline = ScreenPipeLine.create()
		//#region Lift Type Drop Down.
		.onShow(() => {
			this.view.analysisTypeDropDown.items = this.stateManager.s_AnalysisTypeList;
			this.view.accelerationJointTypeDropDown.items = this.stateManager.s_JointTypeList;
			this.view.speedJointTypeDropDown.items = this.stateManager.s_JointTypeList;
			this.view.positionJointTypeDropDown.items = this.stateManager.s_JointTypeList;
			this.view.angleJointTypeDropDownA.items = this.stateManager.s_JointTypeList;
			this.view.angleJointTypeDropDownB.items = this.stateManager.s_JointTypeList;
			this.view.angleJointTypeDropDownC.items = this.stateManager.s_JointTypeList;

			this.view.accelerationContainer.visible = false;
			this.view.speedContainer.visible = false;
			this.view.positionContainer.visible = false;
			this.view.angleContainer.visible = false;

			this.view.analysisTypeDropDown.onSelect = (data) => {
				this.stateManager.AnalysisTypeChange.trigger({ id: data.id });
			}

			let jointTypeOnSelectHandlerA = (data: IListItem) => {
				this.stateManager.JointTypeChange.trigger({ jointTypeIDA: data.id });
			};

			let jointTypeOnSelectHandlerB = (data: IListItem) => {
				this.stateManager.JointTypeChange.trigger({ jointTypeIDB: data.id });
			};

			let jointTypeOnSelectHandlerC = (data: IListItem) => {
				this.stateManager.JointTypeChange.trigger({ jointTypeIDC: data.id });
			};

			this.view.accelerationJointTypeDropDown.onSelect = jointTypeOnSelectHandlerA;
			this.view.speedJointTypeDropDown.onSelect = jointTypeOnSelectHandlerA;
			this.view.positionJointTypeDropDown.onSelect = jointTypeOnSelectHandlerA;
			this.view.angleJointTypeDropDownA.onSelect = jointTypeOnSelectHandlerA;
			this.view.angleJointTypeDropDownB.onSelect = jointTypeOnSelectHandlerB;
			this.view.angleJointTypeDropDownC.onSelect = jointTypeOnSelectHandlerC;

			// this.view.jointTypeDropDown.items = this.stateManager.s_JointTypeList;
		})
		.onRender((current: LiftProfileDialogState, original: LiftProfileDialogState) => {

			this.view.accelerationContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Acceleration);
			this.view.speedContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Speed);
			this.view.positionContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Position);
			this.view.angleContainer.visible = (current.analysisTypeID === AnalysisTypeEnum.Angle);

			this.view.analysisTypeDropDown.modified = (current.analysisTypeID !== original.analysisTypeID);

			let jointTypeA = this.stateManager.s_JointTypeList.find(t => t.id === current.jointTypeIDA);
			let originalJointTypeA = this.stateManager.s_JointTypeList.find(t => t.id === original.jointTypeIDA);

			let jointTypeB = this.stateManager.s_JointTypeList.find(t => t.id === current.jointTypeIDB);
			let originalJointTypeB = this.stateManager.s_JointTypeList.find(t => t.id === original.jointTypeIDB);

			let jointTypeC = this.stateManager.s_JointTypeList.find(t => t.id === current.jointTypeIDC);
			let originalJointTypeC = this.stateManager.s_JointTypeList.find(t => t.id === original.jointTypeIDC);

			this.view.accelerationJointTypeDropDown.selected = jointTypeA
			this.view.accelerationJointTypeDropDown.modified = !Utils.equivalent(jointTypeA, originalJointTypeA);

			this.view.speedJointTypeDropDown.selected = jointTypeA;
			this.view.speedJointTypeDropDown.modified = !Utils.equivalent(jointTypeA, originalJointTypeA);

			this.view.positionJointTypeDropDown.selected = jointTypeA;
			this.view.positionJointTypeDropDown.modified = !Utils.equivalent(jointTypeA, originalJointTypeA);

			this.view.angleJointTypeDropDownA.selected = jointTypeA;
			this.view.angleJointTypeDropDownA.modified = !Utils.equivalent(jointTypeA, originalJointTypeA);

			this.view.angleJointTypeDropDownB.selected = jointTypeB;
			this.view.angleJointTypeDropDownB.modified = !Utils.equivalent(jointTypeB, originalJointTypeB);

			this.view.angleJointTypeDropDownC.selected = jointTypeC;
			this.view.angleJointTypeDropDownC.modified = !Utils.equivalent(jointTypeC, originalJointTypeC);

		});
	//#endregion

	public async onShow(data: { onAccept: OnAcceptCallback<LiftProfileDialogState>}): Promise<void> {
		this.stateManager = await StateManagerFactory.create(LiftProfileDialogStateManager);
		super.onShow(data);		
		this._pipeline.onShowInvokable();
		this.stateManager.bind(this._pipeline.onRenderInvokable);
		this.stateManager.CreateState.trigger();
	};
}



import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView";
import { StateManager } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { LiftProfileStateManager, LiftProfileState } from "App/Screens/LiftProfile/LiftProfileEdit/Tabs/Profile/LiftProfileStateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import LiftEditView from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditView";
import UEye from "UEye/UEye";
import LiftProfileDialogScreen from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogScreen";
import { LiftProfileDialogState } from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogStateManager";
import AnalysisListItem from "UEye/Elements/Components/AnalysisListItem/AnalysisListItem";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import { AnalysisTypeEnum } from "App/Data/Models/Analysis/AnalysisTypeEnum";

export default class LiftProfileHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftProfileStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftProfileState, original: LiftProfileState) {
        console.log(original, current);

        let isListModified =
            Utils.compare(current.accelerationCriteriaList, original.accelerationCriteriaList) ||
            Utils.compare(current.angleCriteriaList, original.angleCriteriaList) ||
            Utils.compare(current.speedCriteriaList, original.speedCriteriaList) ||
            Utils.compare(current.positionCriteriaList, original.positionCriteriaList);

        this.view.criteriaListInfo.visible = !isListModified;
        this.view.criteriaTab.modified = isListModified;
        this.view.criteriaList.items =
            current.accelerationCriteriaList.map(p => {
                let jointType = this._stateManager
                    .s_JointTypeList.find(t => t.id === p.jointTypeID);

                return BaseListItem.create<AnalysisListItem>({
                    id: 1,
                    icon: "fa-fire",
                    name: AnalysisTypeEnum.Acceleration.name,
                    nameCaption: "Analysis Type",
                    value1: jointType === undefined ? "" : jointType.name,
                    modified: true
                });
            }).concat(
                current.speedCriteriaList.map(p => {
                    let jointType = this._stateManager
                        .s_JointTypeList.find(t => t.id === p.jointTypeID);
    
                    return BaseListItem.create<AnalysisListItem>({
                        id: 1,
                        icon: "fa-fire",
                        name: AnalysisTypeEnum.Speed.name,
                        nameCaption: "Analysis Type",
                        value1: jointType === undefined ? "" : jointType.name,
                        modified: true
                    });
                })
            ).concat (
                current.positionCriteriaList.map(p => {
                    let jointType = this._stateManager
                        .s_JointTypeList.find(t => t.id === p.jointTypeID);
    
                    return BaseListItem.create<AnalysisListItem>({
                        id: 1,
                        icon: "fa-fire",
                        name: AnalysisTypeEnum.Position.name,
                        nameCaption: "Analysis Type",
                        value1: jointType === undefined ? "" : jointType.name,
                        modified: true
                    });
                })
            ).concat(
                current.angleCriteriaList.map(p => {
                    let jointTypeA = this._stateManager
                        .s_JointTypeList.find(t => t.id === p.jointTypeAID);
    
                    let jointTypeB = this._stateManager
                        .s_JointTypeList.find(t => t.id === p.jointTypeAID);
    
                    let jointTypeC = this._stateManager
                        .s_JointTypeList.find(t => t.id === p.jointTypeAID);
    
                    return BaseListItem.create<AnalysisListItem>({
                        id: 1,
                        icon: "fa-fire",
                        name: AnalysisTypeEnum.Angle.name,
                        nameCaption: "Analysis Type",
                        value1: jointTypeA === undefined ? "" : jointTypeA.name,
                        caption1: "Joint Type",
                        value2: jointTypeB === undefined ? "" : jointTypeB.name,
                        caption2: "Joint Type",
                        value3: jointTypeC === undefined ? "" : jointTypeC.name,
                        caption3: "Joint Type",
                        modified: true
                    });
                })
            )
    }

    public async onShow(data: { liftProfileID: number }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(LiftProfileStateManager, this.parentStateManager);

        this._stateManager.bind(this._onRender.bind(this));

        this.view.addButton.onClick = () =>
            (UEye.push(LiftProfileDialogScreen) as LiftProfileDialogScreen)
                .onAccept = this._onSaveHandler.bind(this);

        this._stateManager.CreateState.trigger({ liftProfileID: data.liftProfileID });
    }

    private _onSaveHandler(data: LiftProfileDialogState) {
        console.log(data);
        if (data.analysisTypeID === AnalysisTypeEnum.Acceleration.id) {
            if (data.jointTypeIDA === undefined) return;
            this._stateManager.AddAccelerationCriteria.trigger({
                jointTypeID: data.jointTypeIDA
            });
        } else if (data.analysisTypeID === AnalysisTypeEnum.Angle.id) {
            if (data.jointTypeIDA === undefined) return;
            if (data.jointTypeIDB === undefined) return;
            if (data.jointTypeIDC === undefined) return;
            this._stateManager.AddAngleCriteria.trigger({
                jointTypeAID: data.jointTypeIDA,
                jointTypeBID: data.jointTypeIDB,
                jointTypeCID: data.jointTypeIDC
            });
        } else if (data.analysisTypeID === AnalysisTypeEnum.Position.id) {
            if (data.jointTypeIDA === undefined) return;
            this._stateManager.AddPositionCriteria.trigger({
                jointTypeID: data.jointTypeIDA
            });
        } else if (data.analysisTypeID === AnalysisTypeEnum.Speed.id) {
            if (data.jointTypeIDA === undefined) return;
            this._stateManager.AddSpeedCriteria.trigger({
                jointTypeID: data.jointTypeIDA
            });
        }
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
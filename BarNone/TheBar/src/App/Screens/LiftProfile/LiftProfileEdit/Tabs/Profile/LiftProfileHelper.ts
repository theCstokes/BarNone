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

export default class LiftProfileHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftProfileStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftProfileState, original: LiftProfileState) {
        console.log(original, current);

        this.view.criteriaListInfo.visible = (current.profiles.length === 0);
        this.view.criteriaTab.modified = (current.profiles.length > 0);
        this.view.criteriaList.items = current.profiles.map(p => {
            let analysisType = this._stateManager
                .s_AnalysisTypeList.find(t => t.id === p.analysisTypeID);

            let jointTypeIDA = this._stateManager
                .s_JointTypeList.find(t => t.id === p.jointTypeIDA);

            let jointTypeIDB = this._stateManager
                .s_JointTypeList.find(t => t.id === p.jointTypeIDB);

            let jointTypeIDC = this._stateManager
                .s_JointTypeList.find(t => t.id === p.jointTypeIDC);

            return BaseListItem.create<AnalysisListItem>({
                id: 1,
                icon: "fa-fire",
                name: analysisType === undefined ? "" : analysisType.name,
                nameCaption: "Analysis Type",
                value1: jointTypeIDA === undefined ? "" : jointTypeIDA.name,
                caption1: "Joint Type",
                value2: jointTypeIDB === undefined ? "" : jointTypeIDB.name,
                caption2: "Joint Type",
                value3: jointTypeIDC === undefined ? "" : jointTypeIDC.name,
                caption3: "Joint Type",
                modified: true
            });
        });
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
        this._stateManager.AddAnalysisType.trigger({
            analysisType: {
                analysisTypeID: data.analysisTypeID,
                jointTypeIDA: data.jointTypeIDA,
                jointTypeIDB: data.jointTypeIDB,
                jointTypeIDC: data.jointTypeIDC
            }
        });
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
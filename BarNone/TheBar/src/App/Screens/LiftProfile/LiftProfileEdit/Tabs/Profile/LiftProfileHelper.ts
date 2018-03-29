import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView";
import { StateManager } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { LiftProfileStateManager, LiftProfileState } from "App/Screens/LiftProfile/LiftProfileEdit/Tabs/Profile/LiftProfileStateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import LiftEditView from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditView";
import UEye from "UEye/UEye";
import LiftProfileDialogScreen from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogScreen";
import { LiftProfileDialogState } from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogStateManager";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";

export default class LiftProfileHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftProfileStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftProfileState, original: LiftProfileState) {
        console.log(original, current);
        
        this.view.profileList.items = current.profiles.map(p => {
            let analysisType = this._stateManager
                .s_AnalysisTypeList.find(t => t.id === p.analysisTypeID);

            return BaseListItem.create<DataListItem>({
                id: 1,
                name: analysisType === undefined ? "" : analysisType.name
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
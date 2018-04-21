import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Permission from "App/Data/Models/Lift/Permission";
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import { LiftVideoStateManager, LiftVideoState } from "App/Screens/Lifts/LiftEdit/Tabs/Video/LiftVideoStateManager";
import { LiftPermissionState } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionStateManager";
import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import { SkeletonBuilder } from "App/Screens/Lifts/LiftEdit/SkeletonBuilder";
import StringUtils from "UEye/Core/StringUtils";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";
import { AnalysisTypeEnum } from "App/Data/Models/Analysis/AnalysisTypeEnum";

export default class LiftVideoHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftVideoStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftVideoState, original: LiftVideoState) {
        console.log(current);
        // let bd = new BodyExample();

        var data = SkeletonBuilder.build(
            current.bodyData,
            this.view.player.canvasHeight,
            this.view.player.canvasWidth
        );
        this.view.player.frameData = data;

        this.view.player.src = StringUtils.format("{0}Lift/{1}/Video?access_token={2}", //https://www.rmp-streaming.com/media/bbb-360p.mp4",
            BaseDataManager.resourceAddress,
            current.liftID,
            BaseDataManager.auth.access_token);
        console.log(this.view.player.src);

        if (current.analysisProfile !== undefined && current.analysisProfile.details) {
            let analysisListItem: DataListItem[] = [];

            if (current.analysisProfile.details.accelerationAnalysisCriteria !== undefined) {
                analysisListItem = analysisListItem.concat(
                    current.analysisProfile.details
                        .accelerationAnalysisCriteria.map(data => {
                            return <DataListItem>{
                                id: data.id,
                                name: AnalysisTypeEnum.Acceleration.name
                            }
                        })
                );
            }

            if (current.analysisProfile.details.angleAnalysisCriteria !== undefined) {
                analysisListItem = analysisListItem.concat(
                    current.analysisProfile.details
                        .angleAnalysisCriteria.map(data => {
                            return <DataListItem>{
                                id: data.id,
                                name: AnalysisTypeEnum.Angle.name
                            }
                        })
                );
            }

            if (current.analysisProfile.details.speedAnalysisCriteria !== undefined) {
                analysisListItem = analysisListItem.concat(
                    current.analysisProfile.details
                        .speedAnalysisCriteria.map(data => {
                            return <DataListItem>{
                                id: data.id,
                                name: AnalysisTypeEnum.Speed.name
                            }
                        })
                );
            }

            if (current.analysisProfile.details.positionAnalysisCriteria !== undefined) {
                analysisListItem = analysisListItem.concat(
                    current.analysisProfile.details
                        .positionAnalysisCriteria.map(data => {
                            return <DataListItem>{
                                id: data.id,
                                name: AnalysisTypeEnum.Position.name
                            }
                        })
                );
            }
            this.view.analysisList.items = analysisListItem;
        }
    }

    public async onShow(data: { liftID: number, liftTypeID: number }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(LiftVideoStateManager, this.parentStateManager);

        this.view.analyticsButton.onClick = () => this.view.videoLayout.toggleSideBar();

        this._stateManager.bind(this._onRender.bind(this));

        this._stateManager.CreateState.trigger({
            liftID: data.liftID,
            liftTypeID: data.liftTypeID,
            bodyDataID: this.parentStateManager.getCurrentState().bodyDataID
        });
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
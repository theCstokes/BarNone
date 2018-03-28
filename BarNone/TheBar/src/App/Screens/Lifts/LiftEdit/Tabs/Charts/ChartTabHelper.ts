import Screen from "UEye/Screen/Screen"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import { ELiftAnalysisType } from "App/Data/Models/Analysis/AnalysisRequest";
import { EDimension } from "App/Data/Models/Analysis/EDimension";
import { EJointType } from "App/Data/Models/Joint/EJointType";
import { LineData } from "UEye/Elements/Components/Graph/Graph";
import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import { StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { ChartTabStateManager } from "App/Screens/Lifts/LiftEdit/Tabs/Charts/ChartTabStateManager";

export default class ChartTabHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: ChartTabStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender() {
        var ts = this._stateManager.getCurrentState().timeSeries;
        if (ts != null && ts.t != null && ts.y != null) {
            var lineData: LineData[] = [];
            for (var n = 0; n < ts.t.length; n++) {
                var v: LineData = { x: ts.t[n], y: ts.y[n] };
                lineData.push(v);
            }
            this.view.chart.data = lineData;
            this.view.chart.draw = true;
        }
    }

    public async onShow(data: { liftID: number }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(ChartTabStateManager, this.parentStateManager);

        this.view.jointDropdown.items =
            this._stateManager.JointTypes.map((type) => {
                var x: { name: string, id: number } = { name: EJointType[type], id: type as number };
                return x;
            });

        this.view.analysisTypeDropdown.items =
            this._stateManager.LiftAnalysisTypes.map((type) => {
                var x: { name: string, id: number } = { name: ELiftAnalysisType[type], id: type as number };
                return x;
            });

        this.view.dimensionDropdown.items =
            this._stateManager.Dimensions.map((type) => {
                var x: { name: string, id: number } = { name: EDimension[type], id: type as number };
                return x;
            });

        this.view.dimensionDropdown.onSelect = (d) => {
            this._stateManager.DimensionChanged.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this.view.analysisTypeDropdown.onSelect = (d) => {
            this._stateManager.AnalysisTypeChange.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this.view.jointDropdown.onSelect = (d) => {
            this._stateManager.JointTypeChanged.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this._stateManager.bind(this._onRender.bind(this));

        this._stateManager.CreateState.trigger({ liftID: data.liftID });
    }


}
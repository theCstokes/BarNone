import Screen from "UEye/Screen/Screen"
import { ChartTabStateManager } from "App/Screens/Lifts/ChartTab/ChartTabStateManager";
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import { ChartTab } from "App/Screens/Lifts/ChartTab/ChartTab";
import { ELiftAnalysisType } from "App/Data/Models/Analysis/AnalysisRequest";
import { EDimension } from "App/Data/Models/Analysis/EDimension";
import { EJointType } from "App/Data/Models/Joint/EJointType";
import { LineData } from "UEye/Elements/Components/Graph/Graph";



export default class ChartTabHelper {
    private _stateManager: ChartTabStateManager;
    private _view: LiftEditView;

    constructor(view : LiftEditView, id : number){
        this._view = view;
        this._stateManager = new ChartTabStateManager(id);
    }

    private _onRender(){
        var ts = this._stateManager.getCurrentState().timeSeries;
        if (ts != null && ts.t != null && ts.y != null){
            var lineData : LineData[] = [];
            for(var n = 0; n <ts.t.length; n++){
                var v : LineData = {x:ts.t[n],y:ts.y[n]};
                lineData.push(v);
            }
            this._view.chart.data = lineData;
            this._view.chart.draw = true;
        }
    }

    public onShow(data?: any){
        this._view.jointDropdown.items =
            this._stateManager.JointTypes.map((type) => {
                var x : {name:string,id:number} =  {name:EJointType[type], id: type as number};
                return x;
            });
        this._view.analysisTypeDropdown.items = 
            this._stateManager.LiftAnalysisTypes.map((type) => {
                var x : {name:string,id:number} =  {name:ELiftAnalysisType[type], id: type as number};
                return x;
            });
        
        this._view.dimensionDropdown.items = 
            this._stateManager.Dimensions.map((type) => {
                var x : {name:string,id:number} =  {name:EDimension[type], id: type as number};
                return x;
            });

        this._view.dimensionDropdown.onSelect = (d) =>{
            this._stateManager.DimensionChanged.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this._view.analysisTypeDropdown.onSelect = (d) =>{
            this._stateManager.AnalysisTypeChange.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this._view.jointDropdown.onSelect = (d) =>{
            this._stateManager.JointTypeChanged.trigger(d.id);
            this._stateManager.UpdateTimeSeries.trigger();
        };

        this._stateManager.bind(this._onRender.bind(this));
    }

    
}
import Screen from "UEye/Screen/Screen"
import { ChartTabStateManager } from "App/Screens/Lifts/ChartTab/ChartTabStateManager";
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import { ChartTab } from "App/Screens/Lifts/ChartTab/ChartTab";
import { ELiftAnalysisType } from "App/Data/Models/Analysis/AnalysisRequest";
import { EDimension } from "App/Data/Models/Analysis/EDimension";



export default class ChartTabHelper {
    private _stateManager: ChartTabStateManager;
    private _view: LiftEditView;

    constructor(view : LiftEditView, id : number){
        this._view = view;
        this._stateManager = new ChartTabStateManager();
    }

    private _onRender(){

    }

    public onShow(data?: any){
        this._view.jointDropdown.items = this._stateManager.JointTypes ;
        this._view.analysisTypeDropdown.items = 
            this._stateManager.LiftAnalysisTypes.map((type) => {
                var x : {name:string,id:number} =  {name:ELiftAnalysisType[type], id: type as number};
                return x;
            }) ;
        
        this._view.dimensionDropdown.items = 
            this._stateManager.Dimensions.map((type) => {
                var x : {name:string,id:number} =  {name:EDimension[type], id: type as number};
                return x;
            }) ;
    }

    
}
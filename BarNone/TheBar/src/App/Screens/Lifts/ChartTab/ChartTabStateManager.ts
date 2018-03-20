// import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { OnClickCallback, OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import StateBind from "UEye/StateManager/StateBind";
import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State, StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import JointType from "App/Data/Models/Joint/JointType";
import { Resource } from "UEye/Data/Resource";
import DataManager from "App/Data/DataManager";
import AnalysisRequest, { ELiftAnalysisType, RequestEntity, AnalysisRequestPosition, AnalysisRequestVelocity } from "App/Data/Models/Analysis/AnalysisRequest";
import { EDimension } from "App/Data/Models/Analysis/EDimension";
import { EJointType } from "App/Data/Models/Joint/EJointType";
import AnalysisResult from "App/Data/Models/Analysis/AnalysisResult";
import UEye from "UEye/UEye";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

// enum LiftAnalysisTypeEnum{
//     Angle = "Angle",
//     Position = "Position",
//     Velocity = "Velocity"
// }

class TimeSeries{
    public y: number[];
    public t: number[];
}

export class ChartTabState {
    public selectedAnalysisType: ELiftAnalysisType;
    public selectedJoint: EJointType;
    public selectedDimension: EDimension;
    public timeSeries: TimeSeries;
    public liftID: string;
}

export class ChartTabStateManager extends BaseStateManager<ChartTabState> {
    public constructor() {
        super(ChartTabState);
    }

    public LiftAnalysisTypes = [ELiftAnalysisType.Position,ELiftAnalysisType.Velocity,ELiftAnalysisType.Angle];
    public JointTypes = ["A","B","C"];
    public Dimensions = [EDimension.X,EDimension.Y,EDimension.Z];

    public async onInitialize(): Promise<void> { 	}


    public readonly AnalysisTypeChange = StateBind
        .onAction<ChartTabState, ELiftAnalysisType>(this, (state,data) => {
            var nextState = Utils.clone(state);
            nextState.current.selectedAnalysisType = data;
            this.UpdateTimeSeries.trigger();
            return nextState;
        });

    public readonly JointTypeChanged = StateBind
        .onAction<ChartTabState, EJointType>(this,(state,data) =>{
            var nextState = Utils.clone(state);
            nextState.current.selectedJoint = data;
            this.UpdateTimeSeries.trigger();
            return nextState;
        })

    public readonly UpdateTimeSeries = StateBind
        .onAsyncCallable<ChartTabState>(this, async (state) => {
            var ar : AnalysisRequest = new AnalysisRequest();
            var re : RequestEntity = new RequestEntity();
            var nextState = Utils.clone(state);
            
            switch(state.current.selectedAnalysisType){
                case ELiftAnalysisType.Position:{
                    var pre = new AnalysisRequestPosition();
                    pre.type = state.current.selectedAnalysisType;
                    pre.Dimension = state.current.selectedDimension;
                    pre.Joint = state.current.selectedJoint;
                    re = pre;
                    break;
                }
                case ELiftAnalysisType.Velocity:{
                    var vre = new AnalysisRequestVelocity();
                    vre.type = state.current.selectedAnalysisType;
                    vre.Dimension = state.current.selectedDimension;
                    vre.Joint = state.current.selectedJoint;
                    re = vre;
                    break;
                }
                case ELiftAnalysisType.Angle:{
                    break;
                }
            }
            ar.requests = [re];
            var results: AnalysisResult = await DataManager.AnalysisPipe.resource.param("ID",state.current.liftID).create(ar);
            nextState.current.timeSeries = new TimeSeries();
            nextState.current.timeSeries.t = results.Results[0].value["time"];
            nextState.current.timeSeries.y = results.Results[0].value["data"];
            return nextState;
    });
}
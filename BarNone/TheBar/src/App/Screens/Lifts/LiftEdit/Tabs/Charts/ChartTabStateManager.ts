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

class TimeSeries {
    public y: number[];
    public t: number[];
}

export class ChartTabState {
    public selectedAnalysisType: ELiftAnalysisType;
    public selectedJoint: EJointType;
    public selectedDimension: EDimension;
    public timeSeries: TimeSeries;
    public liftID: number;
}

export class ChartTabStateManager extends ChildStateManager<ChartTabState, State> {
    public constructor(parentStateManager: BaseStateManager<State>) {
        super(
            parentStateManager,
            ChartTabState,
            false,
            (state: State) => state.chartState,
            (state: State, data: ChartTabState) => state.chartState = data
        );

        this.JointTypes = [];
        //build the joint types array.
        for (var num = 0; num < 25; num++) {
            this.JointTypes.push(num);
        }
    }

    public LiftAnalysisTypes = [ELiftAnalysisType.Position, ELiftAnalysisType.Velocity, ELiftAnalysisType.Angle];
    public JointTypes: EJointType[];
    public Dimensions = [EDimension.X, EDimension.Y, EDimension.Z];

    public async onInitialize(): Promise<void> { }

    public readonly CreateState = StateBind
        .onAction<ChartTabState, {
            liftID: number
        }>(this, (state, data) => {
            var nextState = state.empty();
            nextState.current.liftID = data.liftID;
            return nextState.initialize();
        })

    public readonly AnalysisTypeChange = StateBind
        .onAction<ChartTabState, ELiftAnalysisType>(this, (state, data) => {
            var nextState = Utils.clone(state);
            nextState.current.selectedAnalysisType = data;
            return nextState;
        });

    public readonly JointTypeChanged = StateBind
        .onAction<ChartTabState, EJointType>(this, (state, data) => {
            var nextState = Utils.clone(state);
            nextState.current.selectedJoint = data;
            return nextState;
        })

    public readonly DimensionChanged = StateBind
        .onAction<ChartTabState, EDimension>(this, (state, data) => {
            var nextState = Utils.clone(state);
            nextState.current.selectedDimension = data;
            return nextState;
        })

    public readonly UpdateTimeSeries = StateBind
        .onAsyncCallable<ChartTabState>(this, async (state) => {
            if (state.current.selectedAnalysisType == null || state.current.selectedDimension == null || state.current.selectedJoint == null) {
                return state;
            }

            var ar: AnalysisRequest = new AnalysisRequest();
            var re: RequestEntity = new RequestEntity();
            var nextState = Utils.clone(state);

            switch (state.current.selectedAnalysisType) {
                case ELiftAnalysisType.Position: {
                    var pre = new AnalysisRequestPosition();
                    pre.Type = state.current.selectedAnalysisType;
                    pre.Dimension = state.current.selectedDimension;
                    pre.Joint = state.current.selectedJoint;
                    re = pre;
                    break;
                }
                case ELiftAnalysisType.Velocity: {
                    var vre = new AnalysisRequestVelocity();
                    vre.Type = state.current.selectedAnalysisType;
                    vre.Dimension = state.current.selectedDimension;
                    vre.Joint = state.current.selectedJoint;
                    re = vre;
                    break;
                }
                case ELiftAnalysisType.Angle: {
                    break;
                }
            }
            ar.requests = [re];
            var results = await DataManager.AnalysisPipe.resource.param("ID", state.current.liftID.toString()).create(ar);
            console.log(results);
            nextState.current.timeSeries = new TimeSeries();
            nextState.current.timeSeries.t = results.results[0].value["time"];
            nextState.current.timeSeries.y = results.results[0].value["data"];
            return nextState;
        });
}
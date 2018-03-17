// import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { OnClickCallback, OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import StateBind from "UEye/StateManager/StateBind";
import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State, StateManager } from "App/Screens/Nav/StateManager";
import JointType from "App/Data/Models/Joint/JointType";
import { Resource } from "UEye/Data/Resource";
import DataManager from "App/Data/DataManager";

enum LiftAnalysisTypeEnum{
    Angle = "Angle",
    Position = "Position",
    Velocity = "Velocity"
}

class TimeSeries{
    public y: number[];
    public t: number[];
}

export class ChartTabState {
    public selectedAnalysisType: LiftAnalysisTypeEnum;
    public selectedJoint: JointType;
    public timeSeries: TimeSeries;
}

export class ChartTabStateManager extends ChildStateManager<ChartTabState, State> {
    public constructor(parentStateManager: StateManager) {
        super(parentStateManager, ContextState,
            (state: State) => state.context,
            (state: State, data: ContextState) => state.context = data);
    }

    public async onInitialize(): Promise<void> { 	}

    public readonly AnalysisTypeChange = StateBind
        .onAction<ChartTabState, LiftAnalysisTypeEnum>(this, (state,data) => {
            var nextState = Utils.clone(state);
            nextState.current.selectedAnalysisType = data;
            return nextState;
        });

    public readonly JointTypeChanged = StateBind
        .onAction<ChartTabState, JointType>(this,(state,data) =>{
            var nextState = Utils.clone(state);
            nextState.current.selectedJoint = data;
            return nextState;
        })

    public readonly AddSubBreadcrumb = StateBind
        .onCallable<ChartTabState>(this, (state) => {
            
            return nextState;
        });

    public readonly PopSubBreadcrumbTo = StateBind
        .onAction<ContextState, IListItem>(this, (state, data) => {
            var nextState = Utils.clone(state);

            var targetIdx: number | undefined = undefined;

            // Remove all crumbs after the target.
            nextState.current.crumbList =
                nextState.current.crumbList.filter((crumb, idx) => {
                    if (crumb.id === data.id) {
                        targetIdx = idx;
                        return true;
                    }
                    return !(targetIdx !== undefined && idx > targetIdx);
                });

            return nextState;
        });

    public readonly ToggleShowHelp = StateBind
        .onCallable<ContextState>(this, (state) => {
            var nextState = Utils.clone(state);

            nextState.current.showNotifications = false;
            nextState.current.showHelp = (!state.current.showHelp);

            return nextState;
        });

    public readonly ToggleShowNotifications = StateBind
        .onCallable<ContextState>(this, (state)  => {
            var nextState = Utils.clone(state);
            
            nextState.current.showHelp = false;
            nextState.current.showNotifications = !state.current.showNotifications;

            return nextState;
        });
}
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
//import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import StateBind from "UEye/StateManager/StateBind";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";
import User from "App/Data/Models/User/User";
import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State } from "App/Screens/Lifts/LiftEdit/StateManager";
import Permission from "App/Data/Models/Lift/Permission";
import BodyData from "App/Data/Models/BodyData/BodyData";
import LiftAnalysisProfile from "App/Data/Models/Lift/LiftAnalysisProfile";

export class LiftVideoState {
    public liftID: number;
    public bodyDataID: number;
    public bodyData: BodyData;
    public analysisProfile: LiftAnalysisProfile | undefined;
}

export class LiftVideoStateManager extends ChildStateManager<LiftVideoState, State> {

    public constructor(parentStateManager: BaseStateManager<State>) {
        super(
            parentStateManager,
            LiftVideoState,
            true,
            (state: State) => state.liftVideoState,
            (state: State, data: LiftVideoState) => state.liftVideoState = data
        );

        this.trackChildChangesFrom(this.CreateState);
    }

    public async onInitialize(): Promise<void> {

    }

    public readonly CreateState = StateBind
        .onAsyncAction<LiftVideoState, {
            liftID: number,
            liftTypeID: number,
            bodyDataID: number
        }>(this, async (state, data) => {
            let nextState = state.empty();

            nextState.current.liftID = data.liftID;
            nextState.current.bodyDataID = data.bodyDataID;
            nextState.current.bodyData = await DataManager
                .BodyData.single(data.bodyDataID, { includeDetails: true });

            let profile = (await DataManager
                .LiftAnalysisProfile.all({
                    filter: {
                        property: (p) => p.liftTypeID,
                        comparisons: "eq",
                        value: data.liftTypeID
                    }
                })).first();

            if (profile !== undefined && typeof(profile.id) === "number") {
                nextState.current.analysisProfile = await DataManager
                    .LiftAnalysisProfile.single(profile.id, {
                        includeDetails: true
                    });
            }

            return nextState.initialize();
        });
}

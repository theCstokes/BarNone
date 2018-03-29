import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import JointType from "App/Data/Models/Joint/JointType";
import DataManager from "App/Data/DataManager";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";

export class ProfileAnalysisType {
    public analysisTypeID: number;
    public jointTypeIDA?: number
    public jointTypeIDB?: number
    public jointTypeIDC?: number
}

export class LiftProfileState {
    public liftProfileID: number;
    public profiles: ProfileAnalysisType[] = [];
}

export class LiftProfileStateManager extends ChildStateManager<LiftProfileState, State> {

    public s_JointTypeList: JointType[];
	public s_AnalysisTypeList: AnalysisType[];

    public constructor(parentStateManager: BaseStateManager<State>) {
        super(
            parentStateManager,
            LiftProfileState,
            true,
            (state: State) => state.liftProfileState,
            (state: State, data: LiftProfileState) => state.liftProfileState = data
        );

        this.trackChildChangesFrom(this.CreateState);
    }

    public async onInitialize(): Promise<void> {
        this.s_JointTypeList = await DataManager.JointTypes.all();
		this.s_AnalysisTypeList = await DataManager.AnalysisTypes.all();
    }

    public readonly CreateState = StateBind
        .onAsyncAction<LiftProfileState, {
            liftProfileID: number
        }>(this, async (state, data) => {
            let nextState = state.empty();

            nextState.current.liftProfileID = data.liftProfileID;
            // nextState.current.permissions = await DataManager
            //     .Permission.resource
            //     .param("liftID", data.liftID.toString())
            //     .all();

            return nextState.initialize();
        });

    public readonly AddAnalysisType = StateBind
        .onAction<LiftProfileState, {
            analysisType: ProfileAnalysisType
        }>(this, (state, data) => {
           let nextState = Utils.clone(state);
           
           nextState.current.profiles.push(data.analysisType);

           return nextState;
        });
}

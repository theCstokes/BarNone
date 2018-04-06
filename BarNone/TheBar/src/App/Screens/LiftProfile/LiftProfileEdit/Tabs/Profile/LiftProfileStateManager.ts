import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State } from "App/Screens/LiftProfile/LiftProfileEdit/StateManager";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import JointType from "App/Data/Models/Joint/JointType";
import DataManager from "App/Data/DataManager";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import AccelerationAnalysisCriteria from "App/Data/Models/Lift/Analysis/AccelerationAnalysisCriteria";
import SpeedAnalysisCriteria from "App/Data/Models/Lift/Analysis/SpeedAnalysisCriteria";
import PositionAnalysisCriteria from "App/Data/Models/Lift/Analysis/PositionAnalysisCriteria";
import AngleAnalysisCriteria from "App/Data/Models/Lift/Analysis/AngleAnalysisCriteria";

// export class ProfileAnalysisType {
//     public analysisTypeID: number;
//     public jointTypeIDA?: number
//     public jointTypeIDB?: number
//     public jointTypeIDC?: number
// }

export class LiftProfileState {
    public liftProfileID: number;
    public accelerationCriteriaList: AccelerationAnalysisCriteria[] = [];
    public speedCriteriaList: SpeedAnalysisCriteria[] = [];
    public positionCriteriaList: PositionAnalysisCriteria[] = [];
    public angleCriteriaList: AngleAnalysisCriteria[] = [];
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

            let liftType = await DataManager.LiftAnalysisProfile.single(
                data.liftProfileID,
                {
                    includeDetails: true
                });

            if (liftType.details !== undefined
                && liftType.details.accelerationAnalysisCriteria !== undefined) {
                nextState.current.accelerationCriteriaList =
                    liftType.details.accelerationAnalysisCriteria;
            }

            if (liftType.details !== undefined
                && liftType.details.angleAnalysisCriteria !== undefined) {
                nextState.current.angleCriteriaList =
                    liftType.details.angleAnalysisCriteria;
            }

            if (liftType.details !== undefined
                && liftType.details.positionAnalysisCriteria !== undefined) {
                nextState.current.positionCriteriaList =
                    liftType.details.positionAnalysisCriteria;
            }

            if (liftType.details !== undefined
                && liftType.details.speedAnalysisCriteria !== undefined) {
                nextState.current.speedCriteriaList =
                    liftType.details.speedAnalysisCriteria;
            }

            return nextState.initialize();
        });

    public readonly AddAccelerationCriteria = StateBind
        .onAction<LiftProfileState, {
            jointTypeID: number
        }>(this, (state, data) => {
            let nextState = Utils.clone(state);
            nextState.current.accelerationCriteriaList.push({
                id: Utils.guid(),
                jointTypeID: data.jointTypeID,
                isNew: true
            });
            return nextState;
        });

    public readonly AddSpeedCriteria = StateBind
        .onAction<LiftProfileState, {
            jointTypeID: number
        }>(this, (state, data) => {
            let nextState = Utils.clone(state);
            nextState.current.speedCriteriaList.push({
                id: Utils.guid(),
                jointTypeID: data.jointTypeID,
                isNew: true
            });
            return nextState;
        });

    public readonly AddPositionCriteria = StateBind
        .onAction<LiftProfileState, {
            jointTypeID: number
        }>(this, (state, data) => {
            let nextState = Utils.clone(state);
            nextState.current.positionCriteriaList.push({
                id: Utils.guid(),
                jointTypeID: data.jointTypeID,
                isNew: true
            });
            return nextState;
        });

    public readonly AddAngleCriteria = StateBind
        .onAction<LiftProfileState, {
            jointTypeAID: number,
            jointTypeBID: number,
            jointTypeCID: number
        }>(this, (state, data) => {
            let nextState = Utils.clone(state);
            nextState.current.angleCriteriaList.push({
                id: Utils.guid(),
                jointTypeAID: data.jointTypeAID,
                jointTypeBID: data.jointTypeBID,
                jointTypeCID: data.jointTypeCID,
                isNew: true
            });
            return nextState;
        });
}

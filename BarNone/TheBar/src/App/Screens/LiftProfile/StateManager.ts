import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
//import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import StateBind from "UEye/StateManager/StateBind";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import LiftAnalysisProfile from "App/Data/Models/Lift/LiftAnalysisProfile";
import LiftType from "App/Data/Models/Lift/LiftType";

export class State implements ISelectionState<LiftAnalysisProfile> {
    public selectionList: LiftAnalysisProfile[];
    public name: string;
    public selectionId: number | string;
    public parentID: number | null;
}

export class StateManager extends SelectionStateManager<LiftAnalysisProfile, State> {
    public s_LiftTypeList: LiftType[];

    public constructor() {
        super(State);
    }

    public async onInitialize(): Promise<void> {
        this.s_LiftTypeList = await DataManager.LiftTypes.all();
    }

    protected async listProvider(): Promise<LiftAnalysisProfile[]> {
        return await DataManager.LiftAnalysisProfile.all();
    }


    public readonly LiftProfileAdd = StateBind
        .onAsyncAction<State, {
            liftTypeID: number
        }>(this, async (state, data) => {
            let nextState = Utils.clone(state);

            let profile = await DataManager.LiftAnalysisProfile.create({
                liftTypeID: data.liftTypeID
            });

            nextState.current.selectionList.push({
                id: profile.id,
                liftTypeID: data.liftTypeID
            });

            nextState.current.selectionId = profile.id;

            return nextState;
        });
}
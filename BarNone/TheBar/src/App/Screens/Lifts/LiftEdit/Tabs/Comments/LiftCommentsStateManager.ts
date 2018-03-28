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
import LiftComment from "App/Data/Models/Comment/LiftComment";

export class LiftCommentsState {
    public liftID: number;
    public comments: LiftComment[] = [];
}

export class LiftCommentsStateManager extends ChildStateManager<LiftCommentsState, State> {

    public constructor(parentStateManager: BaseStateManager<State>) {
        super(
            parentStateManager,
            LiftCommentsState,
            false,
            (state: State) => state.liftCommentsState,
            (state: State, data: LiftCommentsState) => state.liftCommentsState = data
        );
    }

    public async onInitialize(): Promise<void> {
    }

    public readonly CreateState = StateBind
        .onAsyncAction<LiftCommentsState, {
            liftID: number
        }>(this, async (state, data) => {
            let nextState = state.empty();

            nextState.current.liftID = data.liftID;
            
            nextState.current.comments = await DataManager.LiftComments.all({
				params: {
					liftID: data.liftID
				}
            });
            
            // nextState.current.permissions = await DataManager
            //     .Permission.resource
            //     .param("liftID", data.liftID.toString())
            //     .all();

            return nextState.initialize();
        });

    public readonly RefreshComments = StateBind
        .onAsyncCallable<LiftCommentsState>(this, async (state) => {
            var nextState = Utils.clone(state);

            nextState.current.comments = await DataManager.LiftComments.all({
                params: {
                    liftID: nextState.current.liftID
                }
            });

            return nextState;
        });

    // public readonly AddUserPermission = StateBind
    //     .onAction<LiftPermissionState, {
    //         userID: number
    //     }>(this, (state, data) => {
    //         let nextState = Utils.clone(state);

    //         nextState.current.permissions.push({
    //             id: Utils.guid(),
    //             liftID: nextState.current.liftID,
    //             userID: data.userID
    //         });

    //         return nextState;
    //     });

}

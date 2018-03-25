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

export class LiftPermissionState  {
    public liftID: number;
    public permissions: Permission[] = [];
}

export class LiftPermissionStateManager extends ChildStateManager<LiftPermissionState, State> {
    public s_UserList: User[];
    public constructor(parentStateManager: BaseStateManager<State>) {
        super(
            parentStateManager,
            LiftPermissionState,
            true,
            (state: State) => state.liftPermissionState,
            (state: State, data: LiftPermissionState) => state.liftPermissionState = data
        );
        
        this.s_UserList = [];
    }
    public async onInitialize(): Promise<void> {
        this.s_UserList = await DataManager.Users.all();
    }

    public readonly CreateState = StateBind
		.onAsyncCallable<LiftPermissionState>(this, async (state) => {
            let nextState = state.empty();
			return nextState.initialize();
        });
        
    public readonly AddUserPermission = StateBind
        .onAction<LiftPermissionState, {
            userID: number
        }>(this, (state, data) => {
            let nextState = Utils.clone(state);
            
            nextState.current.permissions.push({
                id: Utils.guid(),
                liftID: nextState.current.liftID,
                userID: data.userID
            });

            return nextState;
        });

}

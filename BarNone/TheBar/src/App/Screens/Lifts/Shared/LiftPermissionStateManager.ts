import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
//import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import StateBind from "UEye/StateManager/StateBind";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";
import User from "../../../Data/Models/User/User";

export class State  {
   
}

export class LiftPermissionStateManager extends BaseStateManager<State> {
    public s_UserList: User[];
    public constructor() {
        super(State);
        this.s_UserList = [];
        this.onInitialize();
    }
    public async onInitialize(): Promise<void> {
		this.s_UserList = await DataManager.Users.all();
    }

    public readonly CreateState = StateBind
		.onCallable<State>(this, (state) => {
			return state;
		});

}

import {LiftPermissionStateManager} from "App/Screens/Lifts/Shared/LiftPermissionStateManager"
import {LiftPermissionTab} from "App/Screens/Lifts/Shared/LiftPermissionView"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";

export default class LiftPermissionHelper {
    private _stateManager: LiftPermissionStateManager;
    private _view: LiftEditView;
    
    constructor(view: LiftEditView, id: number){
        this._view = view;
        
    }

    private _onRender(){
        console.log("On render being called")
        console.log(this._view.userShareSearch.items);
    }
    
    public async onShow(data?: any):Promise<void>{
        this._stateManager =await StateManagerFactory.create(LiftPermissionStateManager);
        //this._stateManager.CreateState.trigger();
        this._view.userShareSearch.items = this._stateManager.s_UserList;
    }


}
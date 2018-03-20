import {LiftPermissionStateManager} from "App/Screens/Lifts/Shared/LiftPermissionStateManager"
import {LiftPermissionTab} from "App/Screens/Lifts/Shared/LiftPermissionView"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";

export default class LiftPermissionHelper {
    private _stateManager: LiftPermissionStateManager;
    private _view: LiftEditView;
    
    constructor(view: LiftEditView, id: number){
        this._view = view;
        this._stateManager = new LiftPermissionStateManager();
    }

    private _onRender(){
       
    }
    
    public onShow(data?: any){
        this._view.userShareSearchBar.items = 
            this._stateManager.s_UserList.map((user) => {
                var x :{title: string, id: number|string }
                    ={title: user.name, id:user.id};
                return x;
        });
        console.log(this._view.userShareSearchBar.items);
    }

}
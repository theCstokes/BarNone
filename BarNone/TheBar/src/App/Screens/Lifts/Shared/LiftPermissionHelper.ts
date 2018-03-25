import { LiftPermissionStateManager, LiftPermissionState } from "App/Screens/Lifts/Shared/LiftPermissionStateManager"
import { LiftPermissionTab } from "App/Screens/Lifts/Shared/LiftPermissionView"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Permission from "App/Data/Models/Lift/Permission";

export default class LiftPermissionHelper {
    private _parentStateManager: StateManager;
    private _stateManager: LiftPermissionStateManager;
    private _view: LiftEditView;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        this._view = view;
        this._parentStateManager = parentStateManager;
    }

    private _onRender(current: LiftPermissionState, original: LiftPermissionState) {
        console.log("On render being called")
        console.log(this._view.userShareSearch.items);

        this._view.userShareList.items = current.permissions.reduce((result, permission) => {
            let user = this._stateManager.s_UserList.find(user => user.id === permission.userID);
            if (user === undefined) return result;
            result.push({
                id: permission.id,
                name: user.name
            });
            return result;
        }, new Array<IListItem>());
    }

    public async onShow(data?: { liftID: number, permissions: Permission[] }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(LiftPermissionStateManager, this._parentStateManager);

        this._stateManager.bind(this._onRender.bind(this));

        this._view.userShareSearch.items = this._stateManager.s_UserList;
        this._view.userShareSearch.onSelect = (data) => {
            this._stateManager.AddUserPermission.trigger({ userID: data.id })
        }

        this._stateManager.CreateState.trigger({
            // liftID: data.liftID,
            // permissions: data.permissions
        });
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
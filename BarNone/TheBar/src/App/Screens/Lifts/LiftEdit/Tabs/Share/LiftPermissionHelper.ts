import { LiftPermissionStateManager, LiftPermissionState } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionStateManager"
import { LiftPermissionTab } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionView"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Permission from "App/Data/Models/Lift/Permission";
import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";

export default class LiftPermissionHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftPermissionStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftPermissionState, original: LiftPermissionState) {
        console.log("On render being called")
        console.log(this.view.userShareSearch.items);

        this.view.userShareList.items = current.permissions.reduce((result, permission) => {
            let user = this._stateManager.s_UserList.find(user => user.id === permission.userID);
            if (user === undefined) return result;
            result.push({
                id: permission.id,
                name: user.name
            });
            return result;
        }, new Array<IListItem>());

        this.view.userShareSearch.items = this._stateManager.s_UserList
            .filter(user => current.permissions.find(p => p.userID === user.id) === undefined);
    }

    public async onShow(data: { liftID: number }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(LiftPermissionStateManager, this.parentStateManager);

        this._stateManager.bindToParent(this._onRender.bind(this));
        this._stateManager.bind(this._onRender.bind(this));

        this.view.userShareSearch.items = this._stateManager.s_UserList;
        this.view.userShareSearch.onSelect = (data) => {
            this._stateManager.AddUserPermission.trigger({ userID: data.id })
        }

        this._stateManager.CreateState.trigger({
            liftID: data.liftID,
            // permissions: data.permissions
        });
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
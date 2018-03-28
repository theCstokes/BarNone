import { LiftPermissionStateManager, LiftPermissionState } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionStateManager"
import { LiftPermissionTab } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionView"
import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { StateManager } from "App/Screens/Lifts/LiftEdit/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import Permission from "App/Data/Models/Lift/Permission";
import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import { LiftCommentsStateManager, LiftCommentsState } from "App/Screens/Lifts/LiftEdit/Tabs/Comments/LiftCommentsStateManager";
import NotificationManager from "UEye/NotificationManager";
import NotificationRequestDTO from "App/Data/Models/NotificationRequestDTO";
import LiftComment from "App/Data/Models/Comment/LiftComment";
import DataManager from "App/Data/DataManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";

export default class LiftCommentsHelper extends ScreenSection<LiftEditView, StateManager> {
    private _stateManager: LiftCommentsStateManager;

    constructor(view: LiftEditView, parentStateManager: StateManager) {
        super(view, parentStateManager);
    }

    private _onRender(current: LiftCommentsState, original: LiftCommentsState) {
        console.log(current);

        this.view.messenger.messages = current.comments.map(comment => {
            return {
                id: comment.id,
                value: comment.text,
                userName: (comment.sentUserID === BaseDataManager.auth.userID) ? "You" : "Other",
                date: comment.timeSent,
                isCurrentUser: (comment.sentUserID === BaseDataManager.auth.userID)
            }
        });
    }

    public async onShow(data: { liftID: number }): Promise<void> {
        this._stateManager = await StateManagerFactory
            .create(LiftCommentsStateManager, this.parentStateManager);

        NotificationManager.addListener<LiftComment>(new NotificationRequestDTO<LiftComment>({
            type: "Comment",
            filter: {
                property: (comment) => comment.liftID,
                comparisons: "eq",
                value: data.liftID
            }
        }), async () => {
            console.log("GoT");
            await this._stateManager.RefreshComments.trigger();
        });

        this.view.messenger.onSend = (msg: string) => {
            DataManager.Comments.create({
                liftID: this._stateManager.getCurrentState().liftID,
                text: msg,
                timeSent: "2018-02-04"
            });
        };

        this._stateManager.bind(this._onRender.bind(this));

        this._stateManager.CreateState.trigger({ liftID: data.liftID });
    }

    public async onSave(): Promise<void> {
        this._stateManager.save();
    }
}
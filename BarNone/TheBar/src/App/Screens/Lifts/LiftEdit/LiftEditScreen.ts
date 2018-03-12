import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Lifts/LiftEdit/StateManager";
import { SkeletonBuilder } from "App/Screens/Lifts/LiftEdit/SkeletonBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";
import NotificationManager from "UEye/NotificationManager";
import DataManager from "App/Data/DataManager";
import { ELiftType } from "App/Screens/Lifts/StateManager";
import NotificationRequestDTO from "App/Data/Models/NotificationRequestDTO";
import Comment from "App/Data/Models/Comment/Comment";

export default class LiftEditScreen extends EditScreen<LiftEditView, StateManager> {
	public constructor() {
		super(LiftEditView);
	}

	private _onRender(current: State, original: State) {
		console.log(current);
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		// this.view.player.src = StringUtils.format("https://www.rmp-streaming.com/media/bbb-360p.mp4",
		// 	current.lift.id,
		// 	BaseDataManager.auth.access_token);

		this.view.typeDropDown.selected = current.liftType;
		this.view.parentDropDown.selected = this.stateManager
			.s_FolderList.find(f => f.id === current.parentID);

		this.view.player.frameData = SkeletonBuilder.build(current.bodyData);

		this.view.messenger.messages = current.comments.map(comment => {
			return {
				id: comment.id,
				value: comment.text,
				userName: (comment.sentUserID === BaseDataManager.auth.userID) ? "You" : "Other",
				date: comment.timeSent,
				isCurrentUser: (comment.sentUserID === BaseDataManager.auth.userID)
			}
		});

		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}

	public async onShow(data: { id: number, name: string, type: ELiftType }): Promise<void> {
		this.init(new StateManager(data.type));
		this.stateManager.bind(this._onRender.bind(this));
		await this.stateManager.Create.trigger();

		this.view.typeDropDown.items = this.stateManager.s_LiftTypeList;
		this.view.parentDropDown.items = this.stateManager.s_FolderList;

		NotificationManager.addListener<Comment>(new NotificationRequestDTO<Comment>({
			type: "Comment",
			filter: {
				property: (comment) => comment.liftID,
				comparisons: "eq",
				value: this.stateManager.getCurrentState().id
			}
		}), async () => {
			await this.stateManager.RefreshComments.trigger();
		});

		this.view.messenger.onSend = (msg: string) => {
			DataManager.Comments.create({
				liftID: this.stateManager.getCurrentState().id,
				text: msg,
				timeSent: "2018-02-04"
			});
		};

		this.view.analyticsButton.onClick = () => this.view.videoLayout.toggleSideBar();

		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};

		await this.stateManager.ResetState.trigger({ id: data.id, name: data.name });

		// this.view.player.play();
	}

	// public nameBind = ScreenBind
	// 	.create<State>(this, "nameInput")
	// 	.onChange(data => {
	// 		this.stateManager.nameChange.trigger(data);
	// 	})
	// 	.onRender((original, current) => {
	// 		this.view.nameInput.text = current.name;
	// 		this.view.nameInput.modified = (original.name !== current.name);
	// 	});

	// public panelBind = ScreenBind
	// 	.create<State>(this, "editPanel")
	// 	.onRender((original, current) => {
	// 		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
	// 		this.view.editPanel.modified = isModified;
	// 		// this.isDirty = isModified;
	// 	});

	// public onShow(data: any): void {
	// 	console.log(data);
	// 	// this.stateManager.resetState.trigger(data);
	// }

	public save(): void {

	}
}
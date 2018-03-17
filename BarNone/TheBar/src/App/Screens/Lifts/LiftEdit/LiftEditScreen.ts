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
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import BodyExample from "App/Data/DataOverride/api/v1/Joints"
import BodyData from "../../../Data/Models/BodyData/BodyData";

export default class LiftEditScreen extends EditScreen<LiftEditView, StateManager> {
	public constructor() {
		super(LiftEditView);
	}

	private _pipeLine = ScreenPipeLine.create()
	//#region Panel
	.onRender((current: State, original: State) => {
		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	})
	//#endregion

	//#region Name Input
	.onShow(() => {
		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};
	})
	.onRender((current: State, original: State) => {
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);
	})
	//#endregion

	//#region Type Drop Down
	.onShow(() => {
		this.view.typeDropDown.items = this.stateManager.s_LiftTypeList
	})
	.onRender((current: State, original: State) => {
		this.view.typeDropDown.selected = current.liftType;
		this.view.typeDropDown.modified =
			(JSON.stringify(current.liftType) !== JSON.stringify(original.liftType));
	})
	//#endregion
	
	//#region Parent Drop Down
	.onShow(() => {
		this.view.typeDropDown.items = this.stateManager.s_LiftTypeList;
		this.view.parentDropDown.items = this.stateManager.s_FolderList;

		this.view.parentDropDown.onSelect = (item) => {
			this.stateManager.ParentChange.trigger({ parentID: item.id });
		};
	})
	.onRender((current: State, original: State) => {
		var currentParent = this.stateManager.s_FolderList.find(f => f.id === current.parentID);
		var originalParent = this.stateManager.s_FolderList.find(f => f.id === original.parentID);
		this.view.parentDropDown.selected = currentParent;
		this.view.parentDropDown.modified =
			(JSON.stringify(currentParent) !== JSON.stringify(originalParent));
	})
	//#endregion

	//#region Video
	.onShow(() => {
		this.view.analyticsButton.onClick = () => this.view.videoLayout.toggleSideBar();
	})
	.onRender((current: State, original: State) => {
		let bd= new BodyExample();
		this.view.player.frameData = SkeletonBuilder.build(bd.data[0]);
	})
	//#endregion
	
	//#region Massager
	.onShow(() => {
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
	})
	.onRender((current: State, original: State) => {
		this.view.messenger.messages = current.comments.map(comment => {
			return {
				id: comment.id,
				value: comment.text,
				userName: (comment.sentUserID === BaseDataManager.auth.userID) ? "You" : "Other",
				date: comment.timeSent,
				isCurrentUser: (comment.sentUserID === BaseDataManager.auth.userID)
			}
		});
	})
	//#endregion

	public async onShow(data: { id: number, name: string, type: ELiftType }): Promise<void> {
		super.onShow(data);
		this.init(await StateManagerFactory.create(StateManager));
		this._pipeLine.onShowInvokable();
		this.stateManager.bind(this._pipeLine.onRenderInvokable.bind(this));		
		await this.stateManager.ResetState.trigger(data);
	}



	// private _onRender(current: State, original: State) {
	// 	// console.log(current);
	// 	// this.view.nameInput.text = current.name;
	// 	// this.view.nameInput.modified = (original.name !== current.name);

	// 	// this.view.player.src = StringUtils.format("https://www.rmp-streaming.com/media/bbb-360p.mp4",
	// 	// 	current.lift.id,
	// 	// 	BaseDataManager.auth.access_token);

	// 	// this.view.typeDropDown.selected = current.liftType;
	// 	// this.view.typeDropDown.modified =
	// 	// 	(JSON.stringify(current.liftType) !== JSON.stringify(original.liftType));

	// 	// var currentParent = this.stateManager.s_FolderList.find(f => f.id === current.parentID);
	// 	// var originalParent = this.stateManager.s_FolderList.find(f => f.id === original.parentID);
	// 	// this.view.parentDropDown.selected = currentParent;
	// 	// this.view.parentDropDown.modified =
	// 	// 	(JSON.stringify(currentParent) !== JSON.stringify(originalParent));

	// 	// this.view.player.frameData = SkeletonBuilder.build(current.bodyData);

	// 	// this.view.messenger.messages = current.comments.map(comment => {
	// 	// 	return {
	// 	// 		id: comment.id,
	// 	// 		value: comment.text,
	// 	// 		userName: (comment.sentUserID === BaseDataManager.auth.userID) ? "You" : "Other",
	// 	// 		date: comment.timeSent,
	// 	// 		isCurrentUser: (comment.sentUserID === BaseDataManager.auth.userID)
	// 	// 	}
	// 	// });

	// 	// var isModified = (JSON.stringify(original) !== JSON.stringify(current));
	// 	// this.view.editPanel.modified = isModified;
	// }

	// public async onShow(data: { id: number, name: string, type: ELiftType }): Promise<void> {
	// 	this.init(new StateManager(data.type));
	// 	this.stateManager.bind(this._onRender.bind(this));
	// 	await this.stateManager.Create.trigger();

		

		

		

	// 	await this.stateManager.ResetState.trigger({ id: data.id, name: data.name });

	// 	// this.view.player.play();
	// }

	public save(): void {

	}
}
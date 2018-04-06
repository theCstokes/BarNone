import LiftEditView from "App/Screens/Lifts/LiftEdit/LiftEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Lifts/LiftEdit/StateManager";
import { SkeletonBuilder } from "App/Screens/Lifts/LiftEdit/SkeletonBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";
import NotificationManager from "UEye/NotificationManager";
import DataManager from "App/Data/DataManager";
import NotificationRequestDTO from "App/Data/Models/NotificationRequestDTO";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { LiftFolderHelp } from "App/Help/Lifts/LiftFolderEdit/helpDemo";
import { ELiftType } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";
import BodyExample from "App/Data/DataOverride/api/v1/Joints"
import LiftPermissionHelper from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionHelper";
import LiftVideoHelper from "App/Screens/Lifts/LiftEdit/Tabs/Video/LiftVideoHelper";
import ChartTabHelper from "App/Screens/Lifts/LiftEdit/Tabs/Charts/ChartTabHelper";
import LiftCommentsHelper from "App/Screens/Lifts/LiftEdit/Tabs/Comments/LiftCommentsHelper";

export default class LiftEditScreen extends EditScreen<LiftEditView, StateManager> {
	private _chartTabHelper: ChartTabHelper;
	private _liftpermissionHelper: LiftPermissionHelper;

	public constructor() {
		super(LiftEditView, LiftFolderHelp);
	}

	public isModified(current: State, original: State) {
		return !Utils.equivalent(original, current, ["comments"]);
	}

	private _pipeLine = ScreenPipeLine.create()
		//#region Panel
		// .onRender((current: State, original: State) => {
		// 	// var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		// 	var isModified = !Utils.compare(original, current, ["comments"]);
		// 	this.view.editPanel.modified = isModified;
		// })
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

	// //#region Video
	// .onShow(() => {
	// 	this.view.analyticsButton.onClick = () => this.view.videoLayout.toggleSideBar();
	// })
	// .onRender((current: State, original: State) => {
	// 	let bd = new BodyExample();

	// 	var data = SkeletonBuilder.build(
	// 		current.bodyData,
	// 		this.view.player.canvasHeight,
	// 		this.view.player.canvasWidth
	// 	);
	// 	this.view.player.frameData = data;

	// 	this.view.player.src = StringUtils.format("{0}Lift/{1}/Video?access_token={2}", //https://www.rmp-streaming.com/media/bbb-360p.mp4",
	// 		BaseDataManager.resourceAddress,
	// 		current.id,
	// 		BaseDataManager.auth.access_token);
	// 	console.log(this.view.player.src);
	// })
	// //#endregion

	// //#region Massager
	// .onShow((data: { id: number, name: string, type: ELiftType }) => {
	// 	NotificationManager.addListener<Comment>(new NotificationRequestDTO<Comment>({
	// 		type: "Comment",
	// 		filter: {
	// 			property: (comment) => comment.liftID,
	// 			comparisons: "eq",
	// 			value: data.id
	// 		}
	// 	}), async () => {
	// 		console.log("GoT");
	// 		await this.stateManager.RefreshComments.trigger();
	// 	});

	// 	this.view.messenger.onSend = (msg: string) => {
	// 		DataManager.Comments.create({
	// 			liftID: this.stateManager.getCurrentState().id,
	// 			text: msg,
	// 			timeSent: "2018-02-04"
	// 		});
	// 	};
	// })
	// .onRender((current: State, original: State) => {
	// 	this.view.messenger.messages = current.comments.map(comment => {
	// 		return {
	// 			id: comment.id,
	// 			value: comment.text,
	// 			userName: (comment.sentUserID === BaseDataManager.auth.userID) ? "You" : "Other",
	// 			date: comment.timeSent,
	// 			isCurrentUser: (comment.sentUserID === BaseDataManager.auth.userID)
	// 		}
	// 	});
	// })
	////#endregion

	public async onShow(data: { id: number, name: string, type: ELiftType }): Promise<void> {

		this.init(await StateManagerFactory.create(StateManager));
		this._pipeLine.onShowInvokable(data);
		this.stateManager.bind(this._pipeLine.onRenderInvokable.bind(this));
		await this.stateManager.ResetState.trigger(data);

		this.bindSections(
			ChartTabHelper,
			LiftPermissionHelper,
			LiftVideoHelper,
			LiftCommentsHelper
		);

		// this._chartTabHelper = new ChartTabHelper(this.view, data.id);
		// this._liftpermissionHelper = new LiftPermissionHelper(this.view, this.stateManager);
		// this._liftpermissionHelper.onShow();
		// this._chartTabHelper.onShow();

		// let  = new LiftPermissionHelper(this.view, this.stateManager);


		super.onShow({
			liftID: data.id,
			liftTypeID: this.stateManager.getCurrentState().liftType.id
		});
	}
}
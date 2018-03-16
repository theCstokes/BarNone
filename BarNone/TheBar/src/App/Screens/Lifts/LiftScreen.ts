import Screen from "UEye/Screen/Screen"
import LiftView from "App/Screens/Lifts/LiftView";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";
import { StateManager, State, ELiftType } from "App/Screens/Lifts/StateManager";
import EditScreen from "UEye/Screen/EditScreen";
import { LiftListType, LiftListItem } from "App/Screens/Lifts/Models";
import LiftFolderEditScreen from "App/Screens/Lifts/LiftFolderEdit/LiftFolderEditScreen";
import LiftEditScreen from "App/Screens/Lifts/LiftEdit/LiftEditScreen";
import App from "App/App";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftFolderHelp } from "App/Help/Lifts/LiftFolderEdit/helpDemo";
import NotificationManager from "UEye/NotificationManager";

export default class LiftScreen extends Screen<LiftView> {
	// private subScreen: LiftEditScreen;
	private subScreen: EditScreen<any, any>
	private _stateManager: StateManager;
	private _type: ELiftType;

	public static ParentChange: DataEvent<LiftListItem>;
	public static LiftChange: DataEvent<Lift>;
	
	public constructor() {
		super(LiftView, LiftFolderHelp);

		LiftScreen.ParentChange = new DataEvent();
		LiftScreen.ParentChange.on(this._onFolderOpenHandler.bind(this));

		LiftScreen.LiftChange = new DataEvent();
		LiftScreen.LiftChange.on(this._onListOpenHandler.bind(this));
	}

	private _onRender(current: State, original: State): void {
		this.view.liftList.items = current.selectionList.map(item => {
			return {
				selected: (item.id === current.selectionId),
				id: item.id,
				name: item.name,
				icon: (item.type === LiftListType.Folder) ? "fa-folder" : "fa-universal-access",
				onOpen: () => {
					console.log(item);
					if (item.type === LiftListType.Folder) {
						this._onFolderOpenHandler(item);
					}
				}
			}
		});
		this.view.liftListInfo.visible = (current.selectionList.length < 0);

		var userData = current.selectionList.find(item => {
			return (item.id === current.selectionId);
		});

		if (userData !== undefined) {
			if (this.subScreen !== undefined) {
				UEye.popTo(this);
			}
			// this.view.mainPanel.content=this.
			if (userData.type === LiftListType.Lift) {
				this.subScreen = UEye.push(LiftEditScreen, {
					id: userData.id,
					name: userData.name,
					type: this._type
				}) as LiftEditScreen;
			} else if (userData.type === LiftListType.Folder) {
				this.subScreen = UEye.push(LiftFolderEditScreen, {
					id: userData.id,
					name: userData.name,
					type: this._type
				}) as LiftFolderEditScreen;
			}

			// this.subScreen.stateManager.ResetState.trigger({
			// 	id: userData.id,
			// 	name: userData.name,
			// 	age: 0
			// });
		}
	}

	public onShow(type: ELiftType): void {
		this._type = type;
		this._stateManager = new StateManager(type);
		this._stateManager.bind(this._onRender.bind(this));

		this.view.liftList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id });
		};

		// this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
		this._stateManager.ResetState.trigger();
	}

	private _onFolderOpenHandler(item: LiftListItem) {
		App.Navigation.AddSubBreadcrumb.trigger({
			id: Utils.guid(),
			value: item.name,
			onClick: (crumb) => {
				this._stateManager.ParentChange.trigger({ parentID: item.id });
				App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
			}
		});

		this._stateManager.ParentChange.trigger({ parentID: item.id });
	}

	private _onListOpenHandler(item: Lift) {
		if (item.details.parent !== undefined) {
			App.Navigation.AddSubBreadcrumb.trigger({
				id: Utils.guid(),
				value: item.details.parent.name,
				onClick: (crumb) => {
					this._stateManager.ParentChange.trigger({ parentID: item.id });
					App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
				}
			});
		}

		this._stateManager.ParentChange.trigger({ 
			parentID: item.parentID,
			selectionId: item.id
		});
	}

	// public userListBind = ScreenBind
	// 	.create<State>(this, "userList")
	// 	.onSelect(async data => {
	// 		// console.log(data);
	// 		// UEye.popTo(this);
	// 		this.stateManager.selectionChange.trigger({
	// 			id: data.id
	// 		});
	// 	})
	// 	.onRender(async(original, current) => {
	// 		this.view.userList.items = current.selectionList.map(item => {
	// 			return {
	// 				selected: (item.id === current.selectionId),
	// 				id: item.id,
	// 				name: item.name
	// 			}
	// 		});

	// 		var userData = current.selectionList.find(item => {
	// 			return (item.id === current.selectionId);
	// 		});
	// 		this.subScreen.stateManager.resetState.trigger(userData);
	// 		// await UEye.push(UserEditScreen, userData);
	// 	});

	// public async onShow() {
	// 	this.subScreen = await UEye.push(LiftEditScreen) as LiftEditScreen;
	// 	this.stateManager.init();

	// 	this.subScreen.stateManager.saveEvent.on(() => {
	// 		this.stateManager.init();
	// 	});

	// 	this.subScreen.cancelEvent.on(() => {
	// 		var current = this.stateManager.getCurrentState();
	// 		var userData = current.selectionList.find(item => {
	// 			return (item.id === current.selectionId);
	// 		});
	// 		this.subScreen.stateManager.resetState.trigger(userData);
	// 	});
		
	// 	// UEye.push(UserEditScreen);
	// }
}
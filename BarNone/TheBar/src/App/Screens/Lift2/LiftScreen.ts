import Screen from "UEye/Screen/Screen"
import LiftView from "App/Screens/Lift2/LiftView";
// import LiftEditScreen from "App/Screens/Lift2/Edit/LiftEditScreen";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";
import { StateManager, State } from "App/Screens/Lift2/StateManager";
import EditScreen from "UEye/Screen/EditScreen";
import { LiftListType, LiftListItem } from "App/Screens/Lift2/Models";
import LiftFolderEditScreen from "App/Screens/Lift2/LiftFolderEdit/LiftFolderEditScreen";
import LiftEditScreen from "App/Screens/Lift2/LiftEdit/LiftEditScreen";
import App from "App/App";

export default class LiftScreen extends Screen<LiftView> {
	// private subScreen: LiftEditScreen;
	private subScreen: EditScreen<any, any>
	private _stateManager: StateManager;
	
	public constructor() {
		super(LiftView);

		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));
	}

	private _onRender(current: State, original: State): void {
		this.view.userList.items = current.selectionList.map(item => {
			return {
				selected: (item.id === current.selectionId),
				id: item.id,
				name: item.name,
				icon: (item.type === LiftListType.Folder) ? "fa-folder-o" : "fa-universal-access",
				onOpen: () => {
					console.log(item);
					if (item.type === LiftListType.Folder) {
						this._onFolderOpenHandler(item);
					}
				}
			}
		});

		var userData = current.selectionList.find(item => {
			return (item.id === current.selectionId);
		});

		if (userData !== undefined) {
			if (this.subScreen !== undefined) {
				UEye.popTo(this);
			}

			if (userData.type === LiftListType.Lift) {
				this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
			} else if (userData.type === LiftListType.Folder) {
				this.subScreen = UEye.push(LiftFolderEditScreen) as LiftFolderEditScreen;
			}

			this.subScreen.stateManager.ResetState.trigger({
				id: userData.id,
				name: userData.name,
				age: 0
			});
		}
	}

	public onShow(): void {
		this.view.userList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id });
		};

		// this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
		this._stateManager.init();
	}

	private _onFolderOpenHandler(item: LiftListItem) {
		App.Navigation.AddSubBreadcrumb.trigger({
			value: item.name,
			onClick: () => {
				this._stateManager.ParentChange.trigger({ parentID: item.parentID });
			}
		});
		
		// App.breadcrumbs.push({
		// 	value: item.name,
		// 	onClick: () => {
		// 		this._stateManager.ParentChange.trigger({ parentID: item.parentID });
		// 	}
		// });

		this._stateManager.ParentChange.trigger({ parentID: item.id });
	}

	private _onListOpenHandler(parentID: number) {
		this._stateManager.ParentChange.trigger({ parentID: parentID });
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
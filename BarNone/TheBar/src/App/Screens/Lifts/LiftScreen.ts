// import BasicScreen from "Application/Core/BasicScreen";
// import UEye from "UEye/UEye";
// import LiftView from "Application/Screens/Lifts/LiftView";
// import LiftEditScreen from "Application/Screens/Lifts/Edit/LiftEditScreen";
// import { StateManager, State } from "Application/Screens/Lifts/StateManager";
// import ScreenBind from "UEye/Screen/ScreenBind";

import Screen from "UEye/Screen/Screen"
import LiftView from "App/Screens/Lifts/LiftView";
import LiftEditScreen from "App/Screens/Lifts/Edit/LiftEditScreen";
import { StateManager, State } from "App/Screens/Lifts/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";

export default class LiftScreen extends Screen<LiftView> {
	private subScreen: LiftEditScreen;
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
				name: item.name
			}
		});

		var userData = current.selectionList.find(item => {
			return (item.id === current.selectionId);
		});
		if (userData !== undefined) {
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

		this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
		this._stateManager.init();
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
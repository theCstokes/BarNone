// import BasicScreen from "Application/Core/BasicScreen";
// import UEye from "UEye/UEye";
// import LiftView from "Application/Screens/Lifts/LiftView";
// import LiftEditScreen from "Application/Screens/Lifts/Edit/LiftEditScreen";
// import { StateManager, State } from "Application/Screens/Lifts/StateManager";
// import ScreenBind from "UEye/Screen/ScreenBind";

import Screen from "UEye/Screen/Screen"
import SettingsView from "App/Screens/Settings/SettingsView";
import SettingsEditScreen from "App/Screens/Settings/Edit/SettingsEditScreen";
import { StateManager, State } from "App/Screens/Settings/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";

export default class SettingsScreen extends Screen<SettingsView> {
	private subScreen: SettingsEditScreen;
	private _stateManager: StateManager;
	
	public constructor() {
		super(SettingsView);

		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));
	}

	private _onRender(current: State, original: State): void {
		this.view.userList.items = current.SettingsElementList.map(item => {
			return {
				selected: (item.id === current.selectionId),
				id: item.id,
				name: item.name
			}
		});

	
	}

	public onShow(): void {
		this.view.userList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id });
		};

		this.subScreen = UEye.push(SettingsEditScreen) as SettingsEditScreen;
		this._stateManager.ResetState.trigger();
		//this._stateManager.init();
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
import UserView from "App/Screens/User/UserView";
import Screen from "UEye/Screen/Screen";
import UserEditScreen from "App/Screens/User/Edit/UserEditScreen";
import { StateManager, State } from "App/Screens/User/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";

export default class UserScreen extends Screen<UserView> {
	private subScreen: UserEditScreen;
	private _stateManager: StateManager;
	
	public constructor() {
		super(UserView);
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

	public onShow() {
		this.view.userList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id })
		};

		this.subScreen = UEye.push(UserEditScreen) as UserEditScreen;
		this._stateManager.init();

		// this.subScreen.stateManager.saveEvent.on(() => {
		// 	this.stateManager.init();
		// });

		// this.subScreen.cancelEvent.on(() => {
		// 	var current = this.stateManager.getCurrentState();
		// 	var userData = current.selectionList.find(item => {
		// 		return (item.id === current.selectionId);
		// 	});
		// 	this.subScreen.stateManager.resetState.trigger(userData);
		// });
		
		// this._stateManager.ResetState.trigger();

		// UEye.push(UserEditScreen);
	}
}
import AppScreen from "Vee/Screen/AppScreen";
import Vee from "Vee/Vee";
import UserView from "Application/Screens/User/UserView";
import UserEditScreen from "Application/Screens/User/Edit/UserEditScreen";
import DataManager from "Application/Data/DataManager";
import ScreenBind from "Vee/Screen/ScreenBind";
import { State, StateManager } from "Application/Screens/User/StateManager";

export default class UserScreen extends AppScreen {
	private _stateManager: StateManager;
	
	public constructor() {
		super(UserView);
		this._stateManager = new StateManager(this);
	}

	public userListBind = ScreenBind
		.create<State>(this, "userList")
		.onRender((original, current) => {
			this.view.userList.items = current.userList.map(item => {
				return {
					selected: (item.id === current.currentId),
					name: item.name
				}
			});
		});

	public async onShow() {
		this._stateManager.init();
		Vee.push(UserEditScreen);
	}
}
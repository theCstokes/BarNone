import BasicScreen from "Application/Core/BasicScreen";
import Vee from "Vee/Vee";
import UserView from "Application/Screens/User/UserView";
import UserEditScreen from "Application/Screens/User/Edit/UserEditScreen";
import DataManager from "Application/Data/DataManager";
import ScreenBind from "Vee/Screen/ScreenBind";
import { State, StateManager } from "Application/Screens/User/StateManager";

export default class UserScreen extends BasicScreen<StateManager> {
	
	public constructor() {
		super(UserView, StateManager);
	}

	public userListBind = ScreenBind
		.create<State>(this, "userList")
		.onSelect(async data => {
			// console.log(data);
			Vee.pop();
			this.stateManager.selectionChange.trigger({
				id: data.id
			});
		})
		.onRender(async(original, current) => {
			this.view.userList.items = current.userList.map(item => {
				return {
					selected: (item.id === current.currentId),
					id: item.id,
					name: item.name
				}
			});

			var userData = current.userList.find(item => {
				return (item.id === current.currentId);
			});
			await Vee.push(UserEditScreen, userData);
		});

	public async onShow() {
		this.stateManager.init();
		// Vee.push(UserEditScreen);
	}
}
import BasicScreen from "Application/Core/BasicScreen";
import Vee from "Vee/Vee";
import UserView from "Application/Screens/User/UserView";
import UserEditScreen from "Application/Screens/User/Edit/UserEditScreen";
import DataManager from "Application/Data/DataManager";
import ScreenBind from "Vee/Screen/ScreenBind";
import { State, StateManager } from "Application/Screens/User/StateManager";
import EditScreen from "Application/Core/EditScreen";

export default class UserScreen extends BasicScreen<StateManager> {
	private subScreen: UserEditScreen;
	
	public constructor() {
		super(UserView, StateManager);
	}

	public userListBind = ScreenBind
		.create<State>(this, "userList")
		.onSelect(async data => {
			// console.log(data);
			// Vee.popTo(this);
			this.stateManager.selectionChange.trigger({
				id: data.id
			});
		})
		.onRender(async(original, current) => {
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
			this.subScreen.stateManager.resetState.trigger(userData);
			// await Vee.push(UserEditScreen, userData);
		});

	public async onShow() {
		this.subScreen = await Vee.push(UserEditScreen) as UserEditScreen;
		this.stateManager.init();

		this.subScreen.stateManager.saveEvent.on(() => {
			this.stateManager.init();
		});

		this.subScreen.cancelEvent.on(() => {
			var current = this.stateManager.getCurrentState();
			var userData = current.selectionList.find(item => {
				return (item.id === current.selectionId);
			});
			this.subScreen.stateManager.resetState.trigger(userData);
		});
		
		// Vee.push(UserEditScreen);
	}
}
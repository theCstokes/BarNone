import AppScreen from "Vee/Screen/AppScreen";
import Vee from "Vee/Vee";
import UserView from "Application/Screens/User/UserView";
import UserEditScreen from "Application/Screens/User/Edit/UserEditScreen";
import DataManager from "Application/Data/DataManager";

export default class UserScreen extends AppScreen {
	public constructor() {
		super(UserView);
	}

	public async onShow() {
		Vee.push(UserEditScreen);

		console.log(await DataManager.Users.load());
	}
}
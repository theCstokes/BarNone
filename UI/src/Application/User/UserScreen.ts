import AppScreen from "Vee/Screen/AppScreen";
import UserView from "Application/User/UserView";
import Vee from "Vee/Vee";
import UserEditScreen from "Application/User/Edit/UserEditScreen";

export default class UserScreen extends AppScreen {
	public constructor() {
		super(UserView);
	}

	public onShow() {
		Vee.push(UserEditScreen);
	}
}
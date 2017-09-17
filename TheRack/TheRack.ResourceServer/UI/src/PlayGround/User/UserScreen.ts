import AppScreen from "Vee/Screen/AppScreen";
import UserView from "PlayGround/User/UserView";
import Vee from "Vee/Vee";
import UserEditScreen from "PlayGround/User/Edit/UserEditScreen";

export default class UserScreen extends AppScreen {
	public constructor() {
		super(UserView);
	}

	public onShow() {
		Vee.push(UserEditScreen);
	}
}
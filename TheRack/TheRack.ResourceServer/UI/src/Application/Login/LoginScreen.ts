import AppScreen from "Vee/Screen/AppScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import LoginView from "Application/Login/LoginView";
import Vee from "Vee/Vee";
import NavScreen from "Application/Nav/NavScreen";
import UserScreen from "Application/User/UserScreen";
import DataManager from "Vee/DataManager";
// import { StateManager, State } from "Application/User/Edit/StateManager";

export default class LoginScreen extends AppScreen {
	// private _stateManager: StateManager;

	public constructor() {
		super(LoginView);
		// this._stateManager = new StateManager(this);
	}

	public loginBind = ScreenBind
		.create(this, "loginButton")
		.onClick(async () => {

			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				Vee.pop();
				await Vee.push(NavScreen);
				await Vee.push(UserScreen);
			}
		});
}
import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import Vee from "Vee/Vee";
import DataManager from "Application/Data/DataManager";
import LoginView from "Application/Screens/Login/LoginView";
import NavScreen from "Application/Screens/Nav/NavScreen";
import UserScreen from "Application/Screens/User/UserScreen";
import { StateManager } from "Application/Screens/User/Edit/StateManager";

export default class LoginScreen extends BasicScreen<StateManager> {
	// private _stateManager: StateManager;

	public constructor() {
		super(LoginView, StateManager);
		// this._stateManager = new StateManager(this);
	}

	public loginBind = ScreenBind
		.create(this, "loginButton")
		.onClick(async () => {

			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				Vee.pop();
				await Vee.push(NavScreen);
				// await Vee.push(UserScreen);
			}
		});

	// public onShow(data?: any): void {
	// 	throw new Error("Method not implemented.");
	// }
}
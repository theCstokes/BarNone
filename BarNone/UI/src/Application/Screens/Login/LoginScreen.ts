import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "UEye/Screen/ScreenBind";
import UEye from "UEye/UEye";
import DataManager from "Application/Data/DataManager";
import LoginView from "Application/Screens/Login/LoginView";
import NavScreen from "Application/Screens/Nav/NavScreen";
import UserScreen from "Application/Screens/User/UserScreen";
import { StateManager } from "Application/Screens/User/Edit/StateManager";
import CreateAccountScreen from "Application/Screens/Login/Create/CreateAccountScreen";

export default class LoginScreen extends BasicScreen<StateManager> {
	// private _stateManager: StateManager;

	public constructor() {
		super(LoginView, StateManager);
		// this._stateManager = new StateManager(this);
	}

	public createAccountBind = ScreenBind
		.create(this, "createButton")
		.onClick(async () => {
			UEye.pop();
			await UEye.push(CreateAccountScreen);
		});

	public loginBind = ScreenBind
		.create(this, "loginButton")
		.onClick(async () => {

			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				UEye.pop();
				await UEye.push(NavScreen);
				// await UEye.push(UserScreen);
			}
		});

	// public onShow(data?: any): void {
	// 	throw new Error("Method not implemented.");
	// }
}
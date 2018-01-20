// import BasicScreen from "Application/Core/BasicScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import UEye from "UEye/UEye";
// import DataManager from "Application/Data/DataManager";
// import LoginView from "Application/Screens/Login/LoginView";
// import NavScreen from "Application/Screens/Nav/NavScreen";
// import UserScreen from "Application/Screens/User/UserScreen";
// import { StateManager } from "Application/Screens/User/Edit/StateManager";
// import CreateAccountScreen from "Application/Screens/Login/Create/CreateAccountScreen";

import Screen from "UEye/Screen/Screen";
import LoginView from "App/Screens/Login/LoginView";
import UEye from "UEye/UEye";
import CreateAccountScreen from "App/Screens/Login/Create/CreateAccountScreen";
import DataManager from "App/Data/DataManager";
import NavScreen from "App/Screens/Nav/NavScreen";
/**
 *  Represents Login Screen Component
 */
export default class LoginScreen extends Screen<LoginView> {

 /** Constructor intialized Screen Component and binds corresponding View 
     * */
	public constructor() {
		super(LoginView);
		// this._stateManager = new StateManager(this);
	}
		/** Method defines UI properties when shown
     * */
	public onShow(): void {
		// TODO - remove hard code login.
		this.view.usernameInput.text = "admin";
		this.view.passwordInput.text = "admin";


		this.view.createButton.onClick = () => {
			UEye.pop();
			UEye.push(CreateAccountScreen);
		};

		this.view.loginButton.onClick = async () => {
			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				this.view.statusLabel.text = "Success!";
				UEye.pop();
				await UEye.push(NavScreen);
				// await UEye.push(UserScreen);
			}
			this.view.statusLabel.text = "Password or Username incorrect.";
		};
	}

	// private _onRender(current: any, original: any) {

	// }

	// public createAccountBind = ScreenBind
	// 	.create(this, "createButton")
	// 	.onClick(async () => {
	// 		UEye.pop();
	// 		await UEye.push(CreateAccountScreen);
	// 	});

	// public loginBind = ScreenBind
	// 	.create(this, "loginButton")
	// 	.onClick(async () => {

	// 		if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
	// 			UEye.pop();
	// 			await UEye.push(NavScreen);
	// 			// await UEye.push(UserScreen);
	// 		}
	// 	});


}
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

export default class LoginScreen extends Screen<LoginView> {
	// private _stateManager: StateManager;

	public constructor() {
		super(LoginView);
		// this._stateManager = new StateManager(this);
	}

	public onShow(): void {
		this.view.createButton.onClick = () => {
			UEye.pop();
			UEye.push(CreateAccountScreen);
		};

		this.view.loginButton.onClick = async () => {
			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				UEye.pop();
				await UEye.push(NavScreen);
				// await UEye.push(UserScreen);
			}
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
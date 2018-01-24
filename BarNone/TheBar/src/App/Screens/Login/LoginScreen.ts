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
import StringUtils from "UEye/Core/StringUtils";
import { BaseDataManager } from "UEye/Data/BaseDataManager";

export default class LoginScreen extends Screen<LoginView> {
	// private _stateManager: StateManager;

	public constructor() {
		super(LoginView);
		// this._stateManager = new StateManager(this);
	}

	public onShow(): void {
		// TODO - remove hard code login.
		this.view.usernameInput.text = "admin";
		this.view.passwordInput.text = "admin";

		this.view.frame.onDo = async () => {
			this.view.container.visible = !this.view.container.visible;
			this.view.video.visible = !this.view.video.visible;

			if (this.view.video.visible && await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				this.view.video.src = StringUtils.format("http://localhost:58428/api/v1/Lift/EM?access_token={0}",
					BaseDataManager.auth.access_token);
			}
		}


		this.view.createButton.onClick = () => {
			this.view.loginButton.enabled = false;

			UEye.pop();
			UEye.push(CreateAccountScreen);
		};

		this.view.loginButton.onClick = async () => {
			this.view.loginButton.enabled = false;

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
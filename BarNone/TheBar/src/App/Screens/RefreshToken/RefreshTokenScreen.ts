import DialogScreen from "UEye/Screen/DialogScreen";
import RefreshTokenView from "App/Screens/RefreshToken/RefreshTokenView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LoginScreen from "App/Screens/Login/LoginScreen";

export default class RefreshTokenScreen extends DialogScreen<RefreshTokenView, any, any> {
	public constructor() {
		super(RefreshTokenView);
	}

	public onShow(): void {
		super.onShow();
		this.view.acceptButton.enabled = true;
		
		// TODO - remove hard code login.
		this.view.usernameInput.text = "admin";
		this.view.passwordInput.text = "admin";

		this.view.cancelButton.onClick = () => {
			DataManager.logout();
			UEye.popAll();
			UEye.push(LoginScreen);
		};

		this.onAccept = async () => {
			this.view.acceptButton.enabled = false;

			if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
				this.view.statusLabel.text = "Success!";
				UEye.pop();
			}
			this.view.statusLabel.text = "Password or Username incorrect.";
		};

		// this.view.loginButton.onClick = async () => {
		// 	this.view.loginButton.enabled = false;

		// 	if (await DataManager.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
		// 		this.view.statusLabel.text = "Success!";
		// 		UEye.pop();
		// 	}
		// 	this.view.statusLabel.text = "Password or Username incorrect.";
		// };
	}
}
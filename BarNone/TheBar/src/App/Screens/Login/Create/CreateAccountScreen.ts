import Screen from "UEye/Screen/Screen";
import CreateAccountView from "App/Screens/Login/Create/CreateAccountView";
import { StateManager, State } from "App/Screens/Login/Create/StateManager";
import UEye from "UEye/UEye";
import LoginScreen from "App/Screens/Login/LoginScreen";
import NavScreen from "App/Screens/Nav/NavScreen";
import DataManager from "App/Data/DataManager";
/**
 *  Represents Create Account Screen Component
 */
export default class CreateAccountScreen extends Screen<CreateAccountView> {
	private _stateManager: StateManager;
	 /** Constructor intialized Create Account Component and binds corresponding View 
     * */
	public constructor() {
		super(CreateAccountView);
		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));
	}
	/** Method renders list items of Navigation
     * */
	public _onRender(current: State, original: State) {
		this.view.usernameInput.text = current.userName;
		this.view.passwordInput1.text = current.password1;
		this.view.passwordInput2.text = current.password2;

		var isValidUserName = (current.userName !== undefined && current.userName.length > 0);
		var isValidPassword1 = (current.password1 !== undefined && current.password1.length > 0);
		var isValidPassword2 = (current.password2 !== undefined && current.password2.length > 0);
		this.view.createButton.enabled = (isValidUserName && isValidPassword1 && isValidPassword2 &&
			current.password1 === current.password2);
	}
	/** Method defines UI properties when shown
     * */
	public onShow(): void {
		this.view.cancelButton.onClick = () => {
			UEye.pop();
			UEye.push(LoginScreen);
		};

		this.view.createButton.onClick = async () => {
			var currentState = this._stateManager.getCurrentState();
			if (await DataManager.create(currentState.userName, currentState.password1)) {
				UEye.pop();
				await UEye.push(NavScreen);
			}
		};

		this.view.usernameInput.onChange = (data) => {
			this._stateManager.UserNameChange.trigger(data);
		};

		this.view.passwordInput1.onChange = (data) => {
			this._stateManager.Password1Change.trigger(data);
		};

		this.view.passwordInput2.onChange = (data) => {
			this._stateManager.Password2Change.trigger(data);
		};
	}

	// public userNameBind = ScreenBind
	// 	.create(this, "usernameInput")
	// 	.onChange(data => {
	// 		this.stateManager.userNameChange.trigger(data);
	// 	})
	// 	.onRender((original: State, current: State) => {
	// 		this.view.usernameInput.text = current.userName
	// 	});

	// public password1Bind = ScreenBind
	// 	.create(this, "password1Input")
	// 	.onChange(data => {
	// 		this.stateManager.password1Change.trigger(data);
	// 	})
	// 	.onRender((original: State, current: State) => {
	// 		this.view.password1Input.text = current.password1
	// 	});

	// public password2Bind = ScreenBind
	// 	.create(this, "password2Input")
	// 	.onChange(data => {
	// 		this.stateManager.password2Change.trigger(data);
	// 	})
	// 	.onRender((original: State, current: State) => {
	// 		this.view.password1Input.text = current.password1
	// 		this.view.password2Input.text = current.password2
	// 	});

	// public createAccountBind = ScreenBind
	// 	.create<State>(this, "cancelButton")
	// 	.onClick(async () => {
	// 		UEye.pop();
	// 		await UEye.push(LoginScreen);
	// 	});

	// public loginBind = ScreenBind
	// 	.create<State>(this, "createButton")
	// 	.onClick(async () => {
	// 		var currentState = this.stateManager.getCurrentState();
	// 		if (await DataManager.create(currentState.userName, currentState.password1)) {
	// 			UEye.pop();
	// 			await UEye.push(NavScreen);
	// 		}
	// 	})
	// 	.onRender((original, current) => {
	// 		var isValidUserName = (current.userName !== undefined && current.userName.length > 0);
	// 		var isValidPassword1 = (current.password1 !== undefined && current.password1.length > 0);
	// 		var isValidPassword2 = (current.password2 !== undefined && current.password2.length > 0);
	// 		this.view.createButton.enabled = (isValidUserName && isValidPassword1 && isValidPassword2 &&
	// 			current.password1 === current.password2);
	// 	});
}
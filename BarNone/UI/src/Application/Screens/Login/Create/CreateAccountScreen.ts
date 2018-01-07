import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "UEye/Screen/ScreenBind";
import UEye from "UEye/UEye";
import DataManager from "Application/Data/DataManager";
import NavScreen from "Application/Screens/Nav/NavScreen";
import CreateAccountView from "Application/Screens/Login/Create/CreateAccountView";
import LoginScreen from "Application/Screens/Login/LoginScreen";
import { StateManager, State } from "Application/Screens/Login/Create/StateManager";

export default class CreateAccountScreen extends BasicScreen<StateManager> {
	// private _stateManager: StateManager;

	public constructor() {
		super(CreateAccountView, StateManager);
		// this._stateManager = new StateManager(this);
	}

	public userNameBind = ScreenBind
		.create(this, "usernameInput")
		.onChange(data => {
			this.stateManager.userNameChange.trigger(data);
		})
		.onRender((original: State, current: State) => {
			this.view.usernameInput.text = current.userName
		});

	public password1Bind = ScreenBind
		.create(this, "password1Input")
		.onChange(data => {
			this.stateManager.password1Change.trigger(data);
		})
		.onRender((original: State, current: State) => {
			this.view.password1Input.text = current.password1
		});

	public password2Bind = ScreenBind
		.create(this, "password2Input")
		.onChange(data => {
			this.stateManager.password2Change.trigger(data);
		})
		.onRender((original: State, current: State) => {
			this.view.password2Input.text = current.password2
		});

	public createAccountBind = ScreenBind
		.create<State>(this, "cancelButton")
		.onClick(async () => {
			UEye.pop();
			await UEye.push(LoginScreen);
		});

	public loginBind = ScreenBind
		.create<State>(this, "createButton")
		.onClick(async () => {
			var currentState = this.stateManager.getCurrentState();
			if (await DataManager.create(currentState.userName, currentState.password1)) {
				UEye.pop();
				await UEye.push(NavScreen);
			}
		})
		.onRender((original, current) => {
			var isValidUserName = (current.userName !== undefined && current.userName.length > 0);
			var isValidPassword1 = (current.password1 !== undefined && current.password1.length > 0);
			var isValidPassword2 = (current.password2 !== undefined && current.password2.length > 0);
			this.view.createButton.enabled = (isValidUserName && isValidPassword1 && isValidPassword2 &&
				current.password1 === current.password2);
		});
}
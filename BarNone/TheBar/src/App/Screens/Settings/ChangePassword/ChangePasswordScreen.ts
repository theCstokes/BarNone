import ChangePasswordView from "App/Screens/Settings/ChangePassword/ChangePasswordView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Settings/ChangePassword/StateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
// import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";

export default class ChangePasswordScreen extends EditScreen<ChangePasswordView, StateManager> {
	public constructor() {
		super(ChangePasswordView);
	}

	private _pipelie = ScreenPipeLine.create()
	//#region Panel
	.onRender((current: State, original: State) => {
		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	})
	//#endregion

	//#region New Password Input
	.onShow(() => {
		this.view.newPassword.onChange = (data) => this.stateManager.NewPassword.trigger(data)
		this.view.retypePassword.onChange = (data) => this.stateManager.RetypePassword.trigger(data)
	})
	.onRender((current: State, original: State) => {
		this.view.newPassword.text = current.newPassword;
		this.view.newPassword.modified = (current.newPassword !== original.newPassword);
	})
	//#endregion

	//#region Retype Password Input
	.onShow(() => {
		this.view.retypePassword.onChange = (data) => this.stateManager.RetypePassword.trigger(data)
	})
	.onRender((current: State, original: State) => {
		this.view.retypePassword.text = current.retypePassword;
		this.view.retypePassword.modified = (current.retypePassword !== original.retypePassword);
	})
	//#endregion
	
	public async onShow(): Promise<void> {
		super.onShow();
		this.init(await StateManagerFactory.create(StateManager));
		this.stateManager.bind(this._pipelie.onRenderInvokable.bind(this));
		this._pipelie.onShowInvokable();
		this.stateManager.CreateState.trigger();
	}

	// public nameBind = ScreenBind
	// 	.create<State>(this, "nameInput")
	// 	.onChange(data => {
	// 		this.stateManager.nameChange.trigger(data);
	// 	})
	// 	.onRender((original, current) => {
	// 		this.view.nameInput.text = current.name;
	// 		this.view.nameInput.modified = (original.name !== current.name);
	// 	});

	// public panelBind = ScreenBind
	// 	.create<State>(this, "editPanel")
	// 	.onRender((original, current) => {
	// 		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
	// 		this.view.editPanel.modified = isModified;
	// 		// this.isDirty = isModified;
	// 	});

	// public onShow(data: any): void {
	// 	console.log(data);
	// 	// this.stateManager.resetState.trigger(data);
	// }

	public save(): void {

	}
}
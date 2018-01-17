import ChangePasswordView from "App/Screens/Settings/ChangePassword/ChangePasswordView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Settings/ChangePassword/StateManager";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
// import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";

export default class ChangePasswordScreen extends EditScreen<ChangePasswordView, StateManager> {
	public constructor() {
		super(ChangePasswordView, StateManager);
		this.stateManager.bind(this._onRender.bind(this));
	}

	private _onRender(current: State, original: State) {
		this.view.currentPassword.modified;
		this.view.newPassword.modified;
		this.view.retypePassword.modified;
		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}
	
	public onShow(): void {
		this.view.currentPassword.onChange = (data) => {
			this.stateManager.CurrentPassword.trigger(data);
		};
		this.view.newPassword.onChange = (data) => {
			this.stateManager.NewPassword.trigger(data);
		};
		this.view.retypePassword.onChange = (data) => {
			this.stateManager.RetypePassword.trigger(data);
		};
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
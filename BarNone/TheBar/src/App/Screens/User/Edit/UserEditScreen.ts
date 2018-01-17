import EditScreen from "UEye/Screen/EditScreen";
import UserEditView from "App/Screens/User/Edit/UserEditView";
import { StateManager, State } from "App/Screens/User/Edit/StateManager";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import { StateManager, State } from "Application/Screens/User/Edit/StateManager";
// import UserEditView from "Application/Screens/User/Edit/UserEditView";

export default class UserEditScreen extends EditScreen<UserEditView, StateManager> {
	public constructor() {
		super(UserEditView, StateManager);
		this.stateManager.bind(this._onRender.bind(this));
	}

	private _onRender(current: State, original: State) {
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
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

	public onShow(): void {
		super.onShow();
		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};
	}

	public save(): void {

	}
}
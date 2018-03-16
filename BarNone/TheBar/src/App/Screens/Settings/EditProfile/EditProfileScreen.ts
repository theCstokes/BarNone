import EditProfileView from "App/Screens/Settings/EditProfile/EditProfileView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Settings/EditProfile/StateManager";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
// import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";

export default class SettingsEditScreen extends EditScreen<EditProfileView, StateManager> {
	public constructor() {
		super(EditProfileView);
	}

	private _onRender(current: State, original: State) {
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}
	
	public async onShow(): Promise<void> {
		super.onShow();
		this.init(await StateManagerFactory.create(StateManager));
		this.stateManager.bind(this._onRender.bind(this));

		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
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
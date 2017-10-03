import EditScreen from "Application/Core/EditScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import { StateManager, State } from "Application/Screens/User/Edit/StateManager";
import UserEditView from "Application/Screens/User/Edit/UserEditView";

export default class UserEditScreen extends EditScreen {
	private _stateManager: StateManager;

	public constructor() {
		super(UserEditView);
		this._stateManager = new StateManager(this);
	}

	public nameBind = ScreenBind
		.create<State>(this, "nameInput")
		.onChange(data => {
			this._stateManager.nameChange.trigger(data);
		})
		.onRender((original, current) => {
			this.view.nameInput.text = current.name;
			this.view.nameInput.modified = (original.name !== current.name);
		});

	public panelBind = ScreenBind
		.create<State>(this, "editPanel")
		.onRender((original, current) => {
			var isModified = (JSON.stringify(original) !== JSON.stringify(current));
			this.view.editPanel.modified = isModified;
			this.isDirty = isModified;
		});

	public onShow(data: any): void {
		console.log(data);
		this._stateManager.resetState.trigger(data);
	}
}
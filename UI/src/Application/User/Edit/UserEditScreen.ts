import AppScreen from "Vee/Screen/AppScreen";
import UserEditView from "Application/User/Edit/UserEditView";
import ScreenBind from "Vee/Screen/ScreenBind";
import { StateManager, State } from "Application/User/Edit/StateManager";

export default class UserEditScreen extends AppScreen {
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
			this.view.editPanel.modified = (JSON.stringify(original) !== JSON.stringify(current));
		});
}
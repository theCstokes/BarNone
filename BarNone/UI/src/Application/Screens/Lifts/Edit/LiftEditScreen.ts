import EditScreen from "Application/Core/EditScreen";
import ScreenBind from "UEye/Screen/ScreenBind";
import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";

export default class LiftEditScreen extends EditScreen<StateManager> {
	public constructor() {
		super(LiftEditView, StateManager);
	}

	public nameBind = ScreenBind
		.create<State>(this, "nameInput")
		.onChange(data => {
			this.stateManager.nameChange.trigger(data);
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
			// this.isDirty = isModified;
		});

	public onShow(data: any): void {
		console.log(data);
		// this.stateManager.resetState.trigger(data);
	}

	public save(): void {
		
	}
}
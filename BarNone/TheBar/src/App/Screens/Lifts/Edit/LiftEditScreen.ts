import LiftEditView from "App/Screens/Lifts/Edit/LiftEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Lifts/Edit/StateManager";
import { SkeletonBuilder } from "App/Screens/Lifts/Edit/SkeletonBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
// import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";
/**
 *  Represents LiftEditScreen class .
 */
export default class LiftEditScreen extends EditScreen<LiftEditView, StateManager> {
	 /** Constructor intialized Screen Component and binds corresponding View and StateManager 
     * */
	public constructor() {
		super(LiftEditView, StateManager);
		this.stateManager.bind(this._onRender.bind(this));
	}
	/** Method renders data corresponding to API requests made in State for LiftsEdit
     * */
	private _onRender(current: State, original: State) {
		console.log(current);
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		this.view.player.src = StringUtils.format("http://localhost:58428/api/v1/Lift/{0}/Video?access_token={1}",
			current.lift.id,
			BaseDataManager.auth.access_token);

		this.view.player.frameData = SkeletonBuilder.build(current.lift.details.bodyData);

		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}
	/** Method Method defines UI properties when shown
     * */
	public onShow(): void {
		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};

		// this.view.player.play();
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
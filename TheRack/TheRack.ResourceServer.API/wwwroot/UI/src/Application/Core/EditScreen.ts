import { AppScreen } from "Vee/Screen/AppScreen";
import ControlTypes from "Vee/ControlTypes";
import { View } from "Vee/View/View";
import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import ScreenBind from "Vee/Screen/ScreenBind";

export default class EditScreen<TStateManager extends BaseStateManager<any>> extends AppScreen {
	private _isDirty: boolean;
	private _stateManager: TStateManager;

	public constructor(ViewType: { new(): View },
		StateManagerType: { new(screen: AppScreen): TStateManager },
		isTrackScreen: boolean = false) {
		super(ViewType, isTrackScreen);
		this._stateManager = new StateManagerType(this);
	}

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Screen,
				content: this.view.content,
				bottomDock: [
					{
						instance: ControlTypes.OrderLayout,
						rightToLeft: true,
						content: [
							{
								id: "cancelButton",
								instance: ControlTypes.Button,
								icon: "fa-times",
								text: "Cancel"
							},
							{
								id: "saveButton",
								instance: ControlTypes.Button,
								icon: "fa-floppy-o",
								text: "Save"
							}
						]
					}
				]
			}
		]
	}

	public get stateManager(): TStateManager {
		return this._stateManager;
	}

	// public get isDirty(): boolean {
	// 	return this._isDirty;
	// }
	// public set isDirty(value: boolean) {
	// 	if (this._isDirty !== value) {
	// 		this._isDirty = value;
	// 		this.view.cancelButton.enabled = this.isDirty;
	// 		this.view.saveButton.enabled = this.isDirty;
	// 	}
	// }

	public cancelBind = ScreenBind
		.create<any>(this, "cancelButton")
		.onRender((original, current) => {
			var isModified = (JSON.stringify(original) !== JSON.stringify(current));
			this.view.cancelButton.enabled = isModified;
			// this.isDirty = isModified;
		})
		.onClick(() => {
			this.stateManager.init();
		});

	public saveBind = ScreenBind
		.create<any>(this, "saveButton")
		.onRender((original, current) => {
			var isModified = (JSON.stringify(original) !== JSON.stringify(current));
			this.view.saveButton.enabled = isModified;
			// this.isDirty = isModified;
		})
		.onClick(() => {
			this.stateManager.save();
		});

}
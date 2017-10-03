import { AppScreen } from "Vee/Screen/AppScreen";
import ControlTypes from "Vee/ControlTypes";

export default class EditScreen extends AppScreen {
	public _isDirty: boolean;

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
								text: "Cancel"
							},
							{
								id: "saveButton",
								instance: ControlTypes.Button,
								text: "Save"
							}
						]
					}
				]
			}
		]
	}

	public get isDirty(): boolean {
		return this._isDirty;
	}
	public set isDirty(value: boolean) {
		if (this._isDirty !== value) {
			this._isDirty = value;
			this.view.cancelButton.enabled = this.isDirty;
			this.view.saveButton.enabled = this.isDirty;
		}
	}

}
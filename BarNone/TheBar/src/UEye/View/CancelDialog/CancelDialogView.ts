import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";

export default class CancelDialogView extends DialogView {
	public noButton: Button;
	public yesButton: Button;

	protected content: ComponentConfig[] = [
		{
			instance: ControlTypes.Label,
			text: "Unsaved Changes"
		},
		{
			instance: ControlTypes.Label,
			text: "Are you sure you want to continue?"
		},
		{
			instance: ControlTypes.OrderLayout,
			content: [
				{
					instance: ControlTypes.Button,
					id: "noButton",
					icon: "fa-times",
					text: "No"
				},
				{
					instance: ControlTypes.Button,
					id: "yesButton",
					icon: "fa-check",
					text: "Yes"
				}
			]
		}
	]
}
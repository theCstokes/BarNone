import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";
import { EDialogSize } from "UEye/Elements/Containers/Dialog/Dialog";

export default class CancelDialogView extends DialogView {
	public caption: string = "Cancel Changes"
	public cancelButtonText: string = "No"
	public cancelButtonIcon: string = "fa-times"
	public acceptButtonText: string = "Yes"
	public acceptButtonIcon: string = "fa-check"

	public constructor() {
		super();
		this.size = EDialogSize.Small;
	}

	// public noButton: Button;
	// public yesButton: Button;

	protected content: ComponentConfig[] = [
		// {
		// 	instance: ControlTypes.Label,
		// 	text: "Unsaved Changes"
		// },
		{
			instance: ControlTypes.Label,
			text: "Are you sure you want to continue?"
		},
		// {
		// 	instance: ControlTypes.OrderLayout,
		// 	content: [
		// 		{
		// 			instance: ControlTypes.Button,
		// 			id: "noButton",
		// 			icon: "fa-times",
		// 			text: "No"
		// 		},
		// 		{
		// 			instance: ControlTypes.Button,
		// 			id: "yesButton",
		// 			icon: "fa-check",
		// 			text: "Yes"
		// 		}
		// 	]
		// }
	]
}
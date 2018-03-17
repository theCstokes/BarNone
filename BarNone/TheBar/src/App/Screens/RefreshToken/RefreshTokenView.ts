import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye";
import Button from "UEye/Elements/Components/Button/Button";
import Input from "UEye/Elements/Components/Input/Input";
import Label from "UEye/Elements/Components/Label/Label";

export default class RefreshTokenView extends DialogView {
	public caption: string = "Login";
	public cancelButtonText: string = "Cancel";
	public cancelButtonIcon: string = "fa-times";
	public acceptButtonText: string = "Login";
	public acceptButtonIcon: string = "fa-sign-in-alt";

	public usernameInput: Input;
	public passwordInput: Input;
	public statusLabel: Label;
	// public cancelButton: Button;
	// public loginButton: Button;

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Label,
				hint: "Username",
				text: "Login Expired."
			},
			{
				id: "usernameInput",
				instance: ControlTypes.Input,
				hint: "Username"
			},
			{
				id: "passwordInput",
				instance: ControlTypes.PasswordInput,
				hint: "Password"
			},
			{
				id: "statusLabel",
				instance: ControlTypes.Label
			}
			// {
			// 	instance: ControlTypes.OrderLayout,
			// 	content: [
			// 		{
			// 			instance: ControlTypes.Button,
			// 			id: "cancelButton",
			// 			icon: "fa-times",
			// 			text: "Cancel"
			// 		},
			// 		{
			// 			instance: ControlTypes.Button,
			// 			id: "loginButton",
			// 			icon: "fa-sign-in",
			// 			text: "Login"
			// 		}
			// 	]
			// }
		]
	}
}
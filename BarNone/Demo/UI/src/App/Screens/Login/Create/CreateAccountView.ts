import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Input from "UEye/Elements/Components/Input/Input";
import Button from "UEye/Elements/Components/Button/Button";

export default class CreateAccountView extends View {
	public usernameInput: Input;
	public passwordInput1: Input;
	public passwordInput2: Input;
	public cancelButton: Button;
	public createButton: Button;

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.LoginFrame,
				background: 'res/bk1.jpg',
				content: [
					{
						id: "usernameInput",
						instance: ControlTypes.Input,
						hint: "Username"
					},
					{
						id: "passwordInput1",
						instance: ControlTypes.Input,
						hint: "Password"
					},
					{
						id: "passwordInput2",
						instance: ControlTypes.Input,
						hint: "Password"
					},
					{
						instance: ControlTypes.OrderLayout,
						content: [
							{
								instance: ControlTypes.Button,
								id: "cancelButton",
								icon: "fa-times",
								text: "Cancel"
							},
							{
								instance: ControlTypes.Button,
								id: "createButton",
								icon: "fa-sign-in",
								text: "Create"
							}
						]
					}
				]
			}
		];
	}
}
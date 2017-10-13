import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class CreateAccountView extends View {
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
						id: "password1Input",
						instance: ControlTypes.Input,
						hint: "Password"
					},
					{
						id: "password2Input",
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
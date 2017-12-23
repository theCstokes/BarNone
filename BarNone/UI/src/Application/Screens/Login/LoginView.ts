import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class LoginView extends View {
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
						id: "passwordInput",
						instance: ControlTypes.PasswordInput,
						hint: "Password"
					},
					{
						instance: ControlTypes.OrderLayout,
						content: [
							{
								instance: ControlTypes.Button,
								icon: "fa-undo",
								text: "Recover"
							},
							{
								instance: ControlTypes.Button,
								id: "createButton",
								icon: "fa-plus",
								text: "Create"
							},
							{
								instance: ControlTypes.Button,
								id: "loginButton",
								icon: "fa-sign-in",
								text: "Login"
							}
						]
					}
				]
			}
		];
	}
}
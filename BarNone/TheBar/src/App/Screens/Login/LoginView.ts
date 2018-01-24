import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";
import Input from "UEye/Elements/Components/Input/Input";
import Label from "UEye/Elements/Components/Label/Label";
import LoginFrame from "UEye/Elements/Containers/LoginFrame/LoginFrame";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
import Video from "UEye/Elements/Components/Video/Video";

export default class LoginView extends View {
	public frame: LoginFrame;
	public container: ContentContainer;
	public createButton: Button;
	public loginButton: Button;
	public usernameInput: Input;
	public passwordInput: Input;
	public statusLabel: Label;
	public video: Video;

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.LoginFrame,
				id: "frame",
				background: 'res/bk1.jpg',
				content: [
					{
						instance: ControlTypes.ContentContainer,
						id: "container",
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
								id: "statusLabel",
								instance: ControlTypes.Label
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
					},
					{
						instance: ControlTypes.Video,
						id: "video",
						visible: false
					}
				]
			}
		];
	}
}
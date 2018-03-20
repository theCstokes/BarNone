import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";
import Input from "UEye/Elements/Components/Input/Input";
import Label from "UEye/Elements/Components/Label/Label";
import LoginFrame from "UEye/Elements/Containers/LoginFrame/LoginFrame";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
import Video from "UEye/Elements/Components/Video/Video";

/**
 *  Represents View for Login Screen .
 */
export default class LoginView extends View {
	
	public frame: LoginFrame;
	public container: ContentContainer;

	/**
 *  Represents Button to Create profile .
 */
	public createButton: Button;
	/**
 *  Represents Button to login .
 */
	public loginButton: Button;
	/**
 *  Represents username input.
 */
	public usernameInput: Input;
	/**
 *  Represents password input.
 */
	public passwordInput: Input;
	/**
 *  Represents status label .
 */
	public statusLabel: Label;

	public video: Video;


	/** Accesor gets content
     * */
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
										icon: "fa-sign-in-alt",
										text: "Login"
									}
								]
							}
						]
					},
					{
						// instance: ControlTypes.Video,
						// id: "video",
						// visible: false
					}
				]
			}
		];
	}
}
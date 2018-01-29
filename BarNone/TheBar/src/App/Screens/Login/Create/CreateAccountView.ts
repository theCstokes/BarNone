import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Input from "UEye/Elements/Components/Input/Input";
import Button from "UEye/Elements/Components/Button/Button";
/**
 *  Represents View for Create Account Screen .
 */
export default class CreateAccountView extends View {
	/**
 *  Represents username input.
 */
	public usernameInput: Input;
	/**
 *  Represents password input 1.
 */
	public passwordInput1: Input;
	/**
 *  Represents password input 2.
 */
	public passwordInput2: Input;
	/**
 *  Represents Button to Cancel .
 */
	public cancelButton: Button;
	/**
 *  Represents Button to Create profile .
 */
	public createButton: Button;
/** Accesor gets content
     * */
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
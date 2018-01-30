import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";

export default class ChangePasswordView extends EditView {
	public currentPassword: Input;
	public newPassword: Input;
	public retypePassword: Input;
	public editPanel: Panel;

	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User Edit",
				content: [
					{
						id: "currentPassword",
						instance: ControlTypes.PasswordInput,
						hint: "Current Password"
					},
					{
						id: "newPassword",
						instance: ControlTypes.PasswordInput,
						hint: "New Password"
					},
					{
						id: "retypePassword",
						instance: ControlTypes.PasswordInput,
						hint: "Retype New Password"
					}
				]
			}
		];
	}
}
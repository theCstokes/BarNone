import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";

export default class UserEditView extends EditView {
	public editPanel: Panel;
	public nameInput: Input;
	public ageInput: Input;

	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User Edit",
				content: [
					{
						id: "nameInput",
						instance: ControlTypes.Input,
						hint: "Name"
					},
					{
						id: "ageInput",
						instance: ControlTypes.Input,
						readonly: true,
						hint: "Age",
						text: 21
					}
				]
			}
		];
	}
}
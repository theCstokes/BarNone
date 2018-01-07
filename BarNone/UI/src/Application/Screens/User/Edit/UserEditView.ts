import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";

export default class UserEditView extends View {
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
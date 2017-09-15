import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class UserEditView extends View {
	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User",
				content: [
					{
						instance: ControlTypes.Button,
						text: "Test"
					},
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
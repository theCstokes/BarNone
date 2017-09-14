import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class UserEditView extends View {
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Panel,
				content: [
					{
						instance: ControlTypes.Button,
						text: "Test"
					},
					{
						instance: ControlTypes.Input,
						hint: "Name"
					}
				]
			}
		];
	}
}
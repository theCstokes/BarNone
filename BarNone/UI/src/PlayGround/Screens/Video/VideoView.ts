import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";
import ContainerType from "Vee/ContainerType";

export default class VideoView extends View {
	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User Edit",
				content: [
					{
						instance: ControlTypes.Input,
						hint: "Teset"
					}
				]
			}
		];
	}
}
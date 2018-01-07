import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import ContainerType from "UEye/ContainerType";

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
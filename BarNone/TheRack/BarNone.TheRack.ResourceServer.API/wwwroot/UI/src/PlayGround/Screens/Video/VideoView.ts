import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class VideoView extends View {
	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User Edit",
				content: [
					{
						id: "videoPlayer",
						instance: ControlTypes.Video,
						frameData: [
							{
								x: 50,
								y: 50
							}
						]
					}
				]
			}
		];
	}
}
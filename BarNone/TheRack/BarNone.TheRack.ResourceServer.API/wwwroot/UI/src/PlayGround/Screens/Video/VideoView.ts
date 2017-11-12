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
						// frameData: [
						// 	{
						// 		x1: 50,
						// 		y1: 50,
						// 		x2: 90,
						// 		y2: 100

						// 	},
						// 	{
						// 		x1: 90,
						// 		y1: 100,
						// 		x2: 110,
						// 		y2: 100
						// 	}
						// ]
					}
				]
			}
		];
	}
}
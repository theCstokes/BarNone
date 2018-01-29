import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";

export default class LiftView extends View {
	public userList: List;
	
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "mainPanel",
						caption: "Users",
						content: [
							{
								instance: ControlTypes.List,
								id: "userList",
								style: ControlTypes.LiftFolderListItem
								// items: [
								// 	{
								// 		name: "Christopher Stokes",
								// 		selected: true
								// 	},
								// 	{
								// 		name: "Bob Bill"
								// 	}
								// ]
							}
						]
					}
				]
			}
		];
	}
}
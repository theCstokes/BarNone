import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";

export default class SettingsView extends View {
	public settingsList: List;
	
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "mainPanel",
						content: [
							{
								instance: ControlTypes.List,
								id: "settingsList",
								isSelectionList: true,
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
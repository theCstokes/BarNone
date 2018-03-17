import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import { SelectionListView } from "UEye/View/SelectionListView";
import ComponentType from "UEye/Elements/Inflater/ComponentInflater";

export default class SettingsView extends SelectionListView {
	public caption: string = "Settings";
	public listType: ComponentType = ControlTypes.LiftFolderListItem;
	public errorMessage: string = "No Settings";

	
	// public settingsList: List;
	
	// public get content(): any[] {
	// 	return [
	// 		{
	// 			instance: ControlTypes.PartitionLayout,
	// 			leftSide: [
	// 				{
	// 					instance: ControlTypes.Panel,
	// 					id: "mainPanel",
	// 					content: [
	// 						{
	// 							instance: ControlTypes.List,
	// 							id: "settingsList",
	// 							isSelectionList: true,
	// 							style: ControlTypes.LiftFolderListItem
	// 							// items: [
	// 							// 	{
	// 							// 		name: "Christopher Stokes",
	// 							// 		selected: true
	// 							// 	},
	// 							// 	{
	// 							// 		name: "Bob Bill"
	// 							// 	}
	// 							// ]
	// 						}
	// 					]
	// 				}
	// 			]
	// 		}
	// 	];
	// }
}
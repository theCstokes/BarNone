import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Info from "UEye/Elements/Components/Info/Info";
import { SelectionListView } from "UEye/View/SelectionListView";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import ComponentType from "UEye/Elements/Inflater/ComponentInflater";

export default class LiftView extends SelectionListView {
	public caption: string =  "Lifts";
	public listType: ComponentType = ControlTypes.LiftFolderListItem;
	public errorMessage: string = "Upload lift from Data Lift.";

	
	// public liftList: List;
	// public liftListInfo: Info;
	// public mainPanel: Panel;
	
	// public get content(): any[] {
	// 	return [
	// 		{
	// 			instance: ControlTypes.PartitionLayout,
	// 			leftSide: [
	// 				{
	// 					instance: ControlTypes.Panel,
	// 					id: "mainPanel",
	// 					caption: "Lifts",
	// 					content: [
	// 						{
	// 							instance: ControlTypes.List,
	// 							id: "liftList",
	// 							isSelectionList: true,
	// 							style: ControlTypes.LiftFolderListItem
	// 						},
	// 						{
	// 							instance: ControlTypes.Info,
	// 							id: "liftListInfo",
	// 							title: "No Lifts Available",
	// 							message: "Upload lift from Data Lift."
	// 						}
	// 					]
	// 				}
	// 			]
	// 		}
	// 	];
	// }
}
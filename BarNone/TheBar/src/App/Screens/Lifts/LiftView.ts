import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Info from "UEye/Elements/Components/Info/Info";

export default class LiftView extends View {
	public liftList: List;
	public liftListInfo: Info;
	public mainPanel: Panel;
	
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "mainPanel",
						caption: "Lifts",
						content: [
							{
								instance: ControlTypes.List,
								id: "liftList",
								style: ControlTypes.LiftFolderListItem
							},
							{
								instance: ControlTypes.Info,
								id: "liftListInfo",
								title: "No Lifts Available",
								message: "Upload lift from Data Lift."
							}
						]
					}
				]
			}
		];
	}
}
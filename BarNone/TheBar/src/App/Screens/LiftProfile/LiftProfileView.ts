import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";

export default class ProfileView extends View {
    public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "mainPanel",
						caption: "Lift Types",
						content: [
							{
								instance: ControlTypes.List,
								id: "typesList",
								isSelectionList: true,
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
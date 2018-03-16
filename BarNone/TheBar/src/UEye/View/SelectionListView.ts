import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Info from "UEye/Elements/Components/Info/Info";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import StringUtils from "UEye/Core/StringUtils";

export abstract class SelectionListView extends View {
	public listPanel: Panel;

	public selectionList: List;
	
	public selectionListInfo: Info;
	
	public abstract get caption(): string;

	public abstract get listType(): BaseListItem;

	public abstract get errorMessage(): string;

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "listPanel",
						caption: this.caption,
						content: [
							{
								instance: ControlTypes.List,
								id: "selectionList",
								isSelectionList: true,
								style: this.listType
							},
							{
								instance: ControlTypes.Info,
								id: "selectionListInfo",
								title: StringUtils.format("No {0} Available", this.caption),
								message: this.errorMessage
							}
						]
					}
				]
			}
		];
	}
}
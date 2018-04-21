import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Info from "UEye/Elements/Components/Info/Info";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import StringUtils from "UEye/Core/StringUtils";
import ComponentType from "UEye/Elements/Inflater/ComponentInflater";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";

export abstract class SelectionListView extends View {
	public listPanel: Panel;

	public selectionList: List;
	
	public selectionListInfo: Info;

	public addButton: IconButton;
	
	public abstract get caption(): string;

	public abstract get listType(): ComponentType;

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
						actions: [
							{
								id: "addButton",
								text: "New",
								icon: "fa-plus",
							}
						],
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
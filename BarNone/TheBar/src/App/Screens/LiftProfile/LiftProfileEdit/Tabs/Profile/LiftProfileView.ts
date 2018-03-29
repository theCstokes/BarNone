import ControlTypes from "UEye/ControlTypes";
import { IList } from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";

export interface ILiftProfileView {
	addButton: IconButton;
	profileList: IList<DataListItem>;
}

export class LiftProfileTab {
	public static get content(): any {
		return {
			actions: [
				{
					id: "addButton",
					text: "New",
					icon: "fa-plus"
				}
			],
			title: "Profiles",
			content: [
				{
					instance: ControlTypes.List,
					id: "profileList",
					isSelectionList: true,
					style: ControlTypes.DataListItem

				}
			]
		};
	}
}
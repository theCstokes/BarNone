import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";

export interface ILiftPermissionView {
	userShareSearch: DropDownInput;
	userShareList: List;
}

export class LiftPermissionTab {
	public static get content(): any {
		return {
			title: "Share",
			content: [
				{
					id: "userShareSearch",
					hint: "Search User",
					instance: ControlTypes.DropDownInput
				},
				{
					id:"userShareList",
					instance: ControlTypes.List,
					style: ControlTypes.DataListItem
				}
			]
		};
	}
}
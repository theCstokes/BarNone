import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";

export interface ILiftPermissionView {
	userShareList: List;
}

export class LiftPermissionTab {
	public static get content(): any {
		return {
			title: "Share",
			visible: false,
			content: [
				{
					id: "userShareSearchBar",
					instance: ControlTypes.SearchBar
				},
				{
					id: "userShareList",
					instance: ControlTypes.List
				}
			]
		};
	}
}
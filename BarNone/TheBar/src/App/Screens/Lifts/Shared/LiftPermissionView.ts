import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";

export interface ILiftPermissionView {
	userShareList: List;
}

export class LiftPermissionTab {
	public static get content(): any {
		return {
			title: "Share",
			content: [
				{
					id: "userShareSearchBar",
					interface: ControlTypes.SearchBar
				},
				{
					id: "userShareList",
					interface: ControlTypes.List
				}
			]
		};
	}
}
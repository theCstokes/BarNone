import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";

export interface ILiftPermissionView {
	userShareSearchBar: SearchBar;
}

export class LiftPermissionTab {
	public static get content(): any {
		return {
			title: "Share",
			content: [
				{
					id: "userShareSearchBar",
					instance: ControlTypes.SearchBar
				}
			]
		};
	}
}
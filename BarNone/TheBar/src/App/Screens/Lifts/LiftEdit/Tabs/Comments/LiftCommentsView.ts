import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";

export interface ILiftCommentsView {
	messenger: Messenger;
}

export class LiftCommentsTab {
	public static get content(): any {
		return {
			title: "Comments",
			content: [
				{
					id: "messenger",
					instance: ControlTypes.Messenger,
					// style: ControlTypes.DataListItem
				}
			]
		};
	}
}
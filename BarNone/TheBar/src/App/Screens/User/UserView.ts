import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";

/**
 * User view.
 */
export default class UserView extends View {
	/**
	 * User selection list.
	 */
	public userList: List;
	
	/**
	 * View content.
	 */
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.PartitionLayout,
				leftSide: [
					{
						instance: ControlTypes.Panel,
						id: "mainPanel",
						caption: "Users",
						content: [
							{
								instance: ControlTypes.List,
								id: "userList",
								style: ControlTypes.ContactListItem
							}
						]
					}
				]
			}
		];
	}
}
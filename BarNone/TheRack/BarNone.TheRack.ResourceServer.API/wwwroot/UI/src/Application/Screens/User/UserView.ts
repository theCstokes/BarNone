import { View } from "Vee/View/View";
import ControlTypes from "Vee/ControlTypes";

export default class UserView extends View {
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
								// items: [
								// 	{
								// 		name: "Christopher Stokes",
								// 		selected: true
								// 	},
								// 	{
								// 		name: "Bob Bill"
								// 	}
								// ]
							}
						]
					}
				]
			}
		];
	}
}
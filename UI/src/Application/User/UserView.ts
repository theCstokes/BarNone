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
						content: [
							{
								instance: ControlTypes.List,
								style: ControlTypes.ContactListItem,
								items: [
									{
										name: "Christopher Stokes",
										selected: true
									}
								]
							}
						]
					}
				]
			}
		];
	}
}
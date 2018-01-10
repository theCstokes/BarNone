import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";

export default class NavView extends View {
	public navList: List;
	public navBreadcrumbs: Breadcrumb;
	
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Frame,
				statusImageSource: "res/MacBarBell.jpg",
				globalDock: {
					instance: ControlTypes.OrderLayout,
					rightToLeft: true,
					content: [
						{
							instance: ControlTypes.Label,
							text: "Hello World"
						}
					]
				},
				contextDock: {
					instance: ControlTypes.ColumnLayout,
					columns: [
						{
							instance: ControlTypes.Column,
							content: [
								{
                                    instance: ControlTypes.Breadcrumb,
                                    id: "navBreadcrumbs",
									onClick: () => {
										console.warn("testst");
									}
								}
							]
						}
						//{
						//	instance: ControlTypes.Column,
						//	content: [
						//		{
						//			instance: ControlTypes.Button,
						//			text: "Add Contact"
						//		}
						//	]
						//},
						//{
						//	instance: ControlTypes.Column,
						//	content: {
						//	}
						//}
					]
				},
				navDock: {
					instance: ControlTypes.List,
					id: "navList",
					style: ControlTypes.NavigationListItem
				}
			}
		];
	}
}
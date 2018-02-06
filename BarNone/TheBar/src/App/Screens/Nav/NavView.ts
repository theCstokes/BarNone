import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
import Button from "UEye/Elements/Components/Button/Button";
import Frame from "UEye/Elements/Containers/Frame/Frame";

/**
 *  Represents View for NavigationScreen .
 */
export default class NavView extends View {
	/**
 *  Represents Navigation List Rendered.
 */
	public navList: List;
	/**
 *  Represents Breadcrumb UI component .
 */
	public navBreadcrumbs: Breadcrumb;

	public logoutButton: Button;
	public helpButton: Button;
	public exitHelpButton: Button;
	public pageFrame: Frame;

	/**
 * Acessor gets content layout of Lifts Edit Screen 
 * */
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Frame,
				id:"pageFrame",
				statusImageSource: "res/MacBarBell.jpg",
				// globalDock: {
				// 	instance: ControlTypes.OrderLayout,
				// 	rightToLeft: true,
				// 	content: [
				// 		{
				// 			instance: ControlTypes.Label,
				// 			text: "Hello World"
				// 		}
				// 	]
				// },
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
						},
						{
							instance: ControlTypes.Column,
							content: [
								{
								instance: ControlTypes.OrderLayout,
								content: [	
									{
										instance: ControlTypes.IconButton,
										id: "helpButton",
										text: "Help",
										icon: "fa-info"
									},
									{
										instance: ControlTypes.IconButton,
										id: "logoutButton",
										text: "Logout",
										icon: "fa-sign-out"
									}
									
							]
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
				},
				helpDock:
				{
					instance:ControlTypes.IconButton,
					id:"exitHelpButton",
					icon: "fa-times",
				}
			}
		];
	}
}
import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
import Button from "UEye/Elements/Components/Button/Button";
import Frame from "UEye/Elements/Containers/Frame/Frame";
import Toast from "UEye/Elements/Components/Toast/Toast";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import HTMLContent from "UEye/Elements/Components/HTMLContent/HTMLContent";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
import Panel from "UEye/Elements/Containers/Panel/Panel";

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

	public logoutButton: IconButton;
	public helpButton: IconButton;
	public notificationButton: IconButton;
	// public exitHelpButton: Button;
	public pageFrame: Frame;
	public toast: Toast;

	// Side Bar.
	public infoCenter: Panel;
	public notificationCenter: ContentContainer;
	public helpCenter: ContentContainer;
	public helpContent: HTMLContent;
	public notificationList: List;

	/**
 * Acessor gets content layout of Lifts Edit Screen 
 * */
	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Frame,
				id: "pageFrame",
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
				// addOns: [
				// 	{
				// 		id: "toast",
				// 		instance: ControlTypes.Toast
				// 	}
				// ],
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
											id: "notificationButton",
											text: "Notification",
											icon: "fa-bell"
										},
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
											icon: "fa-sign-out-alt"
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
				helpDock: [
					{
						id: "infoCenter",
						instance: ControlTypes.Panel,
						// caption: "Help Information",
						content: [
							{
								id: "notificationCenter",
								instance: ControlTypes.ContentContainer,
								fill: true,
								content: [
									{
										id: "notificationList",
										instance: ControlTypes.List,
										style: ControlTypes.DataListItem
									},
									{
										instance: ControlTypes.Info,
										id: "notificationListInfo",
										title: "No Notifications",
										message: "You have no unread notifications."
									}
								]
							},
							{
								id: "helpCenter",
								instance: ControlTypes.ContentContainer,
								fill: true,
								content: [
									{
										id: "helpContent",
										instance: ControlTypes.HTMLContent,
										
										// content: contentString
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
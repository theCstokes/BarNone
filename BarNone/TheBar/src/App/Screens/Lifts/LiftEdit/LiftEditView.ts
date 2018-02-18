import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import SideBarLayout from "Ueye/Elements/Containers/SideBarLayout/SideBarLayout";

export default class LiftEditView extends EditView {
	protected caption: string = "Lift Edit";

	public nameInput: Input;
	public ageInput: Input;
	// public editPanel: Panel;
	public player: Video;
	public commentList: List;
	public analyticsButton: IconButton;
	public videoLayout: SideBarLayout;

	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name"
			},
			{
				instance: ControlTypes.TabLayout,
				tabs: [
					{
						actions: [
							{
								id: "analyticsButton",
								text: "Logout",
								icon: "fa-chart-pie"
							}
						],
						title: "Video",
						content: [
							{
								instance: ControlTypes.SideBarLayout,
								id: "videoLayout",
								content: [
									{
										id: "player",
										instance: ControlTypes.Video
									}
								],
								sideBar: [
									{
										instance: ControlTypes.Panel,
										caption: "Analytics",
										content: [
											{
												instance: ControlTypes.List,
												id: "liftList",
												style: ControlTypes.LiftFolderListItem,
												items: [
													{
														id: 1,
														name: "Acceleration: 55m/s"
													}
												]
											}
										]
									}
								]
							}
						]
					},
					{
						title: "Comments",
						content: [
							{
								id: "commentList",
								instance: ControlTypes.List,
								style: ControlTypes.DataListItem
							}
						]
					}
				]
			}
		]
	}
}
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import SideBarLayout from "UEye/Elements/Containers/SideBarLayout/SideBarLayout";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import Video from "UEye/Elements/Components/Video/Video";
import { IList } from "UEye/Elements/Components/List/List";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";

export interface ILiftVideoView {
	player: Video;
	analyticsButton: IconButton;
	videoLayout: SideBarLayout;
	analysisList: IList<DataListItem>
}

export class LiftVideoTab {
	public static get content(): any {
		return {
			actions: [
				{
					id: "analyticsButton",
					text: "Settings",
					icon: "fa-cog"
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
							actions: [
								{
									id: "editButton",
									text: "Edit",
									icon: "fa-pencil-alt"
								}
							],
							content: [
								// {
								// 	instance: ControlTypes.Checkbox,
								// 	id: "checkOne",
								// 	text: "Skeletal View"
								// },
								{
									instance: ControlTypes.List,
									id: "analysisList",
									style: ControlTypes.AnalysisListItem,
									items: [
										// {
										// 	id: 1,
										// 	name: "Bar Acceleration",
										// 	value: "4.72m/s",
										// 	icon: "fa-plus",
										// 	onAction: () => alert(11)
										// },
										// {
										// 	id: 2,
										// 	name: "Knee Angle",
										// 	value: "30 deg",
										// 	onAction: () => alert(22)
										// },
									]
								}
							]
						}
					]
				}
			]
		};
	}
}
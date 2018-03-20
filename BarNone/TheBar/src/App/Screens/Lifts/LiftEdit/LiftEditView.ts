import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye"
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import SideBarLayout from "UEye/Elements/Containers/SideBarLayout/SideBarLayout";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";
import DropDownInput from "UEye/Elements/Components/DropDownInput/DropDownInput";
import { LiftPermissionTab, ILiftPermissionView } from "App/Screens/Lifts/Shared/LiftPermissionView";
import { ChartTab, IChartTabView } from "App/Screens/Lifts/ChartTab/ChartTab";
import Graph from "UEye/Elements/Components/Graph/Graph";

export default class LiftEditView extends EditView implements ILiftPermissionView, IChartTabView {
	protected caption: string = "Lift Edit";

	public nameInput: Input;
	public typeDropDown: DropDownInput;
	public parentDropDown: DropDownInput;
	public userShareList: List;
	public player: Video;
	public messenger: Messenger;
	public analyticsButton: IconButton;
	public videoLayout: SideBarLayout;

	//Members used by the chart tab
	public analysisTypeDropdown : DropDownInput;
	public jointDropdown: DropDownInput;
	public dimensionDropdown: DropDownInput;
	public chart: Graph;


	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name"
			},
			{
				instance: ControlTypes.OrderLayout,
				content: [
					{
						id: "typeDropDown",
						instance: ControlTypes.DropDownInput,
						hint: "Lift Type"
					},
					{
						id: "parentDropDown",
						instance: ControlTypes.DropDownInput,
						hint: "Parent Folder"
					}
				]
			},
			{
				instance: ControlTypes.TabLayout,
				tabs: [
					{
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
											{
												instance: ControlTypes.Checkbox,
												id: "checkOne",
												text: "Skeletal View"
											},
											{
												instance: ControlTypes.List,
												id: "liftList",
												style: ControlTypes.AnalysisListItem,
												items: [
													{
														id: 1,
														name: "Acceleration",
														value: "55m/s",
														icon: "fa-plus",
														onAction: () => alert(11)
													},
													{
														id: 2,
														name: "Knee Angle",
														value: "30 deg",
														onAction: () => alert(22)
													},
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
								id: "messenger",
								instance: ControlTypes.Messenger,
								// style: ControlTypes.DataListItem
							}
						]
					},
					LiftPermissionTab.content,
					ChartTab.content
					
				]
			}
				
		 ]
	}
}
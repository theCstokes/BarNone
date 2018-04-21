import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye"
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List, { IList } from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import SideBarLayout from "UEye/Elements/Containers/SideBarLayout/SideBarLayout";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";
import DropDownInput from "UEye/Elements/Components/DropDownInput/DropDownInput";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";
import Graph from "UEye/Elements/Components/Graph/Graph";
import { LiftVideoTab, ILiftVideoView } from "App/Screens/Lifts/LiftEdit/Tabs/Video/LiftVideoView";
import { ILiftPermissionView, LiftPermissionTab } from "App/Screens/Lifts/LiftEdit/Tabs/Share/LiftPermissionView";
import { IChartTabView, ChartTab } from "App/Screens/Lifts/LiftEdit/Tabs/Charts/ChartTab";
import { LiftCommentsTab, ILiftCommentsView } from "App/Screens/Lifts/LiftEdit/Tabs/Comments/LiftCommentsView";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";

export default class LiftEditView extends EditView 
	implements ILiftPermissionView, ILiftVideoView, ILiftCommentsView, IChartTabView {

	protected caption: string = "Lift Edit";

	public nameInput: Input;
	public typeDropDown: DropDownInput;
	public parentDropDown: DropDownInput;

	public player: Video;
	public analyticsButton: IconButton;
	public videoLayout: SideBarLayout;
	public analysisList: IList<DataListItem>

	public messenger: Messenger;

	//Members used by the chart tab
	public analysisTypeDropdown : DropDownInput;
	public jointDropdown: DropDownInput;
	public dimensionDropdown: DropDownInput;
	public chart: Graph;

	//Members used by shared tab
	public userShareSearch: DropDownInput;
	public userShareList: List;


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
					LiftVideoTab.content,
					LiftCommentsTab.content,
					LiftPermissionTab.content,
					ChartTab.content					
				]
			}
				
		 ]
	}
}
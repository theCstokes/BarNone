import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import { IList } from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";
import LiftProfileDialogScreen from "../LiftProfileDialog/LiftProfileDialogScreen";
import UEye from "UEye/UEye";
import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import { ILiftProfileView, LiftProfileTab } from "App/Screens/LiftProfile/LiftProfileEdit/Tabs/Profile/LiftProfileView";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";

export default class LiftEditView extends EditView implements ILiftProfileView {
	protected caption: string = "Lift Edit";

	public nameInput: Input;
	public messenger: Messenger;
	public addButton: IconButton;
	public profileList: IList<DataListItem>;

	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name",
				readonly: true
			},
			{
				id: "tab",
				instance: ControlTypes.TabLayout,
				tabs: [
					LiftProfileTab.content
				]
			}
		]
	}
}
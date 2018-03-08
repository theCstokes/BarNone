import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
// import Stream from "UEye/Elements/Components/Stream/Stream";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";
import TabLayout from "UEye/Elements/Containers/TabLayout/TabLayout"
import UEye from "UEye/UEye";
import RefreshTokenScreen from "App/Screens/RefreshToken/RefreshTokenScreen";

export default class LiftFolderEditView extends EditView {
	// public editPanel: Panel;
	public nameInput: Input;
	public liftList: List;
	public tab: TabLayout;

	protected caption = "Lift Folder Edit";

	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name"
			},
			{
				instance: ControlTypes.Button,
				text: "Open",
				onClick: () => {
					// alert(123);
					UEye.push(RefreshTokenScreen);
				}
			},
			{
				id: "tab",
				instance: ControlTypes.TabLayout,
				tabs: [
					{
						title: "Lifts",
						content: [
							{
								id: "liftList",
								instance: ControlTypes.List,
								style: ControlTypes.DataListItem
							}
						]
					}
				]
			}
		];
	}
}
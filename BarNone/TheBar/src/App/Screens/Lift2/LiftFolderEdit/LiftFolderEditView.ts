import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
// import Stream from "UEye/Elements/Components/Stream/Stream";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";

export default class LiftFolderEditView extends EditView {
	public editPanel: Panel;
	public nameInput: Input;
	public liftList: List;

	public get content(): any[] {
		return [
			{
				id: "editPanel",
				instance: ControlTypes.Panel,
				caption: "User Edit",
				content: [
					{
						id: "nameInput",
						instance: ControlTypes.Input,
						hint: "Name"
					},
					{
						id: "liftList",
						instance: ControlTypes.List,
						style: ControlTypes.LiftFolderListItem
					}
				]
			}
		];
	}
}
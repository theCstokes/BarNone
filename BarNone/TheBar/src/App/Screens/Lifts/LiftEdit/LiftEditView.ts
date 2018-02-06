import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";

export default class LiftEditView extends EditView {
	public nameInput: Input;
	public ageInput: Input;
	public editPanel: Panel;
	public player: Video;
	public commentList: List;

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
						instance: ControlTypes.TabLayout,
						tabs: [
							{
								title: "Video",
								content: [
									{
										id: "player",
										instance: ControlTypes.Video
									},
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
		];
	}
}
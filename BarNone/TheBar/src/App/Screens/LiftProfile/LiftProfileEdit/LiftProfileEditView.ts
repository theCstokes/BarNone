import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import List from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";
import LiftProfileDialogScreen from "../LiftProfileDialog/LiftProfileDialogScreen";
import UEye from "UEye/UEye";
import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";

export default class LiftEditView extends EditView {
	protected caption: string = "Lift Edit";

	public nameInput: Input;
	public messenger: Messenger;
	public addButton: IconButton;
	public profileList: List;

	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name",
				readonly: true
			},
			// {
			// 	id: "typeDropDown",
			// 	instance: ControlTypes.DropDownInput,
			// 	hint: "Type",
			// 	items: [
			// 		{
			// 			id: 1,
			// 			name: "Test"
			// 		},
			// 		{
			// 			id: 2,
			// 			name: "New Test"
			// 		}
			// 	]
			// },
			{
				id: "tab",
				instance: ControlTypes.TabLayout,
				tabs: [
					{
						actions: [
							{
								id: "addButton",
								text: "New",
								icon: "fa-plus",
								onClick: () => {
									// alert(123);
									UEye.push(LiftProfileDialogScreen);
								}
							}
						],
						title: "Profiles",
						content: [
							{
								instance: ControlTypes.List,
                                id: "profileList",
                                isSelectionList: true,
								style: ControlTypes.DataListItem
								
							}
						]
					},
				
				]
			}
				
		 ]
	}
}
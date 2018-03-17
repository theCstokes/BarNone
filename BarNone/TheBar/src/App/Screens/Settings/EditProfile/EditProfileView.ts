import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";

export default class EditProfileView extends EditView {
	protected caption: string = "Edit Profile";

	public nameInput: Input;
	public ageInput: Input;
	// public editPanel: Panel;

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.ColumnLayout,
				columns: [
					{
						instance: ControlTypes.Column,
						content: [
							{
								id: "nameInput",
								instance: ControlTypes.Input,
								readonly:true,
								hint: "Account Name"
							}

						]
					},
					{
						instance: ControlTypes.Column,
						content: [
						{
							instance: ControlTypes.Button,
							id: "editName",
							text: "Edit",
							icon: "fa-pencil"
						}
					]
					}
				
				]
			},
			{
				instance: ControlTypes.ColumnLayout,
				columns: [
					{
						instance: ControlTypes.Column,
						content: [
							{
								id: "userNameInput",
								instance: ControlTypes.Input,
								readonly:true,
								hint: "User Name"
							}

						]
					},
					{
						instance: ControlTypes.Column,
						content: [
						{
							instance: ControlTypes.Button,
							id: "editName",
							text: "Edit",
							icon: "fa-pencil"
						}
					]
					}
				
				]
			},
			{
				instance: ControlTypes.ColumnLayout,
				columns: [
					{
						instance: ControlTypes.Column,
						content: [
							{
								id: "ageInput",
								instance: ControlTypes.Input,
								readonly:true,
								hint: "Age"
							}

						]
					},
					{
						instance: ControlTypes.Column,
						content: [
						{
							instance: ControlTypes.Button,
							id: "editName",
							text: "Edit",
							icon: "fa-pencil"
						}
					]
					}
				
				]
			},
			{
				instance: ControlTypes.ColumnLayout,
				columns: [
					{
						instance: ControlTypes.Column,
						content: [
							{
								id: "genderInput",
								instance: ControlTypes.Input,
								readonly:true,
								hint: "Gender"
							}

						]
					},
					{
						instance: ControlTypes.Column,
						content: [
						{
							instance: ControlTypes.Button,
							id: "editName",
							text: "Edit",
							icon: "fa-pencil"
						}
					]
					}
				
				]
			}
		
		]
	}
}
import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";

export default class UserEditView extends EditView {
	protected caption: string = "User Edit";

	public editPanel: Panel;
	public nameInput: Input;
	public ageInput: Input;

	public get content(): any[] {
		return [
			{
				id: "nameInput",
				instance: ControlTypes.Input,
				hint: "Name"
			},
			{
				id: "ageInput",
				instance: ControlTypes.Input,
				readonly: true,
				hint: "Age",
				text: 21
			},
			{
				instance: ControlTypes.Graph,
				data: [{
					x: 15,
					y: 20
				}, {
					x: 15,
					y: 15
				}, {
					x: 60,
					y: 15
				}, 
				{
					x: 95,
					y: 20
				} ],
				title: "Snatch",
				xAxisLabel: "Lift Acceleration",
				yAxisLabel: "Something",
				draw: true
			}
		]
	}
}
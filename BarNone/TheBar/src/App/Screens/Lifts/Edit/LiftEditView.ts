import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
// import Stream from "UEye/Elements/Components/Stream/Stream";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
/**
 *  Represents View for Lifts Edit Screen .
 */
export default class LiftEditView extends EditView {
		/**
 * Represents input for lift name
 * */
	public nameInput: Input;
		/**
 * Represents input for age of user 
 * */
	public ageInput: Input;
		/**
 * Represents panel for editing
 * */
	public editPanel: Panel;
		/**
 * Represents video player of lift
 * */
	public player: Video;
/**
 * Acessor gets content layout of Lifts Edit Screen 
 * */
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
					// {
					// 	id: "liftPlayer",
					// 	instance: ControlTypes.Video
					// },
					{
						id: "player",
						instance: ControlTypes.Video
					},
					{
						id: "ageInput",
						instance: ControlTypes.Input,
						readonly: true,
						hint: "Age",
						text: 21
					}
				]
			}
		];
	}
}
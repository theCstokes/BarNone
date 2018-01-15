import ControlTypes from "UEye/ControlTypes";
import { EditView } from "UEye/View/EditView";
import Input from "UEye/Elements/Components/Input/Input";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import Video from "UEye/Elements/Components/Video/Video";
import Stream from "UEye/Elements/Components/Stream/Stream";
import { BaseDataManager } from "UEye/Data/BaseDataManager";

export default class LiftEditView extends EditView {
	public nameInput: Input;
	public ageInput: Input;
	public editPanel: Panel;
	public liftPlayer: Video;
	public streamPlayer: Video;

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
						id: "streamPlayer",
						instance: ControlTypes.Video,
						src: "http://localhost:58428/api/v1/LiftVideo?access_token=" + BaseDataManager.auth.access_token
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
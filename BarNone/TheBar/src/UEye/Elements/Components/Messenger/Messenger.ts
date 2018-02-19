import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Input from "UEye/Elements/Components/Input/Input";
import List from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import { Translate } from "UEye/Translate";
import ControlTypes from "UEye/ControlTypes";
import Core from "UEye/Elements/Core/Core";

export default class Messenger extends BaseComponent {

	//#region Private DOM Element(s).
	private e_messageList: HTMLElement;
	private e_inputArea: HTMLElement;
	//#endregion

	//#region  Private Field(s).
	private _list: List;
	private _input: Input;
	private _sendButton: IconButton;
	private _messageList: any[];
	//#endregion

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Messenger");

		this.e_messageList = Core.create("div", this.element, "Message-List");
		this.e_inputArea = Core.create("div", this.element, "Input-Area");

		this._list = new List(this.e_messageList);
		this._list.style = ControlTypes.DataListItem;

		this._input = new Input(this.e_inputArea);
		this._input.hint = Translate.Components.Messenger.InputHint;

		this._sendButton = new IconButton(this.e_inputArea);
		this._sendButton.icon = "fa-angle-right";
	}

	public set messages(value: any[]) {
		if (this._messageList !== value) {
			this._messageList = value;
			this._list.items = this._messageList;
		}
	}
	public get messages(): any[] {
		return this._messageList;
	}

	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}
}
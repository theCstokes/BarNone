import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Input from "UEye/Elements/Components/Input/Input";
import List from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import { Translate } from "UEye/Translate";
import ControlTypes from "UEye/ControlTypes";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback, OnActionCallback } from "UEye/Elements/Core/EventCallbackTypes";

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
	private _onSend: OnActionCallback;
	//#endregion

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Messenger");

		this.e_messageList = Core.create("div", this.element, "Message-List");
		this.e_inputArea = Core.create("div", this.element, "Input-Area");

		this._list = new List(this.e_messageList);
		this._list.style = ControlTypes.MessageListItem;
		// this._list.list.scrollTop = (this.element.offsetTop + this.element.offsetHeight);

		this.onBindView.on((view) => {
			// this._list.list.scrollTop = (this.element.offsetTop + this.element.offsetHeight);
			this._list.list.scrollTop = 1000;
		});

		this._input = new Input(this.e_inputArea);
		this._input.hint = Translate.Components.Messenger.InputHint;

		this._sendButton = new IconButton(this.e_inputArea);
		this._sendButton.icon = "fa-angle-right";
		this._sendButton.onClick = this._onSendButtonClickHandler.bind(this);
	}

	public set messages(value: any[]) {
		if (this._messageList !== value) {
			this._messageList = value;
			this._list.items = this._messageList;
			this._list.list.scrollTop = 10000;
		}
	}
	public get messages(): any[] {
		return this._messageList;
	}

	public set onSend(value: OnActionCallback) {
		if (this._onSend !== value) {
			this._onSend = value;
		}
	}
	public get onSend(): OnActionCallback {
		return this._onSend;
	}

	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}

	private _onSendButtonClickHandler(): void {
		if (this._onSend !== undefined) {
			this._onSend(this._input.text);
			this._input.text = "";
		}
	}
}
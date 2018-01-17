import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

export default class Button extends BaseComponent {
	private _text: string;
	private _icon: string;
	private _onClickCallback: OnClickCallback;

	private _textElement: HTMLElement;
	private _iconElement: HTMLElement;

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Button");

		this._iconElement = Core.create('div', this.element, 'fa', 'Icon');
		this._textElement = Core.create('div', this.element, 'Text');

		this.element.onclick = this.onClickHandler.bind(this);
	}

	public get text(): string {
		return this._text;
	}
	public set text(value: string) {
		this._text = value;
		this._textElement.textContent = this._text;
	}

	public get onClick(): OnClickCallback {
		return this._onClickCallback;
	}
	public set onClick(value: OnClickCallback) {
		this._onClickCallback = value;
	}

	public get icon(): string {
		return this._icon;
	}
	public set icon(value: string) {
		if (this._icon !== value) {
			Core.removeClass(this._iconElement, this._icon);
			this._icon = value;	
			if (value !== undefined) {
				Core.addClass(this._iconElement, "Visible");
			}
			Core.addClass(this._iconElement, this._icon);
		}
	}

	public onModifiedChange(): void {
		// Not needed for button.
	}
	public onReadonlyChange(): void {
		// Not needed for button.
	}
	public onErrorChange(): void {
		// Not needed for button.
	}
	public onEnabledChange(): void {
		if (this.enabled) {
			Core.removeClass(this.element, "disabled");
		} else {
			Core.addClass(this.element, "disabled");
		}
	}

	private onClickHandler(): void {
		if (this._onClickCallback !== undefined) {
			this._onClickCallback();
		}
	}
}
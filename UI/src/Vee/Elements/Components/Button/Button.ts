import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import { OnClickCallback } from "Vee/Elements/Core/BindTypes";

export default class Button extends BaseComponent {
	private _text: string;
	private _onClickCallback: OnClickCallback;

	public constructor(parent: HTMLElement) {
		super(parent, "Vee-Button");

		this.element.onclick = this.onClickHandler.bind(this);
	}

	public get text(): string {
		return this._text;
	}
	public set text(value: string) {
		this._text = value;
		this.element.textContent = this._text;
	}

	public get onClick(): OnClickCallback {
		return this._onClickCallback;
	}
	public set onClick(value: OnClickCallback) {
		this._onClickCallback = value;
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


	private onClickHandler(): void {
		if (this._onClickCallback !== undefined) {
			this._onClickCallback();
		}
	}
}
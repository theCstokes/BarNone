import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";

export default class Button extends BaseComponent {
	private _text: string;

	public constructor(parent: HTMLElement) {
		super(parent, "Vee-Button");
	}

	public get text(): string {
		return this._text;
	}
	public set text(value: string) {
		this._text = value;
		this.element.textContent = this._text;
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
}
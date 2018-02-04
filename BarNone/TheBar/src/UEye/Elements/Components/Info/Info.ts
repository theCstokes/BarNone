import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import StringUtils from "UEye/Core/StringUtils";


//#region Private Constant(s).
const MIN_SIZE = 200;
//#endregion

export default class Info extends BaseComponent {


	//#region Private DOM Element(s).
	private e_content: HTMLElement;
	private e_title: HTMLElement;
	private e_message: HTMLElement;
	//#endregion

	//#region Private Field(s).
	private _color: string;
	private _title: string;
	private _message: string;
	//#endregion

	//#region Public Constructor(s).
	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Info");

		this.e_content = Core.create("div", this.element, "Content");
		this.e_title = Core.create("div", this.e_content, "Title");
		this.e_message = Core.create("div", this.e_content, "Message");

		this.e_content.style.width = StringUtils.format("{0}px", MIN_SIZE + 10);
		this.e_content.style.height = StringUtils.format("{0}px", MIN_SIZE + 10);

		this.render();
	}
	//#endregion

	//#region Public Property(s).
	public get color(): string {
		return this._color;
	}
	public set color(value: string) {
		if (this._color !== value) {
			this._color = value;
		}
	}

	public get title(): string {
		return this._title;
	}
	public set title(value: string) {
		if (value !== this._title) {
			this._title = value;
			this.render();
		}
	}

	public get message(): string {
		return this._message;
	}
	public set message(value: string) {
		if (value !== this._message) {
			this._message = value;
			this.render();
		}
	}
	//#endregion

	//#region Public Member(s).
	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}
	//#endregion

	//#region Private Member(s).
	private render() {
		this.e_message.textContent = this._message;
		this.e_title.textContent = this._title;

		var width = Math.max(this.e_title.offsetWidth, this.e_message.offsetWidth);
		var height = Math.max(this.e_title.offsetHeight, this.e_message.offsetHeight);

		var size = Math.max(width, height, MIN_SIZE);

		this.e_content.style.color = this._color;
		this.e_content.style.borderColor = this._color;

		this.e_content.style.width = StringUtils.format("{0}px", size + 10);
		this.e_content.style.height = StringUtils.format("{0}px", size + 10);
	}
	//#endregion
}
import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class Dialog extends BaseContainer {
	//#region Private DOM Element(s).
	private e_content: HTMLElement;
	//#endregion

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Dialog");

		this.e_content = Core.create("div", this.element, "Content");
		this.linkComponentContainer("content", this.e_content);
	}

	public get content(): any[] {
		return this.getScreenContainer("content");
	}
	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}
}
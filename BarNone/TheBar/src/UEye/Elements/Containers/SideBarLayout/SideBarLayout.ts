import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class SidBarLayout extends BaseContainer {
	//#region Private DOM Element(s).
	private e_content: HTMLElement;
	private e_sideBar: HTMLElement;
	//#endregion

	constructor(parent: HTMLElement) {
		super(parent, "UEye-Side-Bar-Layout");
		// Core.addClass(this.element, "UEye-Panel");

		this.e_content = Core.create("div", this.element, "Content");
		this.linkComponentContainer("content", this.e_content);

		this.e_sideBar = Core.create("div", this.element, "Side-Bae");
		this.linkComponentContainer("sideBar", this.e_sideBar);
	}

	public set content(value: any[]) {
		this.setComponentContainer("content", value);
	}
	public get content(): any[] {
		return this.getComponentContainer("content");
	}

	public set sideBar(value: any[]) {
		this.setComponentContainer("sideBar", value);
	}
	public get sideBar(): any[] {
		return this.getComponentContainer("sideBar");
	}
}
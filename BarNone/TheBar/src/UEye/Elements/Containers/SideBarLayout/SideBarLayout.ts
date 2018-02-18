import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class SideBarLayout extends BaseContainer {
	//#region Private DOM Element(s).
	private e_content: HTMLElement;
	private e_sideBar: HTMLElement;
	//#endregion

	//#region Private Field(s).
	private isOpen: boolean = false;
	//#endregion

	//#region Public Constructor(s).
	constructor(parent: HTMLElement) {
		super(parent, "UEye-Side-Bar-Layout");

		this.e_content = Core.create("div", this.element, "Content");
		this.linkComponentContainer("content", this.e_content);

		this.e_sideBar = Core.create("div", this.element, "Side-Bar");
		this.linkComponentContainer("sideBar", this.e_sideBar);
	}
	//#endregion

	//#region Public Property(s).
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
	//#endregion

	//#region Public Member(s).
	public toggleSideBar() {
		this.isOpen = !this.isOpen;
		if (this.isOpen) {
			Core.replaceClass(this.e_sideBar, "Hide-Side-Bar", "Show-Side-Bar");
			Core.replaceClass(this.e_content, "Hide-Side-Bar", "Show-Side-Bar");
		} else {
			Core.replaceClass(this.e_sideBar, "Show-Side-Bar", "Hide-Side-Bar");
			Core.replaceClass(this.e_content, "Show-Side-Bar", "Hide-Side-Bar");
		}
		//#endregion
	}
}
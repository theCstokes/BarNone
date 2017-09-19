import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import Core from "Vee/Elements/Core/Core";
import { IScreen } from "Vee/Elements/Core/IScreen";

export default class Screen extends BaseContainer implements IScreen {
	private _bottomDockElement: HTMLElement;

	constructor(parent: HTMLElement) {
		super(parent);
		Core.addClass(this.element, "Vee-Screen");

		this.linkComponentContainer("content", this.element);

		this._bottomDockElement = Core.create("div", this.element, "Bottom-Dock");
		this.linkComponentContainer("bottomDock", this._bottomDockElement);
	}

	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}
	public get content(): any[] {
		return this.getScreenContainer("content");
	}

	public set dockBottom(value: any[]) {
		this.setComponentContainer("dockBottom", value);
	}
	public get dockBottom(): any[] {
		return this.getComponentContainer("dockBottom");
	}

	public destroy(): void {
		var parentNode = this.element.parentNode;
		if (parentNode !== null) {
			parentNode.removeChild(this.element);
		}
	}

	public onModifiedChange(): void {
		throw new Error("Method not implemented.");
	}
	public onReadonlyChange(): void {
		throw new Error("Method not implemented.");
	}
	public onErrorChange(): void {
		throw new Error("Method not implemented.");
	}
}
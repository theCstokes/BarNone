import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import Core from "Vee/Elements/Core/Core";
import { IScreen } from "Vee/Elements/Core/IScreen";

export default class Screen extends BaseContainer {
	private _contentElement: HTMLElement;
	private _bottomDockElement: HTMLElement;

	constructor(parent: HTMLElement) {
		super(parent);
		Core.addClass(this.element, "Vee-Screen");

		this._contentElement = Core.create("div", this.element, "Content");
		this.linkComponentContainer("content", this._contentElement);

		this._bottomDockElement = Core.create("div", this.element, "Bottom-Dock");
		this.linkComponentContainer("bottomDock", this._bottomDockElement);
	}

	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}
	public get content(): any[] {
		return this.getScreenContainer("content");
	}

	public set bottomDock(value: any[]) {
		this.setComponentContainer("bottomDock", value);
	}
	public get bottomDock(): any[] {
		return this.getComponentContainer("bottomDock");
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
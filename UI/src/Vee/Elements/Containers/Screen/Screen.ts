import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import Core from "Vee/Elements/Core/Core";
import { IScreen } from "Vee/Elements/Core/IScreen";

export default class Screen extends BaseContainer implements IScreen {

	constructor(parent: HTMLElement) {
		super(parent);
		Core.addClass(this.element, "Vee-Screen");

		this.linkComponentContainer("content", this.element);
	}

	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}
	public get content(): any[] {
		return this.getScreenContainer("content");
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
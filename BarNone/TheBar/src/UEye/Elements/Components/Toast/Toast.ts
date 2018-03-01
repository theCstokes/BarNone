import Core from "UEye/Elements/Core/Core";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";

export default class Toast extends BaseComponent {
	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Toast");
	}

	public showMessage(message: string): void {
		this.element.textContent = message;
		Core.addClass(this.element, "show");
		setTimeout(() => Core.removeClass(this.element, "show"), 3000);
	}

	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}
}
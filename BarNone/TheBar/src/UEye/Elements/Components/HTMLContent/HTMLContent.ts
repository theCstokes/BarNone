import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import StringUtils from "UEye/Core/StringUtils";

export default class HTMLContent extends BaseComponent {
	public constructor(parent: HTMLElement) {
		super(parent, "HTML-Content");

		this.onBindView.on((view) => {
			// this.element.style.height = StringUtils.format("{0}px", this.element.offsetHeight);
		});
	}

	public set content(value: string) {
		this.element.innerHTML = value;
	}

	public onEnabledChange(): void {
		throw new Error("Method not implemented.");
	}	
}
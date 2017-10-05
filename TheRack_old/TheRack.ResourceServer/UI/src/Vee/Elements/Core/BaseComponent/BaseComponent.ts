import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";

export abstract class BaseComponent extends BaseElement {

	public constructor(parent: HTMLElement, ... styles: string[]) {
		super(parent, ...styles);
	}
	
}
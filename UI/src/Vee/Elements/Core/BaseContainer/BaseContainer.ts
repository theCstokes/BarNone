import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";

export abstract class BaseContainer extends BaseElement {
	public constructor(parent: HTMLElement, ...style: string[]) {
		super(parent, ...style);
	}
}
import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 * Represents container that functions as a parent to children HTML elements. 
 * This container is used to custom style group of elements.
 */
export default class ContentContainer extends BaseContainer {
	private _fill: boolean = false;

	/**
	 * Constructor initializes and defines the ContentContainer 
	 * as an HTMLElement tag named UEye-Content-Container (using Core.addClass). 
	 * @returns Returns ContentContainer 
	 */
	constructor(parent: HTMLElement) {
		super(parent, "UEye-Content-Container");

		this.linkComponentContainer("content", this.element);
	}

	public set blur(value: boolean) {
		if (value) {
			Core.addClass(this.element, "Blur");
		} else {
			Core.removeClass(this.element, "Blur");
		}
	}

	public set fill(value: boolean) {
		if (value !== this._fill) {
			if (value) {
				Core.addClass(this.element, "Fill");
			} else {
				Core.removeClass(this.element, "Fill");
			}
		}
	}
	public get fill(): boolean {
		return this._fill;
	}

	/**
	 * Method sets the content of the ContentContainer container, 
	 * calling method inherited by BaseContainer. 
	 * @param value Array of contents of type any to be arranged as columns.   
	 */
	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}

	/**
	 * Accessor gets the value of the contents of the ContentContainer container.
	 * @returns An array of elements.
	 */
	public get content(): any[] {
		return this.getScreenContainer("content");
	}
}
import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";
/**
 *  Represents container that functions as a parent to children HTML elements. This container is used to custom style group of elements.
 */
export default class ContentContainer extends BaseContainer {
	// private _contentElement: HTMLElement;
	// private _bottomDockElement: HTMLElement;
	/** Constructor intializes and defines the ContentContainer as an HTMLElement tag named UEye-Content-Container (using Core.addClass). 
	 * * @returns Returns ContentContainer   
     * */
	constructor(parent: HTMLElement) {
		super(parent);
		Core.addClass(this.element, "UEye-Content-Container");

		this.linkComponentContainer("content", this.element);

		// this._contentElement = Core.create("div", this.element, "Content");
		// this.linkComponentContainer("content", this._contentElement);

		// this._bottomDockElement = Core.create("div", this.element, "Bottom-Dock");
		// this.linkComponentContainer("bottomDock", this._bottomDockElement);
	}
	 /** Method sets the content of the ContentContainer container, calling method inheirited by BaseContainer. 
	 * * @param value Array of contents of type any to be arranged as columns.   
     * */
	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}
	/** 
      * Accesor gets the value of the contents of the ContentContainer container.
	 * * @returns An array of elements.
     * */
	public get content(): any[] {
		return this.getScreenContainer("content");
	}

	// public set bottomDock(value: any[]) {
	// 	this.setComponentContainer("bottomDock", value);
	// }
	// public get bottomDock(): any[] {
	// 	return this.getComponentContainer("bottomDock");
	// }

	// public onModifiedChange(): void {
	// 	throw new Error("Method not implemented.");
	// }
	// public onReadonlyChange(): void {
	// 	throw new Error("Method not implemented.");
	// }
	// public onErrorChange(): void {
	// 	throw new Error("Method not implemented.");
	// }
}
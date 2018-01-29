import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import Core from "UEye/Elements/Core/Core";
/**Parent Class to all containers  */
export abstract class BaseContainer extends BaseElement {
	/** Represents Base list of components within containers */
	private componentContainers: { [key: string]: { element: HTMLElement, value: any[] } };
	/** Represents Base list of screens within containers */
	private screenContainers: { [key: string]: { element: HTMLElement, value: any[] } };
		/** Constructor makes basic HTMLElement
	 * @param parent HTMLElement
	 * @param styles Specific style of container
	*/
	public constructor(parent: HTMLElement, ...style: string[]) {
		super(parent, ...style);
		// Core.addClass(this.element, 'UEye-Container');

		this.componentContainers = {};
		this.screenContainers = {};
	}

		/** Method links individual components within container
	 * @param name string type representation of the component
	 * @param element Corresponding HTMLElement to components of container
	*/
	protected linkComponentContainer(name: string, element: HTMLElement) {
		this.componentContainers[name] = { element: element, value: [null] };
	}
		/** Method sets individual components within container
	 * @param name string type representation of the component
	 * @param value List of subcomponents
	*/
	protected setComponentContainer(name: string, value: any[]) {
		if (this.componentContainers.hasOwnProperty(name)) {
			this.componentContainers[name].value = value;
		}
	}
		/** Accessor gets list of components within container
	 * @param name string array epresentation of the component
	 *
	*/
	protected getComponentContainer(name: string): any[] {
		if (this.componentContainers.hasOwnProperty(name)) {
			return this.componentContainers[name].value;
		}
		return [];
	}
		/** Method sets individual components within container
	 * @param name string type representation of the component
	 * @param element HTMLElement
	*/
	protected linkScreenContainer(name: string, element: HTMLElement) {
		this.screenContainers[name] = { element: element, value: [null] };
	}
	/** Method sets individual components within container
	 * @param name string type representation of the component
	 * @param value List of subcomponents
	*/
	protected setScreenContainer(name: string, value: any[]) {
		if (this.screenContainers.hasOwnProperty(name)) {
			this.screenContainers[name].value = value;
		}
	}
		/** Accessor gets element array of type any of ComponentContainer
	 * @param name string array epresentation of the component
	 * @returns Any array
	 *
	*/
	protected getScreenContainer(name: string): any[] {
		if (this.screenContainers.hasOwnProperty(name)) {
			return this.screenContainers[name].value;
		}
		return [];
	}
		/** Method that sets component as a componentContainer
	 * @param name string array epresentation of the component
	 * @returns Boolean value 
	 *
	*/
	public isComponentContainer(name: string): boolean {
		return this.componentContainers.hasOwnProperty(name);
	}
		/** Accessor gets HTML element of ComponentContainer
	 * @param name string array epresentation of the component
	 * @returns HTMLElement or nll type
	 *
	*/
	public getComponentContainerElement(name: string): HTMLElement | null {
		if (this.componentContainers.hasOwnProperty(name)) {
			return this.componentContainers[name].element;
		}
		console.warn("Could not find component container with name: " + name);
		return null;
	}
	/** Accessor gets ScreenMountingPoint
	 * @returns HTMLElement or nll type
	 *
	*/
	public getScreenMountingPoints(): HTMLElement[] {
		return Object.keys(this.screenContainers).map(key => this.screenContainers[key].element);
	}
}
import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";
import Core from "Vee/Elements/Core/Core";

export abstract class BaseContainer extends BaseElement {
	private componentContainers: { [key: string]: { element: HTMLElement, value: any[] } };
	private screenContainers: { [key: string]: { element: HTMLElement, value: any[] } };
	
	public constructor(parent: HTMLElement, ...style: string[]) {
		super(parent, ...style);
		Core.addClass(this.element, 'UEye-Container');

		this.componentContainers = {};
		this.screenContainers = {};
	}


	protected linkComponentContainer(name: string, element: HTMLElement) {
		this.componentContainers[name] = { element: element, value: [null] };
	}

	protected setComponentContainer(name: string, value: any[]) {
		if (this.componentContainers.hasOwnProperty(name)) {
			this.componentContainers[name].value = value;
		}
	}

	protected getComponentContainer(name: string): any[] {
		if (this.componentContainers.hasOwnProperty(name)) {
			return this.componentContainers[name].value;
		}
		return [];
	}

	protected linkScreenContainer(name: string, element: HTMLElement) {
		this.screenContainers[name] = { element: element, value: [null] };
	}

	protected setScreenContainer(name: string, value: any[]) {
		if (this.screenContainers.hasOwnProperty(name)) {
			this.screenContainers[name].value = value;
		}
	}

	protected getScreenContainer(name: string): any[] {
		if (this.screenContainers.hasOwnProperty(name)) {
			return this.screenContainers[name].value;
		}
		return [];
	}

	public getComponentContainerElement(name: string): HTMLElement | null {
		if (this.componentContainers.hasOwnProperty(name)) {
			return this.componentContainers[name].element;
		}
		return null;
	}

	public getScreenMountingPoints(): HTMLElement[] {
		return Object.keys(this.screenContainers).map(key => this.screenContainers[key].element);
	}
}
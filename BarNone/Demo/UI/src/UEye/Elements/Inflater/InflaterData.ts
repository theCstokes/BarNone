import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import ControlTypes from "UEye/ControlTypes";
import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";

export default class InflaterData {
	public componentList: BaseElement[];
	public components: BaseElement[];

	public constructor() {
		this.componentList = [];
		this.components = [];
	}

	public get componentMap(): { [key: string]: BaseElement } {
		return this.componentList.reduce((result: { [key: string]: BaseElement }, component: BaseElement) => {
			if (component.id === undefined) return result;
			result[component.id] = component;
			return result;
		}, {});
	}

	public get mountingPoints(): HTMLElement[] {
		return this.componentList.reduce((result: HTMLElement[], component: BaseElement) => {
			// if (ControlTypes.isContainer(component.instance)) {
			if (component instanceof BaseContainer) {
				var container = component as BaseContainer;
				result = result.concat(container.getScreenMountingPoints());
			}
			// }

			return result;
		}, []);
	}
}
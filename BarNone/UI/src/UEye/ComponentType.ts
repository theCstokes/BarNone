import InflaterData from "UEye/Elements/Core/Inflater/InflaterData";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import InflationHelpers from "UEye/Elements/Core/Inflater/InflationHelpers";
import { IControlType } from "UEye/IControlType";

type ComponentBuilder = (parent: HTMLElement) => BaseComponent;

export default class ComponentType implements IControlType<BaseComponent> {
    private _builder: ComponentBuilder;

	public constructor(builder: ComponentBuilder) {
        this._builder = builder;
	}

    public create(parent: HTMLElement, config: ComponentConfig, data?: InflaterData): BaseComponent {
        if (data === undefined) data = new InflaterData();
		var component = this._builder(parent);

		if ("onShow" in component) {
			component.onShow();
		}

		InflationHelpers.populateComponent(component, config);

		data.componentList.push(component);
		return component;
	}

	public isType(obj: any): boolean {
		return obj instanceof BaseComponent;
	}
}
import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
import ComponentConfig from "Vee/Elements/Core/ComponentConfig";
import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import InflationHelpers from "Vee/Elements/Core/Inflater/InflationHelpers";
import { IControlType } from "Vee/IControlType";

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
}
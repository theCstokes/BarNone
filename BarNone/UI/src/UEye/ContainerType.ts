import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import InflaterData from "UEye/Elements/Core/Inflater/InflaterData";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import InflationHelpers from "UEye/Elements/Core/Inflater/InflationHelpers";
import { IControlType } from "UEye/IControlType";
import Inflater from "UEye/Elements/Core/Inflater/Inflater";

type ComponentBuilder = (parent: HTMLElement) => BaseContainer;

export default class ContainerType implements IControlType<BaseContainer> {
    private _builder: ComponentBuilder;

	public constructor(builder: ComponentBuilder) {
        this._builder = builder;
    }
    
    public create(parent: HTMLElement, config: ComponentConfig, data: InflaterData): BaseContainer {
        if (data === undefined) data = new InflaterData();
        var component = this._builder(parent);

		for (var key in config) {
			if (!config.hasOwnProperty(key)) continue;

			var property = config[key];
			if (!InflationHelpers.validateComponentProperty(component, key, typeof (property))) continue;

			if (component.isComponentContainer(key)) {
				var subComponentParent = component.getComponentContainerElement(key);
				if (subComponentParent !== null) {
					var subComponents = Inflater.execute(subComponentParent, property);
					(component as any)[key] = subComponents;
					data.componentList = data.componentList.concat(subComponents.componentList);
				}
			} else {
				(component as any)[key] = property;
			}
		}

		data.componentList.push(component);
		return component;
	}

	public isType(obj: any): boolean {
		return obj instanceof BaseContainer;
	}
}
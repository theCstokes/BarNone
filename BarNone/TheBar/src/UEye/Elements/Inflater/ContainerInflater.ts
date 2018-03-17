import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { IControlType } from "UEye/IControlType";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import Inflater from "UEye/Elements/Inflater/Inflater";
import { BaseView } from "UEye/Elements/Core/BaseView";

type ComponentBuilder = (parent: HTMLElement) => BaseContainer;

export default class ContainerType implements IControlType<BaseContainer> {
	private _builder: ComponentBuilder;

	public constructor(builder: ComponentBuilder) {
		this._builder = builder;
	}

	public inflate(parent: HTMLElement, config: ComponentConfig, view: BaseView,
		data?: InflaterData): BaseContainer {
		if (data === undefined) data = new InflaterData();
		var result = this.create(parent, config, view, data);
		// result.show(view);
		data!.componentList.forEach(c => c.show(view));
		return result;
	}

	public create(parent: HTMLElement, config: ComponentConfig, view: BaseView,
		data?: InflaterData): BaseContainer {
		if (data === undefined) data = new InflaterData();
		var component = this._builder(parent);

		component.bindView(view);

		for (var key in config) {
			if (!config.hasOwnProperty(key)) continue;

			var property = config[key];
			if (!Inflater.validateComponentProperty(component, key, typeof (property))) continue;

			if (component.isComponentContainer(key)) {
				var subComponentParent = component.getComponentContainerElement(key);
				if (subComponentParent !== null) {
					var subComponents = Inflater.create(subComponentParent, property, view);
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
import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
import InflationHelpers from "Vee/Elements/Core/Inflater/InflationHelpers";
import InflaterPipeline from "Vee/Elements/Core/Inflater/InflaterPipeline";
import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import ComponentConfig from "Vee/Elements/Core/ComponentConfig";

export default class PContainerInflater {
	public async execute(data: InflaterData, parent: HTMLElement, config: ComponentConfig)
		: Promise<BaseContainer> {
		var ComponentType = await InflationHelpers.createInstance<BaseContainer>(config.instance);
		var component = new ComponentType(parent);

		for (var key in config) {
			if (!config.hasOwnProperty(key)) continue;

			var property = config[key];
			if (!InflationHelpers.validateComponentProperty(component, key, typeof (property))) continue;

			if (component.isComponentContainer(key)) {
				var subComponentParent = component.getComponentContainerElement(key);
				if (subComponentParent !== null) {
					var pipeline = new InflaterPipeline();
					var subComponents = await pipeline.execute(subComponentParent, property);
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
}
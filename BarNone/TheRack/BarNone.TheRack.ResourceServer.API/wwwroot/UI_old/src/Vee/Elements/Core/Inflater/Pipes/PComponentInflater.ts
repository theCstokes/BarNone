// import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
// import InflationHelpers from "Vee/Elements/Core/Inflater/InflationHelpers";
// import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
// import ComponentConfig from "Vee/Elements/Core/ComponentConfig";

// export default class PComponentInflater {
// 	public execute(data: InflaterData, parent: HTMLElement, config: ComponentConfig): Promise<BaseComponent> {
// 		var component = config.instance.create(parent);

// 		// var ComponentType = await InflationHelpers.createInstance<BaseComponent>(config.instance);
// 		// var component = new ComponentType(parent);

// 		if ("onShow" in component) {
// 			component.onShow();
// 		}

// 		InflationHelpers.populateComponent(component, config);

// 		data.componentList.push(component);
// 		return component;
// 	}
// }
// import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
// import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

// export default class InflationHelpers {
// 	// public static async createInstance<T extends BaseElement>(path: string)
// 	// 	: Promise<{ new(parent: HTMLElement): T }> {
// 	// 	var module = await Loader.sync(path);
// 	// 	// Return the default class.
// 	// 	return module.default;
// 	// }

// 	public static populateComponent(component: any, config: ComponentConfig) {
// 		for (var key in config) {
// 			if (!config.hasOwnProperty(key)) continue;

// 			var property = config[key];
// 			if (!this.validateComponentProperty(component, key, typeof (property))) continue;

// 			component[key] = property;
// 		}
// 	}

// 	public static validateComponentProperty(component: any, id: string, type: string): boolean {
// 		if (!(id in component)) {
// 			console.warn("Component does not have a property named: " + id);
// 			return false;
// 		}
// 		if (typeof (component[id]) !== type) {
// 			console.warn("Component property " + id + " is not of type " + type);
// 			return false;
// 		}
// 		return true;
// 	}
// }
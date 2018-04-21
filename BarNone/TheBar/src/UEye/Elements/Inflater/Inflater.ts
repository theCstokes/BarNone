import ControlTypes from "UEye/ControlTypes";
// import PComponentInflater from "UEye/Elements/Core/Inflater/Pipes/PComponentInflater";
// import PContainerInflater from "UEye/Elements/Core/Inflater/Pipes/PContainerInflater";

import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import { BaseView } from "UEye/Elements/Core/BaseView";

export default class Inflater {
	public static execute(parent: HTMLElement, config: ComponentConfig[] | ComponentConfig, view: BaseView): InflaterData {
		var data = Inflater.create(parent, config, view);

		data.components.forEach(c => c.show(view));

		return data;
	}

	public static create(parent: HTMLElement, config: ComponentConfig[] | ComponentConfig, 
		view: BaseView): InflaterData {
		if (!Array.isArray(config)) config = [config];

		var validConfigList = config.filter(config => Inflater.validateConfig(config));

		var data = new InflaterData();

		data.components = data.components.concat(validConfigList
			.filter(config => {
				if (config === undefined || config === null) return false;
				if (config.instance === undefined || config.instance === null) return false;
				return true;
			})
			.map(config => {
			return config.instance.create(parent, config, view, data);
		}));

		return data;
	}

	public static populateComponent(component: any, config: ComponentConfig) {
		for (var key in config) {
			if (!config.hasOwnProperty(key)) continue;

			var property = config[key];
			if (!this.validateComponentProperty(component, key, typeof (property))) continue;

			component[key] = property;
		}
	}

	public static validateComponentProperty(component: any, id: string, type: string): boolean {
		// if (!(id in component)) {
		// 	console.warn("Component does not have a property named: " + id);
		// 	return false;
		// }
		// if (typeof (component[id]) !== type) {
		// 	console.warn("Component property " + id + " is not of type " + type);
		// 	return false;
		// }
		return true;
	}

	// public inflateByPath(path: string, parent: HTMLElement, config: ComponentConfig): BaseListItem {
	// 	var validConfig = this.validateConfig(config);

	// 	var data = new InflaterData();

	// 	config.instance = path;

	// 	var inflater = new PComponentInflater();
	// 	inflater.execute(data, parent, config);

	// 	return data.componentList[0] as BaseListItem;
	// }

	private static validateConfig(config: ComponentConfig): boolean {
		if (!config.hasOwnProperty('instance') && typeof (config.instance) === "string") {
			console.warn("Config must contain instance string.");
			return false;
		}
		return true;
	}
}
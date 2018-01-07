import ControlTypes from "UEye/ControlTypes";
// import PComponentInflater from "UEye/Elements/Core/Inflater/Pipes/PComponentInflater";
// import PContainerInflater from "UEye/Elements/Core/Inflater/Pipes/PContainerInflater";
import InflaterData from "UEye/Elements/Core/Inflater/InflaterData";
import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

export default class Inflater {
	public static execute(parent: HTMLElement, config: ComponentConfig[] | ComponentConfig): InflaterData {
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
			return config.instance.create(parent, config, data);
		}));

		return data;
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
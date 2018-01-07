import ControlTypes from "Vee/ControlTypes";
// import PComponentInflater from "Vee/Elements/Core/Inflater/Pipes/PComponentInflater";
// import PContainerInflater from "Vee/Elements/Core/Inflater/Pipes/PContainerInflater";
import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";
import { BaseListItem } from "Vee/Elements/Core/BaseListItem/BaseListItem";
import ComponentConfig from "Vee/Elements/Core/ComponentConfig";

export default class InflaterPipeline {
	public execute(parent: HTMLElement, config: ComponentConfig[] | ComponentConfig): InflaterData {
		if (!Array.isArray(config)) config = [config];

		var validConfigList = config.filter(config => this.validateConfig(config));

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

	private validateConfig(config: ComponentConfig): boolean {
		if (!config.hasOwnProperty('instance') && typeof (config.instance) === "string") {
			console.warn("Config must contain instance string.");
			return false;
		}
		return true;
	}
}
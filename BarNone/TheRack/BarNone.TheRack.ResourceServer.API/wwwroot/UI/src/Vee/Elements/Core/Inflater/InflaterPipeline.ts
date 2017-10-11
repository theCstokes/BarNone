import ControlTypes from "Vee/ControlTypes";
import PComponentInflater from "Vee/Elements/Core/Inflater/Pipes/PComponentInflater";
import PContainerInflater from "Vee/Elements/Core/Inflater/Pipes/PContainerInflater";
import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";
import { BaseListItem } from "Vee/Elements/Core/BaseListItem/BaseListItem";
import ComponentConfig from "Vee/Elements/Core/ComponentConfig";

export default class InflaterPipeline {
	public async execute(parent: HTMLElement, config: ComponentConfig[] | ComponentConfig)
		: Promise<InflaterData> {
		if (!Array.isArray(config)) config = [config];
		
		var validConfigList = config.filter(config => this.validateConfig(config));

		var data = new InflaterData();

		data.components = data.components.concat(await Promise.all(validConfigList
			.filter(config => ControlTypes.isComponent(config.instance))
			.map(async config => {
				var inflater = new PComponentInflater();
				return await inflater.execute(data, parent, config);
			})));

			data.components = data.components.concat(await Promise.all(validConfigList
			.filter(config => ControlTypes.isContainer(config.instance))
			.map(async config => {
				var inflater = new PContainerInflater();
				return await inflater.execute(data, parent, config);
			})));

		return data;
	}

	public async inflateByPath(path: string, parent: HTMLElement, config: ComponentConfig): Promise<BaseListItem> {
		var validConfig = this.validateConfig(config);

		var data = new InflaterData();

		config.instance = path;

		var inflater = new PComponentInflater();
		await inflater.execute(data, parent, config);

		return data.componentList[0] as BaseListItem;
	}

	private validateConfig(config: ComponentConfig): boolean {
		if (!config.hasOwnProperty('instance') && typeof (config.instance) === "string") {
			console.warn("Config must contain instance string.");
			return false;
		}
		return true;
	}
}
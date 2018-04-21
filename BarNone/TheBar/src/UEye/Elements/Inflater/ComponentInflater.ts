import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import { IControlType } from "UEye/IControlType";
import Inflater from "UEye/Elements/Inflater/Inflater";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import { BaseView } from "UEye/Elements/Core/BaseView";

type ComponentBuilder = (parent: HTMLElement) => BaseComponent;

export default class ComponentType implements IControlType<BaseComponent> {
	private _builder: ComponentBuilder;

	public constructor(builder: ComponentBuilder) {
		this._builder = builder;
	}	

	public inflate(parent: HTMLElement, config: ComponentConfig, view: BaseView,
		data?: InflaterData): BaseComponent {
		if (data === undefined) data = new InflaterData();
		var result = this.create(parent, config, view, data);
		// result.show(view);
		data!.componentList.forEach(c => c.show(view));
		return result;
	}

	public create(parent: HTMLElement, config: ComponentConfig, view: BaseView,
		data?: InflaterData): BaseComponent {
		if (data === undefined) data = new InflaterData();
		var component = this._builder(parent);

		component.bindView(view);

		// if ("onShow" in component) {
		// 	component.onShow();
		// }

		Inflater.populateComponent(component, config);

		data.componentList.push(component);
		return component;
	}

	public isType(obj: any): boolean {
		return obj instanceof BaseComponent;
	}
}
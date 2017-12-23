import { BaseElement } from "Vee/Elements/Core/BaseElement/BaseElement";
import InflaterData from "Vee/Elements/Core/Inflater/InflaterData";
import ComponentConfig from "Vee/Elements/Core/ComponentConfig";

export interface IControlType<T extends BaseElement> {
    create(parent: HTMLElement, config: ComponentConfig, data?: InflaterData): T;
}
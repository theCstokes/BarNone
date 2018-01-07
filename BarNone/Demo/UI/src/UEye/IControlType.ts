import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import InflaterData from "UEye/Elements/Inflater/InflaterData";

export interface IControlType<T extends BaseElement> {
    /**
     * Create control.
     */
    create(parent: HTMLElement, config: ComponentConfig, data?: InflaterData): T;

    /**
     * Determines if is of control type.
     */
    // isType(obj: any): boolean;
}
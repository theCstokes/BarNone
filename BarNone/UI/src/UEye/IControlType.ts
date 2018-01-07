import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import InflaterData from "UEye/Elements/Core/Inflater/InflaterData";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

export interface IControlType<T extends BaseElement> {
    /**
     * Create control.
     */
    create(parent: HTMLElement, config: ComponentConfig, data?: InflaterData): T;

    /**
     * Determines if is of control type.
     */
    isType(obj: any): boolean;
}
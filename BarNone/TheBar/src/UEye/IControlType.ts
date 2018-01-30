import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import { BaseView } from "UEye/Elements/Core/BaseView";

/**
 * Builder for control type.
 */
export interface IControlType<T extends BaseElement> {
    /**
     * Create control.
     */
    create(parent: HTMLElement, config: ComponentConfig, view: BaseView, data?: InflaterData): T;
}
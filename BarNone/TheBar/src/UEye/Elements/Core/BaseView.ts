import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
import StringUtils from "UEye/Core/StringUtils";

export class BaseView {
    public setElement(name: string, value: BaseElement) {
        (this as any)[name]  = value;
    }

    public setElements(elementMap: { [key: string]: BaseElement }) {
        for (var key in elementMap) {
            if (!elementMap.hasOwnProperty(key)) continue;
            (this as any)[key] = elementMap[key];

            if (!(key in this)) {
                console.warn(StringUtils.format("{0} is not exposed in view.", key));
            }
        }
    }
}
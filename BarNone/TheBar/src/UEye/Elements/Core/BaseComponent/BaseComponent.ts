import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
/**Parent Class to all components. BaseComponent provides the basic foundation to build individual components out off*/
export abstract class BaseComponent extends BaseElement {
	/** HTMLELement is enabled*/
	private _enabled: boolean;
	/** Constructor makes basic HTMLElement
	 * @param parent HTMLElement
	 * @param styles Specific style of component
	*/
	public constructor(parent: HTMLElement, ... styles: string[]) {
		super(parent, ...styles);
	}
	/**Accessor gets enabled boolean value
	 * @returns Boolean value indicating enabled property of HTMLElement (true: enabled and false: disabled)
	 */
	public get enabled(): boolean {
		return this._enabled;
	}
	/**Method sets enabled property
	 * @param value Represents enabled or disabled property of component
	 */
	public set enabled(value: boolean) {
		if (value !== this._enabled) {
			this._enabled = value;
			this.onEnabledChange();
		}
	}
	/**Abstract event listener */
	public abstract onEnabledChange(): void;
	
}
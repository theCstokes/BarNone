import Core from "UEye/Elements/Core/Core";
import { BaseView } from "UEye/Elements/Core/BaseView";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import StringUtils from "UEye/Core/StringUtils";

/**Parent Class to all components including BaseComponent.BaseElement provides the basic HTML functionality*/
export abstract class BaseElement {
	/** Represents Base HTMLElement  */
	private _element: HTMLElement;
	/** Represents unique identity */
	private _id: any;
	/** Represents an HTML instance  */
	private _instance: any;
	/** Represents modified property  */
	private _modified: boolean;
	/** Represents readonly property  */
	private _readonly: boolean;
	/** Represents error flag property  */
	private _error: string;
	/** Represents visible flag property  */
	private _visible: boolean = true;
	/** Parent HTML Element */
	private _parent: HTMLElement;

	/** Represents onShow callback  */
	protected onBindView: DataEvent<BaseView>;
	protected onShow: DataEvent<BaseView>;

	/** Constructor makes basic HTMLElement
	 * @param parent HTMLElement
	 * @param styles Specific style of component
	*/
	public constructor(parent: HTMLElement, ...styles: string[]) {
		this._element = Core.create('div', parent, ...styles);
		this._parent = parent;
		this.onBindView = new DataEvent<BaseView>();
		this.onShow = new DataEvent<BaseView>();
	}
	
	/** Accessor to get _element property.
	* @returns Returns element property.
	* */
	public get element(): HTMLElement {
		return this._element;
	}

	/**
	 * Accessor for parent HTMLElement
	 */
	public get parent(): HTMLElement {
		return this._parent;
	}

	/** Accessor to get _id property.
	* @returns Returns id property.
	* */
	public get id(): any {
		return this._id;
	}
	/**Method sets _id property
	 * @param value Represents identifier of type any
	 */
	public set id(value: any) {
		this._id = value;
	}
	/** Accessor to get _instance property.
	* @returns Returns instance property.
	* */
	public get instance(): any {
		return this._instance;
	}
	/**Method sets _instance property
	 * @param value Represents instance of type any
	 */
	public set instance(value: any) {
		this._instance = value;
	}

	public get visible(): boolean {
		return this._visible;
	}
	public set visible(value: boolean) {
		if (value !== this._visible) {
			this._visible = value;
			if (!this._visible) {
				Core.addClass(this.element, "UEye-Invisible");
			} else {
				Core.removeClass(this.element, "UEye-Invisible");
			}
		}
	}

	/** Accessor to get _modified property.
     * @returns Returns modified property.
     * */
	public get modified(): boolean {
		return this._modified;
	}
	/**Method sets _modified property
 * @param value Represents modified of type boolean
 */
	public set modified(value: boolean) {
		this._modified = value;
		this.onModifiedChange();
	}
	/** Accessor to get _readonly property.
     * @returns Returns readonly property.
     * */
	public get readonly(): boolean {
		return this._readonly;
	}
	/**Method sets _readonly property
 * @param value Represents modified of type boolean
 */
	public set readonly(value: boolean) {
		this._readonly = value;
		this.onReadonlyChange();
	}
	/** Accessor to get _error property.
     * @returns Returns error property.
     * */
	public get error(): string {
		return this._error;
	}
	/**Method sets _error property
 * @param value Represents flag error of type string
 */
	public set error(value: string) {
		this._error = value;
		this.onErrorChange();
	}
	/**Method destorys element
 */
	public destroy(): void {
		var parentNode = this.element.parentNode;
		if (parentNode !== null) {
			parentNode.removeChild(this.element);
		}
	}

	public bindView(view: BaseView): void {
		this.onBindView.trigger(view);
	}
	
	public show(view: BaseView): void {
		this.onShow.trigger(view);
	}

	/**Abstract event listener */
	public onModifiedChange(): void {
		throw (StringUtils.format("No onModifiedChange implemented for component: {0}", this.getName()))
	}
	/**Abstract event listener */
	public onReadonlyChange(): void {
		throw (StringUtils.format("No onReadonlyChange implemented for component: {0}", this.getName()))
	}
	/**Abstract event listener */
	public onErrorChange(): void {
		throw (StringUtils.format("No onErrorChange implemented for component: {0}", this.getName()))
	}

	private getName(): string { 
		return this.constructor.name;
 };
}
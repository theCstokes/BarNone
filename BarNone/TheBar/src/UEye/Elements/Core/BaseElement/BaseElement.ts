import Core from "UEye/Elements/Core/Core";
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
	/** Constructor makes basic HTMLElement
	 * @param parent HTMLElement
	 * @param styles Specific style of component
	*/
	public constructor(parent: HTMLElement, ...styles: string[]) {
		this._element = Core.create('div', parent, ...styles);
	}
	 /** Accessor to get _element property.
     * @returns Returns element property.
     * */
	public get element(): HTMLElement {
		return this._element;
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
	/**Abstract event listener */
	public onModifiedChange(): void {
		throw("No onModifiedChange implemented for component.")
	}
	/**Abstract event listener */
	public onReadonlyChange(): void  {
		throw("No onModifiedChange implemented for component.")
	}
	/**Abstract event listener */
	public onErrorChange(): void {
		throw("No onModifiedChange implemented for component.")
	}
}
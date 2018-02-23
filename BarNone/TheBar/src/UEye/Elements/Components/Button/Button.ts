import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

/**
 
 *  Represents clickable Button elements. This component is used in multiple screens whenever standard button functionality is needed.
 */
export default class Button extends BaseComponent {
	 /**  Represents text content of the Button component as a string value. Used as an identifier. */
	protected _text: string;
	/**  Represents icon content of the Button component as a string value. Used as an identifier. */
	private _icon: string;
	/**  Represents event listner that is called when even occurs.*/
	private _onClickCallback: OnClickCallback;
	/**  Represents text content of the the Button component as an HTMLElement (and generate a HTML tag). */
	protected _textElement: HTMLElement;
	/**  Represents icon content of the the Button component as an HTMLElement (and generate a HTML tag).*/
	private _iconElement: HTMLElement;

	/** Constructor intializes and defines the Button component as an HTMLElement tag named UEye-Button (using style paramaerter in Parent constructor definition). Returns a Button type with icon and text child elements.
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Button.   
     * */

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Button");

		this._iconElement = Core.create('div', this.element, 'fa', 'Icon');
		this._textElement = Core.create('div', this.element, 'Text');

		this.element.onclick = this.onClickHandler.bind(this);
	}
	 /** Accessor to get _text property.
     * @returns Returns the string value of text content of Button element.
     * */
	public get text(): string {
		return this._text;
	}
	  /** Method for setting value of property _text and changing the content of _textElement to reflect the same.
     * @param value Method parameter represents string of _text.
     * */
	public set text(value: string) {
		this._text = value;
		this._textElement.textContent = this._text;
	}
	 /** Accessor to get callback property.
     * @returns Returns the property responsible for callback on click operation.
     * */
	public get onClick(): OnClickCallback {
		return this._onClickCallback;
	}
	  /** Method for setting property _onClickCallback
     * @param value Method parameter represnts onClickCallback property
     * */
	public set onClick(value: OnClickCallback) {
		this._onClickCallback = value;
	}
	/** Accessor to get _icon property.
     * @returns Returns the string value of icon content of Button element.
     * */
	public get icon(): string {
		return this._icon;
	}
	 /** Method for setting value of property _icon and deleting the existing icon, creating a new icon HTMLElement and replacing the original.
     * @param value Method parameter represents string of _icon (requires the fa prefix to represent use of Font Awesome).
     * */
	public set icon(value: string) {
		if (this._icon !== value) {
			Core.removeClass(this._iconElement, this._icon);
			this._icon = value;	
			if (value !== undefined) {
				Core.addClass(this._iconElement, "Visible");
			}
			Core.addClass(this._iconElement, this._icon);
		}
	}

	/** Method that toggles enable and disable state of a Button element.
     * @returns Nothing (return part of property definition).
     * */

	public onEnabledChange(): void {
		if (this.enabled) {
			Core.removeClass(this.element, "disabled");
		} else {
			Core.addClass(this.element, "disabled");
		}
	}
    /** Method that returns event handler for onClick operations.
     * @returns Nothing (return part of property definition).
     * */
	private onClickHandler(): void {
		if (this._onClickCallback !== undefined) {
			this._onClickCallback();
		}
	}
}
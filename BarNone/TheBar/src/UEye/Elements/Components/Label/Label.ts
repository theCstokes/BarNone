import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
/**
 
 *  Represents captions for UI items and elements. This component is used in multiple screens whenever captions are needed to identify items.
 */
export default class Label extends BaseComponent {
    /**  Represents text content of the Label component as a string value. Used as an identifier. */
    private _text: string;
     /** Constructor intializes and defines the Label component as an HTMLElement tag named UEye-Label(using Core.addClass).
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns Label.   
     * */
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Label");
    }  
     /** Method for setting value of property _text and changing the content of _textElement to reflect the same.
     * @param value Method parameter represents string of _text.
     * */
    public set text(value: string) {
        this._text = value;
        this.element.textContent = this._text;
    }
     /** Accessor to get _text property.
     * @returns Returns the string value of text content of Label element.
     * */
    public get text(): string {
        return this._text;
    }

     /** Method that toggles enable and disable state of a Button element.
     * @returns Nothing (return part of property definition).
     * */
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
}
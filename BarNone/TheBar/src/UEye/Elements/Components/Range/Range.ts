import Core from "UEye/Elements/Core/Core";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
/**
 *  Represents interactive element Range.
 */
export default class Range extends BaseComponent {
    /**Represents HTMLElement as content of the input range*/
    private _content: HTMLElement;
    /**Represents HTMLElement as placeholder that describes what input range user needs to fill*/
    private _hint: HTMLElement;
    /**Represents HTMLInputElement of the input range*/
    protected _input: HTMLInputElement;
    /**Represents actions to clear or save input range */
    private _action: HTMLElement;
    /**Represents placeholder as text*/
    private _hintText: string;
    /**Represents placeholder as text*/

    private _text: string;
    /**  Represents event listner that is called when even occurs.*/
    private _onChangeCallback: OnChangeCallback;
    /** Constructor intializes and defines the Video component as an HTMLElement tag named UEye-Range. 
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Range type.
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Range");

        this._content = Core.create("div", this.element, "Content");
        this._action = Core.create("div", this.element, "Action");

        this._hint = Core.create("div", this._content, "Hint");
        this._input = Core.create("input", this._content, "Input") as HTMLInputElement;

        this._input.oninput = this.onInputHandler.bind(this);
        this._input.onfocus = this.onFocusHandler.bind(this);
        this._input.onblur = this.onBlurHandler.bind(this);
        this._input.type = "range";
    }
    /** Method for setting property _hint.
 * @param value Parameter represents string value of text to be used as placeholder.
 * */
    public set hint(value: string) {
        this._hintText = value;
        this._input.placeholder = this._hintText;
        this._hint.textContent = this._hintText;
        this.updateHint();
    }
    /** Acessor gets property _hint.
     * @returns Returns string text.
     * */
    public get hint(): string {
        return this._hintText;
    }
    /** Method for setting max range for property _input.
    * @param value Parameter represents string value of text representing range.
    * */
    public set max(value: string) {
        this._input.max = value;
    }
    /** Method for setting min range for property _input.
     * @param value Parameter represents string value of text representing range.
     * */
    public set min(value: string) {
        this._input.min = value;
    }
    /** Method for setting step for property _input.
     * @param value Parameter represents string value of text representing step input.
     * */
    public set step(value: string) {
        this._input.step = value;
    }
    /** Acessor gets max range of property _input.
    * @returns Returns string text.
    * */
    public get max(): string {
        return this._input.max;
    }
    /** Acessor gets min range of property _input.
    * @returns Returns string text.
    * */
    public get min(): string {
        return this._input.min;
    }
    /** Acessor gets step of property _input.
    * @returns Returns string text.
    * */
    public get step(): string {
        return this._input.step;
    }
    /** Accessor to get callback property.
      * @returns Returns the property responsible for callback on click operation
      * */
    public get onChange(): OnChangeCallback {
        return this._onChangeCallback;
    }
    /** Method for setting property _onClickCallback
   * @param value Method parameter represnts onClickCallback property
   * */
    public set onChange(value: OnChangeCallback) {
        this._onChangeCallback = value;
    }
    /** Method that invokes event listener of when the contents of Input instance is changed. Responsive element that visually indicates to user change in text. 
    * @returns Nothing (return in the definition of the property)
    * */
    public onModifiedChange(): void {
        if (this.modified) {
            Core.addClass(this.element, "Modified");
        } else {
            Core.removeClass(this.element, "Modified");
        }
    }
    /** Method that invokes event listener of when the contents of Input instance is readonly. Responsive element that visually indicates text is readonly. 
  * @returns Nothing (return in the definition of the property)
  * */
    public onReadonlyChange(): void {
        if (this.readonly) {
            Core.addClass(this.element, "Readonly");
            this._input.readOnly = true;
        } else {
            Core.removeClass(this.element, "Readonly");
            this._input.readOnly = false;
        }
    }
    /** Method not used*/
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }


    /** Method updates placeholder if null
  * */
    private updateHint() {
        if (!Utils.isNullOrWhitespace(this._text)) {
            Core.addClass(this._hint, "Has-And-Text");
        } else {
            Core.removeClass(this._hint, "Has-And-Text");
        }
    }
    /** Method that invoked event listener of when the contents of Inputs are handled on callback. 
  * @returns Nothing (return in the definition of the property)
  * */
    private onInputHandler(): void {
        this._text = this._input.value;
        if (this._onChangeCallback !== undefined) {
            this._onChangeCallback(this._text);
        }
        this.updateHint();
    }
    /** Method that invoked event listener of when to implement Focus. 
 * @returns Nothing (return in the definition of the property)
 * */
    private onFocusHandler(): void {
        Core.addClass(this.element, "Focused");
    }
    /** Method that invoked event listener of when to implement Blur. 
        * @returns Nothing (return in the definition of the property)
        * */
    private onBlurHandler(): void {
        Core.removeClass(this.element, "Focused");
    }
}
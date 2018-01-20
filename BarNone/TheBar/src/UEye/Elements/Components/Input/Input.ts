import Core from "UEye/Elements/Core/Core";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
/**
 *  Represents interactive element Input. This component is editable and takes text (strings) input.
 */
export default class Input extends BaseComponent {
     /**Represents HTMLElement as content of the input*/
    private _content: HTMLElement;
      /**Represents HTMLElement as placeholder that describes what input user needs to fill*/
    private _hint: HTMLElement;
      /**Represents HTMLInputElement of the input */
    protected _input: HTMLInputElement;
      /**Represents actions to clear or save input */
    private _action: HTMLElement;
    /**Represents placeholder as text*/
    private _hintText: string;
     /**Represents placeholder as text*/
    private _text: string;
	/**  Represents event listner that is called when even occurs.*/
    private _onChangeCallback: OnChangeCallback;
    /** Constructor intializes and defines the Video component as an HTMLElement tag named UEye-Input. 
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Input type.
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Input");

        this._content = Core.create("div", this.element, "Content");
        this._action = Core.create("div", this.element, "Action");
        
        this._hint = Core.create("div", this._content, "Hint");
        this._input = Core.create("input", this._content, "Input") as HTMLInputElement;

        this._input.oninput = this.onInputHandler.bind(this);
        this._input.onfocus = this.onFocusHandler.bind(this);
        this._input.onblur = this.onBlurHandler.bind(this);
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
    /** Accessor to get placeholder text of _hint property.
     * @returns Returns string text.
     * */
    public get hint(): string {
        return this._hintText;
    }
    /** Method for setting property _text.
     * @param value Parameter represents string value of text to be used as input text.
     * */
    public set text(value: string) {
        this._text = value;
        this._input.value = value;
        this.updateHint();
    }
      /** Accessor to get text of _text property.
     * @returns Returns string text.
     * */
    public get text(): string {
        return this._text;
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
        if(this.modified) {
            Core.addClass(this.element, "Modified");
        } else {
            Core.removeClass(this.element, "Modified");
        }
    }
       /** Method that invokes event listener of when the contents of Input instance is readonly. Responsive element that visually indicates text is readonly. 
     * @returns Nothing (return in the definition of the property)
     * */
    public onReadonlyChange(): void {
        if(this.readonly) {
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
        if(!Utils.isNullOrWhitespace(this._text)) {
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
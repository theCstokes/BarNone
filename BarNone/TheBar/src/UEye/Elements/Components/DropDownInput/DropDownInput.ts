import Core from "UEye/Elements/Core/Core";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import { IListItem, OnSelectCallback } from "UEye/Elements/Core/EventCallbackTypes";
import List from "UEye/Elements/Components/List/List";
import ControlTypes from "UEye/ControlTypes";
import StringUtils from "UEye/Core/StringUtils";
import DropDownListItem from "UEye/Elements/Components/DropDownListItem/DropDownListItem";

/**
 *  Represents interactive element Input. This component is editable and takes text (strings) input.
 */
export default class DropDownInput extends BaseComponent {
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
    private _onSelectCallback: OnSelectCallback;

    private e_inputArea: HTMLElement;
    private e_dropDown: HTMLElement;
    private c_list: List;
    private _items: any[];

    private _open: boolean;

    /** 
     * Constructor initializes and defines the Video component as an HTMLElement tag named UEye-Input. 
     * @param parent - Represents properties of the current element as an HTMLElement.
     * @returns Returns a Input type.
     */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Drop-Down-Input");

        this.e_inputArea = Core.create("div", this.element, "Input-Area");
        this.e_dropDown = Core.create("div", this.element, "Holder");
        this.e_dropDown.onclick = (e) => {
            e.stopPropagation();
        }

        this._content = Core.create("div", this.e_inputArea, "Content");
        this._action = Core.create("div", this.e_inputArea, "Action fa fa-caret-left");

        this.c_list = new List(this.e_dropDown);
        this.c_list.style = ControlTypes.DropDownListItem;
        this.c_list.isSelectionList = true;
        this.c_list.onSelect = this._onSelectHandler.bind(this);

        this._hint = Core.create("div", this._content, "Hint");
        this._input = Core.create("input", this._content, "Input") as HTMLInputElement;
        this._input.readOnly = true;

        // this.element.tabIndex = 0;
        document.addEventListener("click", (e) => {
            if (!this.element.contains(e.target as Node)) {
                this.onBlurHandler();
            }
        }, false);
        this.element.onfocus = this.onFocusHandler.bind(this);
        this.element.onblur = this.onBlurHandler.bind(this);
        this.element.onclick = this._onOpenHandler.bind(this);
    }
    /** Method for setting property _hint.
     * @param value Parameter represents string value of text to be used as placeholder.
     * */
    public set hint(value: string) {
        this._hintText = value;
        this._renderState();
    }
    /** Accessor to get placeholder text of _hint property.
     * @returns Returns string text.
     * */
    public get hint(): string {
        return this._hintText;
    }

    /** 
     * Accessor to get text of _text property.
     * @returns Returns string text.
     */
    public get text(): string {
        return this._text;
    }

    public set items(value: any[]) {
        if (this._items !== value) {
            this._items = value;
            this.c_list.items = value;
            this.selected = undefined;
        }
        console.log(value)
    }
    public get items(): any[] {
        return this._items;
    }

    public get selected(): any {
        return this.c_list.selected;
    }
    public set selected(value: any) {
        this.c_list.selected = value;
        if (this.c_list.selected === undefined) {
            this._text = "";
        } else {
            this._text = this.c_list.selected.name;
        }
        this._input.value = this._text;
        this._renderState();
    }

    /** Accessor to get callback property.
   * @returns Returns the property responsible for callback on click operation
   * */
    public get onSelect(): OnSelectCallback {
        return this._onSelectCallback;
    }
    /** Method for setting property _onClickCallback
   * @param value Method parameter represnts onClickCallback property
   * */
    public set onSelect(value: OnSelectCallback) {
        this._onSelectCallback = value;
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
    /** 
		 * Method that invokes event listener of when the contents of Input instance is readonly. Responsive element that visually indicates text is readonly. 
		 * @returns Nothing (return in the definition of the property)
		 */
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

    /** 
		 * Method that invoked event listener of when to implement Focus. 
		 * @returns Nothing (return in the definition of the property)
		 */
    private onFocusHandler(): void {
        Core.addClass(this.e_inputArea, "Focused");
        Core.addClass(this.e_dropDown, "Focused");
        // this._open = true;
        // this._renderState();
    }
    /** Method that invoked event listener of when to implement Blur. 
        * @returns Nothing (return in the definition of the property)
        * */
    private onBlurHandler(): void {
        Core.removeClass(this.e_inputArea, "Focused");
        Core.removeClass(this.e_dropDown, "Focused");
        this._open = false;
        this._renderState();
    }

    private _onOpenHandler(): void {
        this._open = !this._open;
        this._renderState();
    }

    private _onSelectHandler(data: IListItem) {
        var item = this.items.find(x => x.id === data.id);
        if (item === null) return;

        this._open = false;
        this._text = item.name;
        this._input.value = this._text;
        this._renderState();

        if (this._onSelectCallback !== undefined) {
            this._onSelectCallback(data);
        }
    }

    private _renderState() {
        this._input.placeholder = this._hintText;
        this._hint.textContent = this._hintText;

        if (this._open) {
            Core.replaceClass(this._action, "fa-caret-left", "fa-caret-down");
            Core.addClass(this.element, "Show-Drop-Down");
            this.e_dropDown.style.top = StringUtils.format(
                "{0}px", (this.element.offsetTop + this.element.offsetHeight + 5));
            this.e_dropDown.style.width = StringUtils.format(
                "{0}px", this.element.offsetWidth - 15);
        } else {
            Core.replaceClass(this._action, "fa-caret-down", "fa-caret-left");
            Core.removeClass(this.element, "Show-Drop-Down");
        }

        if (!Utils.isNullOrWhitespace(this._text)) {
            Core.addClass(this._hint, "Has-And-Text");
        } else {
            Core.removeClass(this._hint, "Has-And-Text");
        }
    }
}
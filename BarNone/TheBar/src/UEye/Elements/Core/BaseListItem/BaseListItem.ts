import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";
/**Describes basic html elements of List Type objects. Allows for development of different styles of ListItem*/
export abstract class BaseListItem
    extends BaseComponent implements IListItem {

    /**Selected property defines if instance of list item is selected ot not */
    private _selected: boolean;
    /**Represents event listener for when item list is selected */
    private _onSelectCallback: OnSelectCallback;
    private _isSelectionList: boolean;

    /** Constructor makes basic HTMLElement for List item
	 * @param parent HTMLElement
	*/
    public constructor(parent: HTMLElement, ...styles: string[]) {
        super(parent, "UEye-List-Item", ...styles);

        this.element.onclick = this.onSelectCallback.bind(this);
    }

    public static create<TListItem extends BaseListItem>(init: Partial<TListItem>): TListItem {
        let obj = {};
        return Object.assign(obj, init) as  TListItem;
    }

    /** Accessor to get _select property.
    * @returns Returns boolean select property.
    * */
    public get selected(): boolean {
        return this._selected;
    }
    /**Method sets _select property
	 * @param value Represents boolean value 
	 */
    public set selected(value: boolean) {
        this._selected = value;
        if (this._selected) {
            Core.addClass(this.element, "Selected");
        } else {
            Core.removeClass(this.element, "Selected");
        }
    }
    /** Accessor to get event listener property.
   * @returns Returns boolean select property.
   * */
    public get onSelect(): OnSelectCallback {
        return this._onSelectCallback;
    }
    /**Method sets _onSelect property
    * @param value Represents event listener property
    */
    public set onSelect(value: OnSelectCallback) {
        this._onSelectCallback = value;
    }

    public set isSelectionList(value: boolean) {
        if (this._isSelectionList !== value) {
            this._isSelectionList = value;
            if (this._isSelectionList) {
                Core.addClass(this.element, "Selection-List");
            } else {
                Core.removeClass(this.element, "Selection-List");
            }
        }
    }
    public get isSelectionList(): boolean {
        return this._isSelectionList;
    }

    private onSelectCallback(): void {
        if (!this.canSelect()) return;

        this._selected = true;
        if (this._onSelectCallback !== undefined) {
            this._onSelectCallback(this);
        }
    }

    public abstract canSelect(): boolean;

    public onModifiedChange() {
        if (this.modified) {
            Core.addClass(this.element, "Modified");
        } else {
            Core.removeClass(this.element, "Modified");
        }
    }
}
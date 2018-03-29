import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import ComponentType from "UEye/Elements/Inflater/ComponentInflater";
import { OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { BaseView } from "UEye/Elements/Core/BaseView";

export interface IList<
    TListItem extends BaseListItem
    > {
    items: TListItem[];

    selected: TListItem | undefined;

    onSelect: OnSelectCallback;

    style: ComponentType;
}

/**
 *  Represents an unordered list of items. This component is rendered as a bulletless list and can have different styles of list items to suit different purposes.
 */
export default class List extends BaseComponent implements IList<any> {
    /**  Represents the List parent element of the unordered list (ul tg).  */
    private e_list: HTMLElement;

    private _view: BaseView;

    private _isSelectionList: boolean;

    /**  Represents an array of additional data that is mapped to corresponding _listElement items  */
    private _items: IListItem[]
    /**  Represents an array of the child elements contained in parent list (li tag).   */
    private e_listElements: HTMLElement[];
    /**  Represents the style of list elements (eg. List item with icon and text is defined as NavListItem,used for navigation) */
    private _style: ComponentType;
    /**  Represents event listener that is called when event of select occurs*/
    private _onSelectCallback: OnSelectCallback;
    /**  Represents one list item that is selected at any given point in time*/
    private _selected: BaseListItem | undefined;

    private _listItems: BaseListItem[];
    /** Constructor intializes and defines the List component as an HTMLElement tag named UEye-List (using Core.addClass). 
      * @param parent - Represents properties of the current element as an HTMLElement.
      * * @returns Returns List.   
      * */
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-List");
        this.onBindView.on(view => this._view = view);

        this.e_list = Core.create("ul", this.element, "Element-List");
    }
    /** Method for setting property _items and _listElements. Calls methods that deletes previos data stored in _listElements and _items properties and then replaces it with data from the method parameter.
     * @param value Method parameter represents list of data corresponding to _listElements
     * */

    public set items(value: IListItem[]) {
        this.destroyItems();
        this._items = value;
        this.refreshItems();
    }
    /** Accessor to get _items.
  * @returns Returns a list of data related to contents of the list component.
  * */
    public get items(): IListItem[] {
        return this._items;
    }
    /** Method sets the _style property.
     * @param value Represents the type of component style of the list items.
     * */
    public set style(value: ComponentType) {
        this._style = value;
    }
    /** Accessor to get _style.
    * @returns Returns the style type of the list's elements.
    * */
    public get style(): ComponentType {
        return this._style;
    }

    public get list(): HTMLElement {
        return this.e_list;
    }

    public set isSelectionList(value: boolean) {
        if (this._isSelectionList !== value) {
            this._isSelectionList = value;
            if (this._isSelectionList) {
                Core.addClass(this.element, "Selection-List");
                // Core.addClass(this._elementList, "Selection-List");
            } else {
                Core.removeClass(this.element, "Selection-List");
                // Core.removeClass(this._elementList, "Selection-List");
            }
        }
    }
    public get isSelectionList(): boolean {
        return this._isSelectionList;
    }

    /** Method that destroys all list elements of type HTMLElement in _listElements and the correponding elements in property _item
 
  * */
    private destroyItems() {
        if (this.e_listElements !== undefined) {
            this.e_listElements.forEach(listElement => {
                var parentNode = listElement.parentNode;
                if (parentNode !== null) {
                    parentNode.removeChild(listElement);
                }
            });
        }
    }
    /** Method that creates list elements of type HTMLElement in _listElemnts correponding to elements in property _item. This operation is carried out asynchrously and also includes selecting a base element (top of the list).
  
   * */
    private refreshItems() {
        this.e_listElements = [];
        this._listItems = []
        this._items.forEach(async element => {
            var listElement = Core.create("li", this.e_list, "Element");
            // var pipeline = new InflaterPipeline();
            var instance: BaseListItem = this._style.inflate(listElement, element as any, this._view) as BaseListItem;

            instance.isSelectionList = this.isSelectionList;

            // if (this._selected !== instance && instance.selected) {
            //     instance.selected = false;
            //     console.warn("Item is already selected");
            // } else 
            if (instance.selected) {
                this._selected = instance;
            }

            listElement.onclick = (e) => {
                this.onSelectHandler(instance);
            };
            this._listItems.push(instance);
            // TODO - add events to component.
            this.e_listElements.push(listElement);
        });
    }

    /** Method not used in this element
    * @returns Nothing(return part of property definition)
  * */

    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    /** Accessor to get _selected.
  * @returns Returns the selected list element in List component.
  * */
    public get selected(): IListItem | undefined {
        return this._selected;
    }
    public set selected(value: IListItem | undefined) {
        if (this._items === undefined) return;
        // Call the items setter with the new data to cause refresh.
        this.items = this._items.map(item => {
            if (value !== undefined && item.id === value.id) {
                item.selected = true;
            } else {
                item.selected = false;
            }
            return item;
        });

        if (value === undefined) {
            this._selected = undefined;
        } else {
            this._selected = this._listItems.find(item => item.id === value.id);
        }
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
    /** Method that returns event handler for onSelect operations.
         * @returns Returns Nothing
         * */
    private onSelectHandler(instance: BaseListItem): void {
        if (!instance.canSelect()) return;
        if (instance !== undefined) {
            if (instance.onSelect !== undefined) instance.onSelect(instance);
            if (this._onSelectCallback !== undefined) this._onSelectCallback(instance);
        }
    }
}
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback, OnSelectCallback, IListItem, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

class BreadcrumbItem implements IListItem {
    // private _onClickCallback: OnClickCallback;

    public id: number | string;

    public value: string;

    public onClick?: OnSelectCallback;

    // public get onClick(): OnClickCallback {
    //     return this._onClickCallback;
    // }
    // public set onClick(value: OnClickCallback) {
    //     this._onClickCallback = value;
    // }
}

/**
 *  Represent navigation control element Breadcrumb. This component is used as a navigational aid that describes navigation relation between different screens
 */
export default class Breadcrumb extends BaseComponent {


    /**  Represents the Breadcrumb parent unordered list (ul tag).  */
    private _crumbHolder: HTMLElement;
    /**  Represents an array of the Breadcrumb child elements contained in parent list (li tag).   */
    private _crumbElements: HTMLElement[];
    /**  Represents an array of additional data that is mapped to view of the corresponding screen.  */
    private _items: BreadcrumbItem[];
    private _crumbContent:HTMLElement;
    private _onSelectCallback: OnSelectCallback;

    /**  Represents event listner that is called when even occurs*/
    private _onClickCallback: OnClickCallback;


    /** Constructor intializes and defines the Breadcrumb component as an HTMLElement tag named UEye-Breadcrumb (using Core.addClass). 
     * @param parent - Represents properties of the current element as an HTMLElement. Required to generate element and return a valid element.
     * @returns Returns a list type (UEye-List). 
     * */
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Breadcrumb");
        this._crumbHolder = Core.create('ul', this.element, 'Crumb-Holder');
        this._items = [];
        this._crumbElements = [];
    }

    public get items(): BreadcrumbItem[] {
        return this._items;
    }
    public set items(value: BreadcrumbItem[]) {
        this._destroyItems();
        this._items = value;
        this._createItems();
    }
    /** Accessor to get callback property.
    * @returns Returns the property responsible for callback on click operation
    * */

    // public get onSelect(): OnSelectCallback {
    //     return this._onSelectCallback;
    // }
    // public set onSelect(value: OnSelectCallback) {
    //     this._onSelectCallback = value;
    // }

    public pop(): void {
        var lastEl = this._crumbElements.pop();
        if (lastEl !== undefined) {
            this._crumbHolder.removeChild(lastEl);
        }
        this._items.pop();
    }

    public get onClick(): any {
        return this._onClickCallback;
    }
    /** Method for setting property _onClickCallback
 * @param value Method parameter represnts onClickCallback property
 * */

    public set onClick(value: any) {
        this._onClickCallback = value;
    }
    /** Method that pushes on new element into queue that is _items property and records previous element in the list
* @param item Parameter represents the new item element that is being pushed into the list
* */

    public push(item: any): void {
        var lastEl = this._crumbElements[this._crumbElements.length - 1];
        if (lastEl !== undefined) {
            Core.addClass(lastEl, "Unselected");
        }

        this._pushItem(item, true);
    }

    /** Method not used in this element
     * @returns Nothing(return part of property definition)
   * */
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    /** Method that creates list elements of type HTMLElement in _crumbElemnts correponding to elements in property _item

     * */
    private _createItems() {
        this._crumbElements = this._items.map((item, idx) => {
            return this._pushItem(item, idx === (this._items.length - 1));

        });
    }

    /** Method that destroys all list elements of type HTMLElement in _crumbElemnts and the correponding elements in property _item
    
     * */
    private _destroyItems() {
        if (this._crumbElements !== undefined) {
            this._crumbElements.forEach(listElement => {
                var parentNode = listElement.parentNode;
                if (parentNode !== null) {
                    parentNode.removeChild(listElement);
                }
            });
            this._items = [];
            this._crumbElements = [];
        }
    }

    /** Method that pushes on new element into queue that is _crumbElements property and generates the appropriate HTML tag for the element
     * @param item Parameter represents the new item element that is being pushed into the list
     * @param select Boolean parameter represents "selection" style property of the element type
     * */
    private _pushItem(item: BreadcrumbItem, select: boolean) {
       
        var el = Core.create('li', this._crumbHolder, "Crumb");
         this._crumbContent= Core.create('div', el, "Content");
        this._crumbContent.innerHTML=item.value;
        if (!select) {
            Core.addClass(el, 'Unselected');
        }
      
        el.onclick = () => {
            this.onSelectHandler(item);
        };
        this._crumbElements.push(el);
        return el;
    }

    private onSelectHandler(data: BreadcrumbItem): void {
        // if (this._onSelectCallback !== undefined) {
        //     this._onSelectCallback(data);
        // }
        if (data.onClick !== undefined) {
            data.onClick(data);
// =======
//     /** Method that returns event handler for onClick operations.
//      * @returns Returns Nothing
//      * */
//     private onClickHandler(): void {
//         if (this._onClickCallback !== undefined) {
//             this._onClickCallback();
// >>>>>>> develop
        }
    }
}
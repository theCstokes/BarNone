import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
/**
 *  Represent navigation control element Breadcrumb. This component is used as a navigational aid that describes navigation relation between different screens
 */
export default class Breadcrumb extends BaseComponent {

    
    /**  Represents the Breadcrumb parent unordered list (ul tag).  */
    private _crumbHolder: HTMLElement;
     /**  Represents an array of the Breadcrumb child elements contained in parent list (li tag).   */
    private _crumbElements: HTMLElement[];
       /**  Represents an array of additional data that is mapped to view of the corresponding screen.  */
    private _items: any[];
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


 
     /** Accessor to get _items.
     * @returns Returns a list of data related to the screen
     * */
    public get items(): any[] {
        return this._items;
    }
     /** Method for setting property _items and _crumbElements. Calls methods that deletes previos data stored in _crumbElements and _items properties and then replaces it with data from the method parameter.
     * @param value Method parameter represents list of data corresponding to _crumbElement
     * */
    
    public set items(value: any[]) {
        this._destroyItems();
        this._items = value;
        this._createItems();
    }
     /** Accessor to get callback property.
     * @returns Returns the property responsible for callback on click operation
     * */

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
    private _pushItem(item: any, select: boolean) {
        var el = Core.create('li', this._crumbHolder, "Crumb");
        if (!select) {
            Core.addClass(el, 'Unselected');
        }
        el.textContent = item.value;
        el.onclick = this.onClickHandler.bind(this);
        return el;
    }
    /** Method that returns event handler for onClick operations.
     * @returns Returns Nothing
     * */
    private onClickHandler(): void {
        if (this._onClickCallback !== undefined) {
            this._onClickCallback();
        }
    }
    // #endregion
}
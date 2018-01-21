import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback, OnSelectCallback, IListItem, OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

class BreadcrumbItem /*implements IListItem*/ {
    // private _onClickCallback: OnClickCallback;
    
    // public id: number;

    public value: string;

    public onClick?: OnClickCallback;

    // public get onClick(): OnClickCallback {
    //     return this._onClickCallback;
    // }
    // public set onClick(value: OnClickCallback) {
    //     this._onClickCallback = value;
    // }
}

export default class Breadcrumb extends BaseComponent {
    // Private Field(s).
    private _crumbHolder: HTMLElement;
    private _crumbElements: HTMLElement[];
    private _items: BreadcrumbItem[];
    private _onSelectCallback: OnSelectCallback;

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

    public push(item: BreadcrumbItem): void {
        var lastEl = this._crumbElements[this._crumbElements.length - 1];
        if (lastEl !== undefined) {
            Core.addClass(lastEl, "Unselected");
        }
        
        this._pushItem(item, true);
    }

    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    private _createItems() {
        this._crumbElements = this._items.map((item, idx) => {
            return this._pushItem(item, idx === (this._items.length - 1));
        });
    }

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

    private _pushItem(item: BreadcrumbItem, select: boolean) {
        var el = Core.create('li', this._crumbHolder, "Crumb");
        if (!select) {
            Core.addClass(el, 'Unselected');
        }
        el.textContent = item.value;
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
            data.onClick();
        }
    }
}
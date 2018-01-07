import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnClickCallback } from "Vee/Elements/Core/BindTypes";

export default class Breadcrumb extends BaseComponent {
    // #region Private Field(s).
    private _crumbHolder: HTMLElement;
    private _crumbElements: HTMLElement[];
    private _items: any[];
    private _onClickCallback: OnClickCallback;
    // #endregion

    // #region Public Constructor(s).
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "Vee-Breadcrumb");

        this._crumbHolder = Core.create('ul', this.element, 'Crumb-Holder');
        this._items = [];
        this._crumbElements = [];
    }
    // #endregion

    // #region Public Property(s).
    public get items(): any[] {
        return this._items;
    }
    public set items(value: any[]) {
        this._destroyItems();
        this._items = value;
        this._createItems();
    }

    public get onClick(): any {
        return this._onClickCallback;
    }
    public set onClick(value: any) {
        this._onClickCallback = value;
    }
    // #endregion

    // #region Public Member(s).
    public push(item: any): void {
        var lastEl = this._crumbElements[this._crumbElements.length - 1];
        if (lastEl !== undefined) {
            Core.addClass(lastEl, "Unselected");
        }
        
        this._pushItem(item, true);
    }
    // #endregion

    // #region BaseComponent Implementation.
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    public onModifiedChange(): void {
        throw new Error("Method not implemented.");
    }
    public onReadonlyChange(): void {
        throw new Error("Method not implemented.");
    }
    public onErrorChange(): void {
        throw new Error("Method not implemented.");
    }
    // #endregion

    // #region Private Member(s).
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

    private _pushItem(item: any, select: boolean) {
        var el = Core.create('li', this._crumbHolder, "Crumb");
        if (!select) {
            Core.addClass(el, 'Unselected');
        }
        el.textContent = item.value;
        el.onclick = this.onClickHandler.bind(this);
        return el;
    }

    private onClickHandler(): void {
        if (this._onClickCallback !== undefined) {
            this._onClickCallback();
        }
    }
    // #endregion
}
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
// import ViewInflater from "UEye/Elements/Core/ViewInflater";
import { OnSelectCallback } from "UEye/Elements/Core/BindTypes";
import { BaseListItem } from "UEye/Elements/Core/BaseListItem/BaseListItem";
import ComponentType from "UEye/ComponentType";

export default class UEyeList extends BaseComponent {
    private _elementList: HTMLElement;

    private _items: any[]
    private _listElements: HTMLElement[];
    private _style: ComponentType;
    private _onSelectCallback: OnSelectCallback;
    private _selected: BaseListItem;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-List");
        // this.element.onclick = this.onSelectCallback.bind(this);

        this._elementList = Core.create("ul", this.element, "Element-List");
    }

    public set items(value: any[]) {
        this.destroyItems();
        this._items = value;
        this.refreshItems();
    }
    public get items(): any[] {
        return this._items;
    }

    public set style(value: ComponentType) {
        this._style = value;
    }
    public get style(): ComponentType {
        return this._style;
    }

    private destroyItems() {
        if (this._listElements !== undefined) {
            this._listElements.forEach(listElement => {
                var parentNode = listElement.parentNode;
                if (parentNode !== null) {
                    parentNode.removeChild(listElement);
                }
            });
        }
    }

    private refreshItems() {
        this._listElements = [];
        this._items.forEach(async element => {
            var listElement = Core.create("li", this._elementList, "Element");
            // var pipeline = new InflaterPipeline();
            var instance: BaseListItem = this._style.create(listElement, element) as BaseListItem;

            // if (this._selected !== instance && instance.selected) {
            //     instance.selected = false;
            //     console.warn("Item is already selected");
            // } else 
            if (instance.selected) {
                this._selected = instance;
            }

            listElement.onclick = (e) => {
                if (this._selected !== undefined) {
                    this._selected.selected = false;
                }
                instance.selected = true;
                this._selected = instance;
                this.onSelectCallback();
            };

            // TODO - add events to component.
            this._listElements.push(listElement);
        });
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
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

    public get selected(): any {
        return this._selected;
    }

    public get onSelect(): OnSelectCallback {
        return this._onSelectCallback;
    }
    public set onSelect(value: OnSelectCallback) {
        this._onSelectCallback = value;
    }

    private onSelectCallback(): void {
        if (this._onSelectCallback !== undefined) {
            this._onSelectCallback(this._selected);
        }
    }
}
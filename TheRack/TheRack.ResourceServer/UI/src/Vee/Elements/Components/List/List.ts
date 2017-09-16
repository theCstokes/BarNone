import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import ViewInflater from "Vee/Elements/Core/ViewInflater";

export default class UEyeList extends BaseComponent {
    private _elementList: HTMLElement;

    private _items: any[]
    private _style: any;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-List");

        this._elementList = Core.create("ul", this.element, "Element-List");
    }

    public set items(value: any[]) {
        this._items = value;
        this.refreshItems();
    }
    public get items(): any[] {
        return this._items;
    }

    public set style(value: any) {
        this._style = value;
    }
    public get style(): any {
        return this._style;
    }
    
    private refreshItems() {
        this._items.forEach(async element => {
            var listElement = Core.create("li", this._elementList, "Element");
            var instance = await ViewInflater.InflateByPath(this._style, listElement, element);
            // TODO - add events to component.
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
}
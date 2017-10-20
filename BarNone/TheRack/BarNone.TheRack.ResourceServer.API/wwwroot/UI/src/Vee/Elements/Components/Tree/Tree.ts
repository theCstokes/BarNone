import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnSelectCallback } from "Vee/Elements/Core/BindTypes";
import { BaseListItem } from "Vee/Elements/Core/BaseListItem/BaseListItem";
import InflaterPipeline from "Vee/Elements/Core/Inflater/InflaterPipeline";

export default class UEyeTree extends BaseComponent {
    private _elementList: HTMLElement;

    private _nodes: any[]
    private _treeElements: HTMLElement[];
    private _style: any;
    private _onSelectCallback: OnSelectCallback;
    private _selected: BaseListItem;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Tree");

        this._elementList = Core.create("ul", this.element, "Element-Tree");
    }

    public set nodes(value: any[]) {
        this.destroyItems();
        this._nodes = value;
        this.refreshNodes();
    }
    public get nodes(): any[] {
        return this._nodes;
    }

    public set style(value: any) {
        this._style = value;
    }
    public get style(): any {
        return this._style;
    }

    private destroyItems() {
        if (this._treeElements !== undefined) {
            this._treeElements.forEach(listElement => {
                var parentNode = listElement.parentNode;
                if (parentNode !== null) {
                    parentNode.removeChild(listElement);
                }
            });
        }
    }

    private refreshNodes() {
        this._treeElements = [];
        this._nodes.forEach(async element => {
            var listElement = Core.create("li", this._elementList, "Element");
            var pipeline = new InflaterPipeline();
            var instance: BaseListItem = await pipeline
                .inflateByPath(this._style, listElement, element);

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
            this._treeElements.push(listElement);
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
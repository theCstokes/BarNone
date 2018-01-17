import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";

export abstract class BaseListItem extends BaseComponent implements IListItem {
    private _selected: boolean;
    private _onSelectCallback: OnSelectCallback;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-List-Item");

        this.element.onclick = this.onSelectCallback.bind(this);
    }

    public get selected(): boolean {
        return this._selected;
    }
    public set selected(value: boolean) {
        this._selected = value;
        if (this._selected) {
            Core.addClass(this.element, "Selected");
        } else {
            Core.removeClass(this.element, "Selected");
        }
    }

    public get onSelect(): OnSelectCallback {
        return this._onSelectCallback;
    }
    public set onSelect(value: OnSelectCallback) {
        this._onSelectCallback = value;
    }

    private onSelectCallback(): void {
        this._selected = true;
        if (this._onSelectCallback !== undefined) {
            this._onSelectCallback(this);
        }
    }
}
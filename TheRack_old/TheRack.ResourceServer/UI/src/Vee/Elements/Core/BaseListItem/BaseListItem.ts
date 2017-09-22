import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnSelectCallback } from "Vee/Elements/Core/BindTypes";

export abstract class BaseListItem extends BaseComponent {
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
            this._onSelectCallback(this._selected);
        }
    }
}
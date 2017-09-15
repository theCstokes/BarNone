import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";

export abstract class BaseListItem extends BaseComponent {
    private _selected: boolean;
    
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-List-Item");
    }

    public set selected(value: any) {
        this._selected = value;
        if(this._selected) {
            Core.addClass(this.element, "Selected");
        } else {
            Core.removeClass(this.element, "Selected");
        }
    }
    public get selected(): any {
        return this._selected;
    }
}
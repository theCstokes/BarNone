import { BaseComponent} from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";


export default class SearchTag extends BaseComponent {
 
    // Private Element(s).
    private _tagElement: HTMLElement;

    private _iconElement: HTMLElement;
    // Private Field(s).
    private _title: string;

    public id: number | string;

    private _selected: boolean = false;

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Search-Tag");
        this._iconElement = Core.create('div', this._tagElement, 'fa', 'fa-times');   
    }

    public set title(value: string) {
        this._title = value;
    }
    public get title(): string {
        return this._title;
    }

    public set selected(value: boolean) {
        if (value !== this._selected) {
            this._selected = value;
            if (this._selected) {
                Core.addClass(this.element, "Selected");
            } else {
                Core.removeClass(this.element, "Selected");
            }
        }
    }
    public get selected(): boolean {
        return this._selected;
    }

  
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    
}
import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class Tab extends BaseContainer {

    // Private Element(s).
    private _contentElement: HTMLElement;

    // Private Field(s).
    private _title: string;
    private _selected: boolean = false;

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Tab");

        this._contentElement = Core.create("div", this.element, "Content");
        this.linkComponentContainer("content", this._contentElement);

        this.onShow.on(v => console.log("Show tab"));
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

    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    public get content(): any[] {
        return this.getComponentContainer("content");
    }
    
}
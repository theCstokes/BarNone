import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class Panel extends BaseContainer {

    // Region Private Member(s).
    private _topDockElement: HTMLElement;
    private _captionElement: HTMLElement
    private _modeElement: HTMLElement;
    private _contentElement: HTMLElement;
    private _bottomDockElement: HTMLElement;

    private _caption: string
    // End Region.

    // Public Constructor(s).
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Panel");

        this._topDockElement = Core.create("div", this.element, "Top-Dock");
        this._captionElement = Core.create("div", this._topDockElement, "Name");
        this._modeElement = Core.create("div", this._topDockElement, "Mode");
        this._contentElement = Core.create("div", this.element, "Content");
        this.linkComponentContainer("content", this._contentElement);
        this._bottomDockElement = Core.create("div", this.element, "Bottom-Dock");
        this.linkComponentContainer("bottomDock", this._bottomDockElement);
    }
    // End Region

    // Region Public Property(s).
    public set caption(value: any) {
        this._caption = value;
        this._captionElement.textContent = this._caption;
        if(!Utils.isNullOrWhitespace(this._caption)) {
            Core.addClass(this._topDockElement, "Has-Caption");
        } else {
            Core.removeClass(this._topDockElement, "Has-Caption");
        }
    }
    public get caption(): any {
        return this._caption;
    }

    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    public get content(): any[] {
        return this.getComponentContainer("content");
    }

    public set dockBottom(value: any[]) {
        this.setComponentContainer("dockBottom", value);
    }
    public get dockBottom(): any[] {
        return this.getComponentContainer("dockBottom");
    }
    // End Region

    // Region Protected Member(s).
    protected onModify(): void {
        
    }
    
    protected onReadonly(): void {
        
    }
    // End Region

    // Region Private Member(s).
    private renderMode(mode: boolean, flag: string) {
        if(mode) {
            Core.addClass(this._topDockElement, flag);
            this._modeElement.textContent = flag;
            Core.addClass(this._modeElement, flag);
        } else {
            Core.removeClass(this._topDockElement, flag);
            this._modeElement.textContent = "";
            Core.removeClass(this._modeElement, flag);
        }
    }

    public onModifiedChange(): void {
        this.renderMode(this.modified, "Modified");
    }
    public onReadonlyChange(): void {
        this.renderMode(this.readonly, "Readonly");
    }
    public onErrorChange(): void {
        throw new Error("Method not implemented.");
    }
}
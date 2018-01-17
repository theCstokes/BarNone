import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class Frame extends BaseContainer {
    private _globalDock: HTMLElement;
    private _contextDock: HTMLElement;
    private _navDock: HTMLElement;
    private _statusArea: HTMLElement;
    private _statusImageElement: HTMLImageElement;
    private _statusImageButtonElement: HTMLElement;
    private _statusTitleElement: HTMLElement;
    private _content: HTMLElement;

    private _statusImageSource: string;
    private _statusTitle: string;
    
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Frame");

        this._globalDock = Core.create("div", this.element, "Global-Dock");
        this.linkComponentContainer("globalDock", this._globalDock);
        
        this._contextDock = Core.create("div", this.element, "Context-Dock");
        this.linkComponentContainer("contextDock", this._contextDock);

        this._statusArea = Core.create("div", this.element, "Status-Area");
        var _statusImageArea = Core.create("div", this._statusArea, "Status-Image-Area");
        this._statusImageElement = Core.create("img", _statusImageArea, "Status-Image") as HTMLImageElement;
        this._statusTitleElement = Core.create("div", this._statusArea, "Status-Title");

        var statusImageHoverElement = Core.create("div", this._statusArea, "Status-Image-Hover");
        this._statusImageButtonElement = Core.create("div", statusImageHoverElement, "Status-Image-Button");
        this._statusImageButtonElement.textContent = "Test";

        this._navDock = Core.create("div", this.element, "Nav-Dock");
        this.linkComponentContainer("navDock", this._navDock);
        
        this._content = Core.create("div", this.element, "Content");
        this.linkScreenContainer("content", this._content);
    }
    
    public set globalDock(value: any[]) {
        this.setComponentContainer("globalDock", value);
    }
    public get globalDock(): any[] {
        return this.getComponentContainer("globalDock");
    }

    public set contextDock(value: any[]) {
        this.setComponentContainer("contextDock", value);
    }
    public get contextDock(): any[] {
        return this.getComponentContainer("contextDock");
    }

    public get statusTitle(): string {
        return this._statusTitle;
    }
    public set statusTitle(value: string) {
        if (this._statusTitle !== value) {
            this._statusTitle = value;
            this._statusTitleElement.textContent = this._statusTitle;
        }
    }

    public get statusImageSource(): string {
        return this._statusImageSource;
    }
    public set statusImageSource(value: string) {
        if (this._statusImageSource !== value) {
            this._statusImageSource = value;
            this._statusImageElement.src = this._statusImageSource;
        }
    }

    public set navDock(value: any[]) {
        this.setComponentContainer("navDock", value);
    }
    public get navDock(): any[] {
        return this.getComponentContainer("navDock");
    }

    public set content(value: any[]) {
        this.setScreenContainer("content", value);
    }
    public get content(): any[] {
        return this.getScreenContainer("content");
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
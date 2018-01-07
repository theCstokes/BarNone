import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class LoginFrame extends BaseContainer {
    private _backgroundImage: HTMLImageElement;
    private _content: HTMLElement;

    private _backgroundImageSource: string;
    
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Login-Frame");

        this._backgroundImage = Core.create("img", this.element, "Background") as HTMLImageElement;

        this._content = Core.create("div", this.element, "Content");
        this.linkComponentContainer("content", this._content);
    }
    
    public set content(value: any[]) {
        this.setScreenContainer("content", value);
    }
    public get content(): any[] {
        return this.getScreenContainer("content");
    }

    public get background(): string {
        return this._backgroundImageSource;
    }
    public set background(value: string) {
        if (this._backgroundImageSource !== value) {
            this._backgroundImageSource = value;
            this._backgroundImage.src = this._backgroundImageSource;
        }
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
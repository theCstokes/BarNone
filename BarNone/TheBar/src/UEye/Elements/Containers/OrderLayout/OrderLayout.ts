import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class OrderLayout extends BaseContainer {
    private _content: HTMLElement;

    private _rightToLeft: boolean;
    
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Order-Layout");
        // this._content = UEyeCore.create("div", this.element);
        this.linkComponentContainer("content", this.element);
    }

    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    public get content(): any[] {
        return this.getComponentContainer("content");
    }

    public set rightToLeft(value: boolean) {
        this._rightToLeft = value;

        if(this._rightToLeft) {
            Core.addClass(this.element, "Right-To-Left");
        } else {
            Core.removeClass(this.element, "Right-To-Left");
        }
    }
    public get rightToLeft(): boolean {
        return this._rightToLeft;
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
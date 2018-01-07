import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export default class Column extends BaseContainer {
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Column");

        // this._columnElements = UEyeCore.create("div", this.element, "Column-Elements");
        this.linkComponentContainer("content", this.element);
    }

    public set content(value: any[]) {
        this.setComponentContainer("content", value);
    }
    public get content(): any[] {
        return this.getComponentContainer("content");
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
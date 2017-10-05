import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import Core from "Vee/Elements/Core/Core";

export default class ColumnLayout extends BaseContainer {
    
    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Column-Layout");

        this.linkComponentContainer("columns", this.element);
    }

    public set columns(value: any[]) {
        this.setComponentContainer("columns", value);
    }
    public get columns(): any[] {
        return this.getComponentContainer("columns");
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
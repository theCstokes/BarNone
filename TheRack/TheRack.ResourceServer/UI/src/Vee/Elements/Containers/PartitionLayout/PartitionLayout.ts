import { BaseContainer } from "Vee/Elements/Core/BaseContainer/BaseContainer";
import Core from "Vee/Elements/Core/Core";

export default class PartitionLayout extends BaseContainer {
    private _leftSide: HTMLElement;
    private _holder: HTMLElement;
    private _rightSide: HTMLElement;
    
    public constructor(parent: HTMLElement) {
        super (parent);
        Core.addClass(this.element, "UEye-Partition-Layout");
        
        this._leftSide = Core.create("div", this.element, "Left-Side");
        this.linkComponentContainer("leftSide", this._leftSide);

        this._holder = Core.create("div", this.element, "Holder");

        this._rightSide = Core.create("div", this._holder, "Right-Side");
        this.linkScreenContainer("rightSide", this._rightSide);
    }
    
    public set leftSide(value: any[]) {
        this.setComponentContainer("leftSide", value);
    }
    public get leftSide(): any[] {
        return this.getComponentContainer("leftSide");
    }

    public set rightSide(value: any[]) {
        this.setScreenContainer("rightSide", value);
    }
    public get rightSide(): any[] {
        return this.getScreenContainer("rightSide");
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
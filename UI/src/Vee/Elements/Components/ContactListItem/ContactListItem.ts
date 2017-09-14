import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";

export default class ContactListItem extends BaseComponent {
    private _nameElement: HTMLElement;

    private _name: string;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Contact-List-Item");

        this._nameElement = Core.create("div", this.element, "Name");

    }

    public set name(value: any) {
        this._name = value;
        this._nameElement.textContent = this._name;
    }
    public get name(): any {
        return this._name;
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
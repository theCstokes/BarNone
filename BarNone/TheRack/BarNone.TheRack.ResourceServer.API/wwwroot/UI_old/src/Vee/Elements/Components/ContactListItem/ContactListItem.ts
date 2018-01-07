import Core from "Vee/Elements/Core/Core";
import { BaseListItem } from "Vee/Elements/Core/BaseListItem/BaseListItem";

export default class ContactListItem extends BaseListItem {
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
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
}
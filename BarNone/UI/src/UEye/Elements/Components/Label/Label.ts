import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";

export default class Label extends BaseComponent {
    private _text: string;

    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Label");
    }

    public set text(value: string) {
        this._text = value;
        this.element.textContent = this._text;
    }
    public get text(): string {
        return this._text;
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
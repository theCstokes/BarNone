import { BaseComponent} from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
 import IconButton from "UEye/Elements/Components/IconButton/IconButton";

export default class SearchTag extends BaseComponent {
 
    // Private Element(s).

    private _textElement: HTMLElement;
    // Private Field(s).
    private _text: string;

    private _closeButton: IconButton;

    private _selected: boolean;

    private _onClickCallback: OnClickCallback;


    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Search-Tag");
        this._textElement = Core.create('div', this.element, 'Text');   
        this._closeButton = new IconButton(this.element);
        this._closeButton.icon = "fa-times";
        this._closeButton.onClick = this.onClickHandler.bind(this);
    }

    public set text(value: string) {
        this._text = value;
        this._textElement.textContent=this._text;
    }
    public get text(): string {
        return this._text;
    }
    public get closeButton(): any{
        return this.closeButton;
    }
    public set selected(value: boolean) {
        this._selected = value;
    }
  
    public get selected(): boolean {
        return this._selected;
    }

  
    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }

     private onClickHandler(): void {
        if (this._onClickCallback !== undefined) {
			this._onClickCallback();
		}
    }

    public get onClick(): OnClickCallback {
        return this._onClickCallback;
    }
    public set onClick(value: OnClickCallback) {
        this._onClickCallback = value;
    }
    
}
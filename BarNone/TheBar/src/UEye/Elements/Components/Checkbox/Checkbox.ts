import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

export default class Checkbox extends BaseComponent {

    private _textElement: HTMLElement;
    private _text: string;
    private _checkElement: HTMLElement;
    private _iconElement: HTMLElement;
    private _selected: boolean;
    private _onClickCallback: OnClickCallback;

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Checkbox");
        this._textElement = Core.create('div', this.element, 'Text');
        this._checkElement = Core.create('div', this.element, 'Check');
        this._iconElement = Core.create('div', this._checkElement, 'Check-Icon', 'fa');
        this._checkElement.onclick = this.onClickHandler.bind(this);
        this._selected=false;
    }
    
    
    public set text(value: string) {
        this._text=value;
        this._textElement.textContent=value;
    }


    public onEnabledChange(): void {
		throw new Error("Method not implemented.");
    }
    private onClickHandler(): void {
        if(this._selected==true){
            this._selected=false;
            Core.removeClass(this._checkElement, 'Selected');
            Core.removeClass(this._iconElement, 'fa-check');
        }else{
            this._selected=true;
            Core.addClass(this._checkElement, 'Selected');
            Core.addClass(this._iconElement, 'fa-check');
        }
    }

    public get onClick(): OnClickCallback {
        return this._onClickCallback;
    }
    public set onClick(value: OnClickCallback) {
        this._onClickCallback = value;
    }
}
 import Core from "Vee/Elements/Core/Core";
import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import { OnChangeCallback } from "Vee/Elements/Core/BindTypes";

export default class Range extends BaseComponent {
    private _content: HTMLElement;
    private _hint: HTMLElement;
    private _input: HTMLInputElement;
    private _action: HTMLElement;

    private _hintText: string;
    private _text: string;
    private _onChangeCallback: OnChangeCallback;

    public constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Range");

         this._content = Core.create("div", this.element, "Content");
        this._action = Core.create("div", this.element, "Action");
        
        this._hint = Core.create("div", this._content, "Hint");
        this._input = Core.create("input", this._content, "Input") as HTMLInputElement;

        this._input.oninput = this.onInputHandler.bind(this);
        this._input.onfocus = this.onFocusHandler.bind(this);
        this._input.onblur = this.onBlurHandler.bind(this);
        this._input.type="range";
    }

    public set hint(value: string) {
        this._hintText = value;
        this._input.placeholder = this._hintText;
        this._hint.textContent = this._hintText;
        this.updateHint();
    }
    public get hint(): string {
        return this._hintText;
    }

    public set max(value: string) {
        this._input.max=value;
    }

    public set min(value: string) {
        this._input.min=value;
    }

    public set step(value: string) {
        this._input.step=value;
    }

    public get max(): string {
        return this._input.max;
    }
    public get min(): string {
        return this._input.min;
    }    

    public get step(): string {
        return this._input.step;
    }


    public get onChange(): OnChangeCallback {
        return this._onChangeCallback;
    }
    public set onChange(value: OnChangeCallback) {
        this._onChangeCallback = value;
    }
    
    public onModifiedChange(): void {
        if(this.modified) {
            Core.addClass(this.element, "Modified");
        } else {
            Core.removeClass(this.element, "Modified");
        }
    }
    public onReadonlyChange(): void {
        if(this.readonly) {
            Core.addClass(this.element, "Readonly");
            this._input.readOnly = true;
        } else {
            Core.removeClass(this.element, "Readonly");
            this._input.readOnly = false;
        }
    }
    public onErrorChange(): void {
        throw new Error("Method not implemented.");
    }

    // Region Private Member(s).
    private updateHint() {
        if(!Utils.isNullOrWhitespace(this._text)) {
            Core.addClass(this._hint, "Has-And-Text");
        } else {
            Core.removeClass(this._hint, "Has-And-Text");
        }
    }
    
    private onInputHandler(): void {
        this._text = this._input.value;
        if (this._onChangeCallback !== undefined) {
            this._onChangeCallback(this._text);
        }
        this.updateHint();
    }

    private onFocusHandler(): void {
        Core.addClass(this.element, "Focused");
    }

    private onBlurHandler(): void {
        Core.removeClass(this.element, "Focused");
    }

    public onEnabledChange(): void {
		if (this.enabled) {
			Core.removeClass(this.element, "disabled");
		} else {
			Core.addClass(this.element, "disabled");
		}
	}
}
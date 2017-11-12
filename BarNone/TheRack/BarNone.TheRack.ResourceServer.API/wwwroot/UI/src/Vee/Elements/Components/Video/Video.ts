import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnChangeCallback } from "Vee/Elements/Core/BindTypes";

export default class Video extends BaseComponent {
    private _content: HTMLElement;
    private _hint: HTMLElement;
    private _canvas: HTMLCanvasElement;
    private _hintText: string;
    private _text: string;
    private _context: CanvasRenderingContext2D | null;
    private _src: string;
   /** private _frames: frameJSON[]; */
    private _currentFrame: number;

    private _onChangeCallback: OnChangeCallback;
   
    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Video");
        this._content = Core.create("div", this.element, "Content");
        this._hint = Core.create("div", this._content, "Hint");
        this._canvas = Core.create("canvas", this._content, "canvas") as HTMLCanvasElement;
        this._currentFrame = 0;
        this._context = this._canvas.getContext('2d'); 
        }
    public set text(value: string) {
        this._text = value;
        this.element.textContent = this._text;
    }
    public set drawFrame(value:any){
       
        if(this._context!=null){
            this._context.fillStyle='#32CD32';
            this._context.beginPath();
            for(var frame in value){
            this._context.lineTo(value.x, value.y);
            }
            this._context.closePath();
        }
        
       
          


    }
    public get text(): string {
        return this._text;
    }

    public get height(): number{
        return this._canvas.height;
    }

    public get width(): number{
        return this._canvas.width;
    }

    public get onChange(): OnChangeCallback {
        return this._onChangeCallback;
    }
    public set onChange(value: OnChangeCallback) {
        this._onChangeCallback = value;
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
		if (this.enabled) {
			Core.removeClass(this.element, "disabled");
		} else {
			Core.addClass(this.element, "disabled");
		}
	}
}

import { BaseComponent } from "Vee/Elements/Core/BaseComponent/BaseComponent";
import Core from "Vee/Elements/Core/Core";
import { OnChangeCallback } from "Vee/Elements/Core/BindTypes";

type FrameData = { x1: number, y1: number, x2: number, y2: number };

export default class Video extends BaseComponent {
    private _content: HTMLElement;
    private _hint: HTMLElement;
    private _canvas: HTMLCanvasElement;
    private _hintText: string;
    private _text: string;
    private _context: CanvasRenderingContext2D;
    private _src: string;
    /** private _frames: frameJSON[]; */
    private _currentFrame: number;

    private _onChangeCallback: OnChangeCallback;

    constructor(parent: HTMLElement) {
        super(parent);
        Core.addClass(this.element, "UEye-Video");
        this._content = Core.create("div", this.element, "Content");
        // this._hint = Core.create("div", this._content, "Hint");
        this._canvas = Core.create("canvas", this._content, "Canvas") as HTMLCanvasElement;
        this._currentFrame = 0;

        var c = this._canvas.getContext('2d');
        if (c !== null) {
            this._context = c;
        }
    }

    public set text(value: string) {
        this._text = value;
        this.element.textContent = this._text;
    }
    public get text(): string {
        return this._text;
    }

    public set frameData(value: FrameData[]) {
        if (this._context != null) {
            this._context.lineWidth = 5;

            value.forEach(frame => {
                this._context.beginPath();
                this._context.moveTo(frame.x1, frame.y1);
                this._context.lineTo(frame.x2, frame.y2);
                this._context.strokeStyle = "green";
                this._context.stroke();
                this._context.closePath();
            });

            value.forEach(frame => {
                this._context.beginPath();
                this._context.arc(frame.x1, frame.y1, 4, 0, 2 * Math.PI);
                this._context.strokeStyle = "blue";
                this._context.stroke();
                this._context.fillStyle = 'blue';
                this._context.fill();
                this._context.closePath();

                this._context.beginPath();
                this._context.arc(frame.x2, frame.y2, 4, 0, 2 * Math.PI);
                this._context.strokeStyle = "blue";
                this._context.stroke();
                this._context.fillStyle = 'blue';
                this._context.fill();
                this._context.closePath();
            });
        }
    }

    public get height(): number {
        return this._canvas.height;
    }

    public get width(): number {
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

    public onShow(): void {
        console.warn("error");
    }
}

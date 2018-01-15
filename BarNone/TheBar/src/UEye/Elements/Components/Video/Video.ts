import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

type FrameData = { x1: number, y1: number, x2: number, y2: number };

export default class Video extends BaseComponent {
    
    private _canvas: HTMLCanvasElement;
    private _context: CanvasRenderingContext2D;
    private _video: HTMLVideoElement;
    private _source: HTMLSourceElement;
    private _src: string;

    constructor(parent: HTMLElement) {
        super(parent, "UEye-Video");
        
        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
        this._canvas.width = this._canvas.offsetWidth;
        this._canvas.height = this._canvas.offsetHeight;
        
        this._video = Core.create("video", this.element, "Video") as HTMLVideoElement;
        this._video.width = 200;
        this._video.controls = true;
        
        this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
        this._source.type = "video/mp4";
        
        var c = this._canvas.getContext('2d');
        if (c !== null) {
            this._context = c;
        }

        this._canvas.onmouseover = (e) => {
            var rect = this._canvas.getBoundingClientRect();
            var x = e.clientX - rect.left;
            var y = e.clientY - rect.top;

            this._context.beginPath();
            this._context.arc(x, y, 4, 0, 2 * Math.PI);
            this._context.strokeStyle = "yellow";
            this._context.stroke();
            this._context.fillStyle = 'yellow';
            this._context.fill();
            this._context.closePath();
        };

        var cw = Math.floor(this._canvas.clientWidth / 100);
        var ch = Math.floor(this._canvas.clientHeight / 100);
        this._canvas.width = cw;
        this._canvas.height = ch;

        this._video.addEventListener('play', () => {
            this.draw(cw, ch);
        }, false);
    }

    public set frameData(value: FrameData[]) {
        if (this._context != null) {
            this._context.clearRect(0, 0, this._canvas.width, this._canvas.height);
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

    private draw(w: number, h: number) {
        if (this._video.paused || this._video.ended) return false;
        // this._context.drawImage(this._video, 0, 0, w, h);
        setTimeout(this.draw, 20, w, h);

        var x = 20;
        var y = 20;

        this._context.beginPath();
        this._context.arc(x, y, 4, 0, 2 * Math.PI);
        this._context.strokeStyle = "yellow";
        this._context.stroke();
        this._context.fillStyle = 'yellow';
        this._context.fill();
        this._context.closePath();

        return true;
    }

    public get src(): string {
        return this._src;
    }
    public set src(value: string) {
        if (this._src !== value) {
            this._src = value;
            this._source.src = this._src;
        }
    }

    public play(): void {
        this._video.play();
    }

    // public get onChange(): OnChangeCallback {
    //     return this._onChangeCallback;
    // }
    // public set onChange(value: OnChangeCallback) {
    //     this._onChangeCallback = value;
    // }

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

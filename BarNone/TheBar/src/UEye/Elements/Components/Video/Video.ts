import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";

type LineData = { x1: number, y1: number, x2: number, y2: number };
type FrameData = LineData[];

export default class Video extends BaseComponent {

    private _canvas: HTMLCanvasElement;
    private _context: CanvasRenderingContext2D;
    private _video: HTMLVideoElement;
    private _source: HTMLSourceElement;
    private _controlBar: HTMLElement;
    private _actionButton: HTMLElement;
    private _slider: HTMLElement;
    private _bar: HTMLElement;
    private _src: string;
    private _frameDataList: FrameData[];

    constructor(parent: HTMLElement) {
        super(parent, "UEye-Video");

        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;

        this._video = Core.create("video", this.element, "Video") as HTMLVideoElement;
        this._video.width = this._canvas.width;

        this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
        this._source.type = "video/mp4";

        this._controlBar = Core.create("div", this.element, "Control-Bar");

        this._actionButton = Core.create("div", this._controlBar, "Action-Button fa fa-pause");
        this._actionButton.onclick = this._onActionHandel.bind(this);

        this._slider = Core.create("div", this._controlBar, "Slider");
        this._bar = Core.create("div", this._slider, "Bar");

        this._slider.onclick = (e) => {
            console.log(e);
            this._bar.style.width = e.offsetX + "px";
            var percent = (e.offsetX / this._slider.offsetWidth);

            // this._video.seekable.

            this._video.currentTime = (this._video.duration * percent);
        };

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

        this._video.addEventListener('play', () => {
            this.draw(this._canvas.width, this._canvas.height);
        }, false);

        // this._video.addEventListener('timeupdate', () => {
        //     var percent = (this._video.currentTime / this._video.duration);
        //     this._bar.style.width = (this._slider.offsetWidth * percent) + "px";
        // }, false);

        //     this._video.addEventListener("loadedmetadata", function() {
        //         this.currentTime = 3;
        //    }, false);
    }

    public set frameData(value: FrameData[]) {
        if (this._frameDataList !== value) {
            this._frameDataList = value;
        }
    }

    public get height(): number {
        return this._canvas.height;
    }

    public get width(): number {
        return this._canvas.width;
    }

    private draw(w: number, h: number) {
        if (this._video.paused || this._video.ended) {
            return;
        }
        // {
        //     this._video.pause();
        //     this._video.currentTime = 3;
        //     this._video.play();
        //     return;
        // }

        var percent = (this._video.currentTime / this._video.duration);

        this._context.drawImage(this._video, 0, 0, w, h);

        if (this._frameDataList !== undefined) {
            var frameIndex = Math.round((this._frameDataList.length - 1) * percent);
            var frameData = this._createFrame(this._frameDataList[frameIndex], w, h);
            // var bit = createImageBitmap(frameData);
            // this._context.putImageData(frameData, 0, 0, w, h);
        }

        this._bar.style.width = (this._slider.offsetWidth * percent) + "px";
        setTimeout(this.draw.bind(this), 20, w, h);
    }

    public get src(): string {
        return this._src;
    }
    public set src(value: string) {
        if (this._src !== value) {
            this._src = value;

            this._video.pause();

            this._video.removeChild(this._source);
            this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
            this._source.type = "video/mp4";
            this._source.src = this._src;

            this._video.load();
            this._video.currentTime = 0;
            this._video.play();
        }
        if (this._src !== undefined) {
            Core.addClass(this._source, "Visible");
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

    private _createFrame(frame: FrameData, w: number, h: number): ImageData {
        // if (this._context != null) {
        var context = this._context;
        // context.clearRect(0, 0, this._canvas.width, this._canvas.height);
        context.lineWidth = 5;

        frame.forEach(line => {
            context.beginPath();
            context.moveTo(line.x1, line.y1);
            context.lineTo(line.x2, line.y2);
            context.strokeStyle = "green";
            context.stroke();
            context.closePath();
        });

        frame.forEach(line => {
            context.beginPath();
            context.arc(line.x1, line.y1, 4, 0, 2 * Math.PI);
            context.strokeStyle = "blue";
            context.stroke();
            context.fillStyle = 'blue';
            context.fill();
            context.closePath();

            context.beginPath();
            context.arc(line.x2, line.y2, 4, 0, 2 * Math.PI);
            context.strokeStyle = "blue";
            context.stroke();
            context.fillStyle = 'blue';
            context.fill();
            context.closePath();
        });

        return context.getImageData(0, 0, w, h);
        // }
    }

    private _onActionHandel() {
        if (this._video.paused || this._video.ended) {
            // Pause put in play more.
            this._video.play();
            Core.replaceClass(this._actionButton, "fa-play", "fa-pause");
        } else {
            // Play put in pause more.
            this._video.pause();
            Core.replaceClass(this._actionButton, "fa-pause", "fa-play");
        }
    }
}

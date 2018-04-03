declare function require(moduleNames: string): any;
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { BaseView } from "UEye/Elements/Core/BaseView";
// Stub a local require for the ts compiler.
const moment = require("momentjs");
/**Type Definition: for LineData to be drawn on canvas */
type LineData = {
    // id: number;
    /**x-coordinate for point 1*/
    x1: number,
    /**y-coordinate for point 1*/
    y1: number,
    /**z-coordinate for point 1*/
    z1: number,
    /**x-coordinate for point 2*/
    x2: number,
    /**y-coordinate for point 2*/
    y2: number,
    /**z-coordinate for point 2 */
    z2: number,
    /**Time Stamp corresponding to time in frame */
    timeStamp: string
};
/**FrameData represents an array of line data */
type FrameData = LineData[];

export default class Video extends BaseComponent {
    //#region Private Elements
    /**Represents HTMLCanvasElement to draw frame data on*/
    private _canvasVideo: HTMLCanvasElement;
    private _canvasFront: HTMLCanvasElement;
    private _canvasSide: HTMLCanvasElement;
    /**Represents context to render on the canvas element (paintbrush)*/
    private _contextVideo: CanvasRenderingContext2D;
    private _contextFront: CanvasRenderingContext2D;
    private _contextSide: CanvasRenderingContext2D;
    /**Represents the embedded video playback */
    private _video: HTMLVideoElement;
    private _videoLayout: HTMLElement;
    /**Represents the streaming source of video playback */
    private _source: HTMLSourceElement;
    /**Represents the container for the video controls */
    private _controlBar: HTMLElement;
    /**Represents the play and pause button for video*/
    private _actionButton: HTMLElement;
    /**Represents the control for seeking in video playback*/
    private _slider: HTMLElement;
    /**Represents the visual bar for controls of seeking in video playback*/
    private _bar: HTMLElement;
    private _thumb: HTMLElement;
    /**Represents the source of content as string path*/
    private _timeStamp: HTMLElement;
    //#endregion

    //#region Private Fields
    private _currentIndex: number;
    private _src: string;
    private _frameDataList: FrameData[];
    private _totalNumber: number;
    private _timeCurrent: string;
    private _timeCheck: string;
    private _secondsCurrent: number;
    private _timeDuration: string;
    private _secondsDuration: number;
    private _stopFrame: boolean;
    //#endregion

    /** Constructor intializes and defines the Video component as an HTMLElement tag named UEye-Video as well as the context needed for drawing skeletal data 
    * @param parent - Represents properties of the current element as an HTMLElement.
    * * @returns Returns a Video  type with the referenced 2d context.   
    * */
    constructor(parent: HTMLElement) {
        super(parent, "UEye-Video");
        //#region Creating Elements

        this._videoLayout = Core.create("div", this.element, "Video-Layout");
        this._canvasVideo = Core.create("canvas", this._videoLayout, "Canvas-Video") as HTMLCanvasElement;
        this._canvasFront = Core.create("canvas", this._videoLayout, "Canvas-Front") as HTMLCanvasElement;
        this._canvasSide = Core.create("canvas", this._videoLayout, "Canvas-Side") as HTMLCanvasElement;

        this._video = Core.create("video", this._videoLayout, "Video") as HTMLVideoElement;
        this._video.crossOrigin = "Anonymous";
        this._video.muted = true;

        this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
        this._source.type = "video/mp4";

        this._controlBar = Core.create("div", this.element, "Control-Bar");

        this._actionButton = Core.create("div", this._controlBar, "Action-Button fa fa-play");
        this._actionButton.onclick = this._onActionHandel.bind(this);

        this._timeStamp = Core.create("div", this._controlBar, "Time-Stamp");

        this._slider = Core.create("div", this._controlBar, "Slider");
        this._bar = Core.create("div", this._slider, "Bar");
        this._thumb = Core.create("div", this._bar, "Thumb");
        //#endregion

        this.videoSetup();

        this._slider.onclick = (e) => { this.seekTime(e) };

        this._video.addEventListener('play', () => {
            this.draw(this._canvasVideo.width, this._canvasVideo.height);
            // this.drawBodyDataOnly(this._canvasVideo.width, this._canvasVideo.height, this._currentIndex);
        }, false);

        this._video.addEventListener('timeupdate', () => { this.updateTime() }, false);

        this._video.addEventListener("loadedmetadata", () => {
            this._secondsDuration = 0;
        }, false);
        this._video.addEventListener("ended", () => {
            this.replacePlayIcon(false);

        })
        this.onShow.on(this._onShowHandler.bind(this));
    }

    /** Method for setting property _frameData
     * @param value Parameter represents array of skeletal data to be viewed on the video.
     * */
    public set frameData(value: FrameData[]) {
        if (this._frameDataList !== value) {
            this._frameDataList = value;
        }
    }
    /** Accessor to get height of _canvas property.
    * @returns Returns height of type number.
    * */
    public get height(): number {
        return this._canvasVideo.height;
    }
    /** Accessor to get width of _canvas property.
    * @returns Returns width of type number.
    * */
    public get width(): number {
        return this._canvasVideo.width;
    }

    private seekTime(e: MouseEvent) {
        var percent = (e.offsetX / this._slider.offsetWidth);
        var currentTime = percent * this._video.duration;

        this._bar.style.width = (percent * 100) + "%";

        this._video.currentTime = (this._video.duration * percent);

    }
    private updateTime() {
        var percent = (this._video.currentTime / this._video.duration);
        this._bar.style.width = (percent * 100) + "%";
        // = (this._slider.offsetWidth * percent) + "px";
        this._timeCurrent = moment.unix(this._video.currentTime).format("mm:ss");;
        // if(moment.isDate(this._timeDuration)==false){
        //     this._timeDuration=moment.unix(0.0).format("mm:ss");
        // }
        this._timeCheck = moment.unix(this._video.currentTime).format("00:mm:ss.SSSSSSS");
        this._timeDuration = moment.unix(this._video.duration).format("mm:ss");
        this._timeStamp.innerHTML = this._timeCurrent + "/" + this._timeDuration;

    }
    private videoSetup() {
        //this._timeStamp.innerHTML = "0:00/0:00";
        this._secondsDuration = 0;
        this._currentIndex = 0;
        this._video.autoplay = true;
        let c1 = this._canvasVideo.getContext('2d');
        let c2 = this._canvasFront.getContext('2d');
        let c3 = this._canvasSide.getContext('2d');
        if (c1 !== null && c2 !== null && c3 !== null) {
            this._contextVideo = c1;
            this._contextFront = c2;
            this._contextSide = c3;
        }
    }
    private drawBodyDataOnly(w: number, h: number, frameIndex: number) {
        // var frameIndex = Math.round((this._frameDataList.length - 1) * percent);
        this._contextFront.clearRect(0, 0, this._canvasFront.width, this._canvasFront.height);
        this._contextSide.clearRect(0, 0, this._canvasFront.width, this._canvasFront.height);
        var frameData1 = this._createFrame(this._frameDataList[frameIndex], w, h, "front");
        var frameData2 = this._createFrame(this._frameDataList[frameIndex], w, h, "side");
        var bit = createImageBitmap(frameData1);
        this._contextFront.putImageData(frameData1, 0, 0);
        this._contextSide.putImageData(frameData2, 0, 0);
        var percentTwo = ((frameIndex) / this._totalNumber);
        // this._minutesCurrent = Math.floor(this._video.currentTime / 60);
        // this._secondsCurrent = Math.floor(this._video.currentTime - this._minutesCurrent * 60);
        // this._timeStamp.innerHTML = this._minutesCurrent + ":" + this._secondsCurrent + "/" + this._timeDuration + ":" + this._secondsDuration;
        // this._bar.style.width = this._thumb.style.marginLeft = (this._slider.offsetWidth * percentTwo + "px");
        this._currentIndex = frameIndex + 1;
        if (this._stopFrame == false && this._currentIndex <= this._totalNumber) {
            setTimeout(this.drawBodyDataOnly.bind(this), 115, w, h, this._currentIndex);
        } else if (this._currentIndex > this._totalNumber) {
            this._stopFrame = true;
            this._currentIndex = 0;
            //this.replacePlayIcon(false);
        }

    }

    /** Method to draw joint and connective bone data on canvas alongside the embedded video
     * @param w Width parameter of the canvas
     * @param h Height parameter of the canvas
     * */
    private draw(w: number, h: number) {
        if (this._video.paused || this._video.ended) {
            return;
        }
        if (this._frameDataList != undefined) {
            this._totalNumber = this._frameDataList.length - 1;
            var x = moment(this._frameDataList[this._currentIndex][0].timeStamp, "hh:mm:ss:SSSSSSS");
            var y = moment(this._timeCheck, "hh:mm:ss:SSSSSSS");
            // if(moment(x).isSame(y))
            this.drawBodyDataOnly(w, h, this._currentIndex);
            var percent = (this._video.currentTime / this._video.duration);
            this._contextVideo.drawImage(this._video, 0, 0, w, h);
            this._bar.style.width = (percent * 100) + "%";
            // = (this._slider.offsetWidth * percent) + "px";
            setTimeout(this.draw.bind(this), 33, w, h);
        }

    }
    /** Accessor to get source path of video.
    * @returns Returns string path
    * */
    public get src(): string {
        return this._src;
    }

    public get canvasHeight(): number {
        return this._canvasFront.offsetHeight;
    }
    public get canvasWidth(): number {
        return this._canvasFront.offsetWidth;
    }
    /** Method for setting property _src. Previous/Current playback is paused before new playback can be loaded andstreamed.
     * @param value string representation of the source path
     * */
    public set src(value: string) {
        if (this._src !== value) {
            this._src = value;

            this._video.pause();

            this._video.removeChild(this._source);
            this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
            this._source.type = "video/mp4";

            this._source.src = this._src;
            this.replacePlayIcon(true);
            this._video.load();
            this._video.currentTime = 0;

            // this._video.play();

        }
        if (this._src !== undefined) {
            Core.addClass(this._source, "Visible");
        }
    }


    /** Method that toggles enable and disable state of a Video element.
     * @returns Nothing (return part of property definition).
     * */

    public onEnabledChange(): void {
        if (this.enabled) {
            Core.removeClass(this.element, "disabled");
        } else {
            Core.addClass(this.element, "disabled");
        }
    }

    /** Method creates seekable frame of skeletal data
     * @param w Parameter represents width of canvas.
     *  @param h Parameter represents height of canvas.
     *  @param frame Parameter represents particular instance of frame data being rendered.
     * @returns Image data that can played back.
     * */
    private _createFrame(frame: FrameData, w: number, h: number, type: string): ImageData {

        if (type == "front") {
            context = this._contextFront;
            context.lineWidth = 2;

            frame.forEach(line => {
                context.beginPath();
                context.moveTo(line.x1, line.y1);
                context.lineTo(line.x2, line.y2);
                context.strokeStyle = "purple";
                context.stroke();
                context.closePath();

            });

            frame.forEach(line => {
                context.beginPath();
                context.rect(line.x1 - 1, line.y1 - 1, 1, 1);
                context.strokeStyle = "blue";
                context.stroke();
                context.fillStyle = 'blue';
                context.fill();
                context.closePath();

                context.beginPath();
                context.rect(line.x2 - 1, line.y2 - 1, 1, 1);
                context.strokeStyle = "blue";
                context.stroke();
                context.fillStyle = 'blue';
                context.fill();
                context.closePath();
            });
            return context.getImageData(0, 0, w, h);
        } else {
            var context = this._contextSide;
            context.lineWidth = 2;
            // context.scale(0.1, 0.1);
            frame.forEach(line => {
                context.beginPath();
                context.moveTo(line.z1, line.y1);
                context.lineTo(line.z2, line.y2);
                context.strokeStyle = "purple";
                context.stroke();
                context.closePath();

            });

            frame.forEach(line => {

                context.beginPath();
                context.rect(line.z1 - 1, line.y1 - 1, 1, 1);
                context.strokeStyle = "blue";
                context.stroke();
                context.fillStyle = 'blue';
                context.fill();
                context.closePath();

                context.beginPath();
                context.rect(line.z2 - 1, line.y2 - 1, 1, 1);
                context.strokeStyle = "blue";
                context.stroke();
                context.fillStyle = 'blue';
                context.fill();
                context.closePath();
            });
            return context.getImageData(0, 0, w, h);
        }

    }
    /**Method handles toggling between pause and play actions */
    private _onActionHandel() {
        if (this._video.paused || this._video.ended) {
            // Pause put in play more.
            this._stopFrame = false;
            this._video.play();
            this.replacePlayIcon(true);
        } else {
            // Play put in pause more.
            this._stopFrame = true;
            this._video.pause();
            this._currentIndex - 1;
            this.replacePlayIcon(false)
        }
    }
    private replacePlayIcon(value: boolean) {
        if (value == true) {
            Core.replaceClass(this._actionButton, "fa-play", "fa-pause");
        }
        else if (value == false) {
            Core.replaceClass(this._actionButton, "fa-pause", "fa-play");
        }
    }
    private _onShowHandler(view: BaseView) {
        this._canvasVideo.height = this._canvasVideo.offsetHeight;
        this._canvasVideo.width = this._canvasVideo.offsetWidth;
        this._video.width = this._canvasVideo.width;
        this._video.height = this._canvasVideo.height
        //this._context.scale(2, 2);
    }
}

import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
/**Type Definition: for LineData to be drawn on canvas */
type LineData = {
    /**x-coordinate for point 1*/
      x1: number,
      /**y-coordinate for point 1*/
      y1: number, 
      /**x-coordinate for point 2*/
      x2: number, 
      /**y-coordinate for point 2*/
      y2: number };
/**FrameData represents an array of line data */
type FrameData = LineData[];

export default class Video extends BaseComponent {
    /**Represents HTMLCanvasElement to draw frame data on*/
    private _canvas: HTMLCanvasElement;
    /**Represents context to render on the canvas element (paintbrush)*/
    private _context: CanvasRenderingContext2D;
    /**Represents the embedded video playback */
    private _video: HTMLVideoElement;
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

    private _src: string;
    /**Represents array of framedata that makes one complete video */
    private _frameDataList: FrameData[];

    private _timeStamp: HTMLElement;
    private _minutesCurrent: number;
    private _secondsCurrent: number;
    private _minutesDuration: number;
    private _secondsDuration: number;


     /** Constructor intializes and defines the Video component as an HTMLElement tag named UEye-Video as well as the context needed for drawing skeletal data 
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Video  type with the referenced 2d context.   
     * */

    constructor(parent: HTMLElement) {
        super(parent, "UEye-Video");

        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
    

        this._video = Core.create("video", this.element, "Video") as HTMLVideoElement;
        this._video.width = this._canvas.width;
        this._video.crossOrigin = "Anonymous";
        this._video.muted=true;
        

        this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
        this._source.type = "video/mp4";

        this._controlBar = Core.create("div", this.element, "Control-Bar");

        this._actionButton = Core.create("div", this._controlBar, "Action-Button fa fa-play");
        this._actionButton.onclick = this._onActionHandel.bind(this);

        this._timeStamp=Core.create("div", this._controlBar, "Time-Stamp");

        this._slider = Core.create("div", this._controlBar, "Slider");
        this._bar = Core.create("div", this._slider, "Bar");
        this._thumb =Core.create("div",this._bar, "Thumb");

    
        this._timeStamp.innerHTML="0:00/0:00";
        this._minutesDuration =0;
        this._secondsDuration= 0;
        this._video.autoplay=false;

        this._slider.onclick = (e) => {
           
            this._bar.style.width=  this._thumb.style.marginLeft = e.offsetX + "px";
            var percent = (e.offsetX / this._slider.offsetWidth);
            var currentTime= percent*this._video.duration;
            this._minutesCurrent= Math.floor(currentTime/ 60);
            this._secondsCurrent= Math.floor(currentTime - this._minutesCurrent * 60);
            this._timeStamp.innerHTML=this._minutesCurrent+":"+this._secondsCurrent+"/"+this._minutesDuration+":"+this._secondsDuration;
             
                       // this._video.seekable.111

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
            this._context.scale(0.5, 0.5);
        };

        this._video.addEventListener('play', () => {
            this.draw(this._canvas.width, this._canvas.height);
        }, false);

        this._video.addEventListener('timeupdate', () => {
            var percent = (this._video.currentTime / this._video.duration);
            this._bar.style.width = this._thumb.style.marginLeft= (this._slider.offsetWidth * percent) + "px";
           
            this._minutesCurrent= Math.floor(this._video.currentTime/ 60);
            this._secondsCurrent= Math.floor(this._video.currentTime - this._minutesCurrent * 60);
            this._minutesDuration = Math.floor(this._video.duration / 60);
            this._secondsDuration= Math.floor(this._video.duration - this._minutesDuration * 60);
            if(isNaN(this._minutesDuration) || isNaN(this._secondsDuration )){
                this._minutesDuration=this._secondsDuration=0;
            }

            this._timeStamp.innerHTML=this._minutesCurrent+":"+this._secondsCurrent+"/"+this._minutesDuration+":"+this._secondsDuration;
           
             
        }, false);

        this._video.addEventListener("loadedmetadata", () => {
            this._minutesDuration=this._secondsDuration=0;
         }, false);
        this._video.addEventListener("ended", ()=>{
            Core.replaceClass(this._actionButton, "fa-pause", "fa-play");
        })
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
        return this._canvas.height;
    }
     /** Accessor to get width of _canvas property.
     * @returns Returns width of type number.
     * */
    public get width(): number {
        return this._canvas.width;
    }
    /** Method to draw joint and connective bone data on canvas alongside the embedded video
     * @param w Width parameter of the canvas
     * @param h Height parameter of the canvas
     * */
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
            var bit = createImageBitmap(frameData);
            this._context.putImageData(frameData, 0, 0);
          
        }
    

       this._minutesCurrent= Math.floor(this._video.currentTime/ 60);
       this._secondsCurrent= Math.floor(this._video.currentTime - this._minutesCurrent * 60);
        this._timeStamp.innerHTML=this._minutesCurrent+":"+this._secondsCurrent+"/"+this._minutesDuration+":"+this._secondsDuration;
        this._bar.style.width=this._thumb.style.marginLeft = (this._slider.offsetWidth * percent) + "px";
        
        setTimeout(this.draw.bind(this), 20, w, h);
       
    
    }
     /** Accessor to get source path of video.
     * @returns Returns string path
     * */
    public get src(): string {
        return this._src;
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
            Core.replaceClass(this._actionButton, "fa-pause", "fa-play");
            this._video.load();
            this._video.currentTime = 0;
            
            // this._video.play();
           
        }
        if (this._src !== undefined) {
            Core.addClass(this._source, "Visible");
        }
    }
    /** Method responsible for toggling on action of playing the video
     * @returns Nothing
     * */
    public play(): void {
        this._video.play();
    }

    // public get onChange(): OnChangeCallback {
    //     return this._onChangeCallback;
    // }
    // public set onChange(value: OnChangeCallback) {
    //     this._onChangeCallback = value;
    // }

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
    /**Method handles toggling between pause and play actions */
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

// import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
// import UEye from "UEye/UEye";
// import Core from "UEye/Elements/Core/Core";

// export default class Stream extends BaseComponent {

//     private _canvas: HTMLCanvasElement;
//     private _context: CanvasRenderingContext2D;
//     private _video: HTMLVideoElement;
//     private _source: HTMLSourceElement;
//     private _src: string;

//     public constructor(parent: HTMLElement) {
//         super(parent, "UEye-Stream");


//         this._video = Core.create("video", this.element, "Video") as HTMLVideoElement;
//         this._video.width = 200;
//         this._video.controls = true;
//         this._source = Core.create("source", this._video, "Source") as HTMLSourceElement;
//         this._source.type = "video/mp4";

//         this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
//         var c = this._canvas.getContext('2d');
//         if (c != null) {
//             this._context = c;
//         }

//         // this._canvas.onmouseover = (e) => {
//         //     console.log(e);

//         //     var rect = this._canvas.getBoundingClientRect();
//         //     var x = e.clientX - rect.left;
//         //     var y = e.clientY - rect.top;

//         //     this._context.beginPath();
//         //     this._context.arc(x, y, 4, 0, 2 * Math.PI);
//         //     this._context.strokeStyle = "yellow";
//         //     this._context.stroke();
//         //     this._context.fillStyle = 'yellow';
//         //     this._context.fill();
//         //     this._context.closePath();
//         // };

//         var cw = Math.floor(this._canvas.clientWidth / 100);
//         var ch = Math.floor(this._canvas.clientHeight / 100);
//         this._canvas.width = cw;
//         this._canvas.height = ch;

//         this._video.addEventListener('play', () => {
//             this.draw(cw, ch);
//         }, false);
//     }

//     private draw(w: number, h: number) {
//         if (this._video.paused || this._video.ended) {
//             this._video.load();
//         }
//         // this._context.drawImage(this._video, 0, 0, w, h);
//         setTimeout(this.draw, 20, w, h);

//         var x = 20;
//         var y = 20;

//         this._context.beginPath();
//         this._context.arc(x, y, 4, 0, 2 * Math.PI);
//         this._context.strokeStyle = "yellow";
//         this._context.stroke();
//         this._context.fillStyle = 'yellow';
//         this._context.fill();
//         this._context.closePath();


//         // return true;
//     }

//     public get src(): string {
//         return this._src;
//     }
//     public set src(value: string) {
//         if (this._src !== value) {
//             this._src = value;
//             this._source.src = this._src;
//         }
//     }

//     public play(): void {
//         this._video.play();
//     }

//     public onEnabledChange(): void {
//         throw new Error("Method not implemented.");
//     }
// }
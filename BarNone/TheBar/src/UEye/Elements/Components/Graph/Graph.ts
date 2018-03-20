// Stub a local require for the ts compiler.
declare function require(moduleNames: string): any;

import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
/**Type Definition: for LineData to be drawn on canvas */
export type LineData = { 
    /**x-coordinate on canvas*/
    x: number, 
      /**y-coordinate on canvas*/
    y: number };




const Chart = require("chartjs");
console.log(Chart);
/**Describes the properties for Chart and Graph related operations. Wrapper for API required from Chart.js*/
export default class Graph extends BaseComponent {
    /**Represents HTMLCanvasElement to draw graphs on*/
    private _canvas: HTMLCanvasElement;
     /**Represents context to render on the canvas element (paintbrush)*/
    private _context: CanvasRenderingContext2D;
     /**Represents type of graph being draw as a string value (eg. Bar for Bar graph and Line for Line graph)*/
    private _type: string;


    /**Represents array of linedata that plots the complete graph */
    private _data: LineData[];

    

     /**Represents the label along the X-Axis */
    private _xAxisLabel: string;
      /**Represents the label along the Y-Axis */
    private _yAxisLabel: string;

      /**Represents Title of Graph*/
    private _title: string;

      /** Constructor intializes and defines the Graph component as an HTMLElement tag named UEye-Graph as well as the context needed for plotting graphs. 
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Graph type with the referenced 2d context.   
     * 
     * */
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Graph");        

        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
        var c = this._canvas.getContext('2d');
        this._title= "Graph";
       
        
        if (c !== null) { this._context = c; }

        // Chart(this._context);


    }
    public get type(): string {
		return this._type;
    }
    
	public set type(value: string) {
		this._type = value;
		
    }
    public set xAxisLabel(value: string) {
		this._xAxisLabel = value;
    }
    public get xAxisLabel (): string {
		return this._xAxisLabel;
    }
    public set yAxisLabel(value: string) {
		this._yAxisLabel = value;
    }
    public get yAxisLabel (): string {
		return this._yAxisLabel;
    }
    public set title(value: string) {
		this._title = value;
    }
    public get title (): string {
		return this._title;
    }
    public set data(value: LineData[]) {
		this._data = value;
    }
    public get data (): LineData[] {
		return this._data;
    }
    public set draw (value:boolean) {
       
        if(value==true){
            var chart = new Chart(this._context, {
                type: 'line',
                data: {
                  //  labels: ["0", "15", "30", "45", "60", "75"],
                    datasets: [{
                        label: this._title,
                        fill: false,
                         data: this._data,
    
                    }]
                },
                options: {
                    scales: {
                        yAxes: [{
                            ticks: {
                                // beginAtZero: true
                            },
                          scaleLabel: {
                            display: true,
                            labelString: this._yAxisLabel
                           
                          }
                        }],
                        xAxes: [{
                            type: 'linear',
                            ticks: {
                                beginAtZero: true,
                            },
                            scaleLabel: {
                              display: true,
                              labelString: this._xAxisLabel
                            }
                          }],
                      }  
                }
            });
        }
        }
    


    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
}
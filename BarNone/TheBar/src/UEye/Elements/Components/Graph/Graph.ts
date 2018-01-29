// import { Chart } from "chart.js";
// var Chart = require("../../../../../../node_modules/chart.js/src/chart");
// import { Chart } from "../../../../../node_modules/chart.js/src/chart";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
/**Type Definition: for LineData to be drawn on canvas */
type LineData = { 
    /**x-coordinate on canvas*/
    x: number, 
      /**y-coordinate on canvas*/
    y: number };
/**GraphData represents an array of line data */
type GraphData = LineData[];

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
    /**Represents array of graphdata that plots the complete graph */
    private _data: GraphData[];
     /**Represents the label along the X-Axis */
    private _xAxisLabel: string;
      /**Represents the label along the Y-Axis */
    private _yAxisLabel: string;
      /**Represents the Range and ID along the X-Axis*/
    private _xRange: GraphData[];
      /** Constructor intializes and defines the Graph component as an HTMLElement tag named UEye-Graph as well as the context needed for plotting graphs. 
     * @param parent - Represents properties of the current element as an HTMLElement.
	 * * @returns Returns a Graph type with the referenced 2d context.   
     * 
     * */
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Graph");        

        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
        var c = this._canvas.getContext('2d');
        if (c !== null) { this._context = c; }

        // Chart(this._context);

        var chart = new Chart(this._context, {
            type: 'line',
            data: {
                labels: ["0", "15", "30", "45", "60", "75"],
                datasets: [{
                    label: '# of Votes',
                     data: [{
                        x: 15,
                        y: 20
                    }, {
                        x: 15,
                        y: 15
                    }, {
                        x: 60,
                        y: 15
                    }, 
                    {
                        x: 95,
                        y: 20
                    } 
                ],

                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        },
                      scaleLabel: {
                        display: true,
                        labelString: 'probability'
                       
                      }
                    }],
                    xAxes: [{
                        scaleLabel: {
                          display: true,
                          labelString: 'Name'
                        }
                      }],
                  }  
            }
        });
    }

    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
}
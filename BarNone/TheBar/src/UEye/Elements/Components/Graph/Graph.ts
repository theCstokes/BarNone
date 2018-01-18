// import { Chart } from "chart.js";
// var Chart = require("../../../../../../node_modules/chart.js/src/chart");
// import { Chart } from "../../../../../node_modules/chart.js/src/chart";
import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";

const Chart = require("chartjs");
console.log(Chart);

export default class Graph extends BaseComponent {
    private _canvas: HTMLCanvasElement;
    private _context: CanvasRenderingContext2D;

    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Graph");        

        this._canvas = Core.create("canvas", this.element, "Canvas") as HTMLCanvasElement;
        var c = this._canvas.getContext('2d');
        if (c !== null) { this._context = c; }

        // Chart(this._context);

        var chart = new Chart(this._context, {
            type: 'bar',
            data: {
                labels: ["Red", "Blue", "Yellow", "Green", "Purple", "Orange"],
                datasets: [{
                    label: '# of Votes',
                    data: [12, 19, 3, 5, 2, 3],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255,99,132,1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            // beginAtZero: true
                        }
                    }]
                }
            }
        });
    }

    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
}
import ControlTypes from "UEye/ControlTypes";
import List from "UEye/Elements/Components/List/List";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import Graph from "UEye/Elements/Components/Graph/Graph";
import { View } from "UEye/View/View";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

export interface IChartTabView{
	analysisTypeDropdown : DropDownInput;
	jointDropdown: DropDownInput;
	dimensionDropdown: DropDownInput;
	chart: Graph;
}

export class ChartTab{
	public static get content() : any{
		return {
			title: "Charts",
			content: [
				{
					id: "analysisTypeDropdown",
					instance: ControlTypes.DropDownInput,
					hint: "Analysis Type"
				},
				{
					id: "jointDropdown",
					instance: ControlTypes.DropDownInput,
					hint: "Joint"
				},
				{
					id: "dimensionDropdown",
					instance: ControlTypes.DropDownInput,
					hint: "Dimension"
				},
				{
					id: "chart",
					instance: ControlTypes.Graph,
					title: "This is a test title",
					xAxisLabel: "Time",
					yAxisLabel: "Whatever",
					data: [{x:1,y:1},{x:2,y:2},{x:3,y:3}],
					draw: true
				}
			]
		};
	}
}
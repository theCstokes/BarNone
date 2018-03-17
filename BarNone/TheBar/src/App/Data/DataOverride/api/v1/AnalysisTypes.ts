import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import { BaseDataOverride } from "UEye/Data/BaseDataOverride";

export default class Settings extends BaseDataOverride<AnalysisType> {
	public data: AnalysisType[] = [
		{
			id: 1,
			name: "Acceleration"
		},
		{
			id: 2,
			name: "Speed"
		},
		{
			id: 3,
			name: "Position"
		},
		{
			id: 4,
			name: "Angle"
		}
	];
}
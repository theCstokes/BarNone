import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import { BaseDataOverride } from "UEye/Data/BaseDataOverride";

export enum AnalysisTypeEnum {
	Acceleration = 1,
	Speed = 2,
	Position = 3,
	Angle = 4
}

export default class AnalysisTypes extends BaseDataOverride<AnalysisType> {
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
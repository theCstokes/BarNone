import { ELiftAnalysisType } from "App/Data/Models/Analysis/AnalysisRequest";

export default class AnalysisResult {
	public Results: ResultEntity[];
}

export class ResultEntity {
	[key: string]: any;

	public type: ELiftAnalysisType;

	public value: any;
}
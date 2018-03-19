export default class AnalysisRequest {
	public requests: RequestEntity[];
}

export enum ELiftAnalysisType {
	Position = 1,
	Velocity = 2,
	Acceleration = 3,
	Angle = 4
}

export class RequestEntity {
	[key: string]: any;

	public type: ELiftAnalysisType;
}
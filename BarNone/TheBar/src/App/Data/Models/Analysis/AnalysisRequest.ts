import { EJointType } from "App/Data/Models/Joint/EJointType";
import { EDimension } from "App/Data/Models/Analysis/EDimension";

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

	public Type: ELiftAnalysisType;
}

export class AnalysisRequestVelocity extends RequestEntity {
	public Joint : EJointType;
	public Dimension: EDimension;
}

export class AnalysisRequestPosition extends RequestEntity {
	public Joint : EJointType;
	public Dimension: EDimension;
}
import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";
import JointType from "App/Data/Models/Joint/JointType";

export default class SpeedAnalysisCriteria extends BaseDataModel {
	public id?: number|string;

	public userID?: number

	public jointTypeID?: number

	public details?: Partial<SpeedAnalysisCriteriaDetails>;
}

class SpeedAnalysisCriteriaDetails {
	public jointType: JointType;
}
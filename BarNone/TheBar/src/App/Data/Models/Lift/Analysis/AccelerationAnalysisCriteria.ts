import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";
import JointType from "App/Data/Models/Joint/JointType";

export default class AccelerationAnalysisCriteria extends BaseDataModel {
	public id?: number|string;

	public userID?: number

	public jointTypeID?: number

	public details?: Partial<AccelerationAnalysisCriteriaDetails>;
}

class AccelerationAnalysisCriteriaDetails {
	public jointType: JointType;
}
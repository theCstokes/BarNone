import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";
import JointType from "App/Data/Models/Joint/JointType";

export default class AngleAnalysisCriteria extends BaseDataModel {
	public id?: number|string;

	public userID?: number

	public jointTypeAID?: number

	public jointTypeBID?: number

	public jointTypeCID?: number

	public details?: Partial<AngleAnalysisCriteriaDetails>;
}

class AngleAnalysisCriteriaDetails {
	public jointTypeA: JointType;

	public jointTypeB: JointType;

	public jointTypeC: JointType;
}
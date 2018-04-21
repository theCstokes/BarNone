import LiftType from "App/Data/Models/Lift/LiftType";
import AccelerationAnalysisCriteria from "App/Data/Models/Lift/Analysis/AccelerationAnalysisCriteria";
import AngleAnalysisCriteria from "App/Data/Models/Lift/Analysis/AngleAnalysisCriteria";
import SpeedAnalysisCriteria from "App/Data/Models/Lift/Analysis/SpeedAnalysisCriteria";
import PositionAnalysisCriteria from "App/Data/Models/Lift/Analysis/PositionAnalysisCriteria";
import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";

export default class LiftAnalysisProfile extends BaseDataModel {
	public id: number | string;

	public userID?: number;

	public liftTypeID?: number;

	public details?: Partial<LiftAnalysisProfileDetails>;

}

class LiftAnalysisProfileDetails {
	public liftType: LiftType;
	
	public accelerationAnalysisCriteria: AccelerationAnalysisCriteria[];

	public positionAnalysisCriteria: PositionAnalysisCriteria[];

	public speedAnalysisCriteria: SpeedAnalysisCriteria[];

	public angleAnalysisCriteria: AngleAnalysisCriteria[];
}
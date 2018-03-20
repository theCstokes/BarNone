import { BaseDataManager } from "UEye/Data/BaseDataManager";
import User from "App/Data/Models/User/User";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import Lift from "App/Data/Models/Lift/Lift";
import { Resource, DetailResource } from "UEye/Data/Resource";
import Joint from "App/Data/Models/Joint/Joint";
import Comment from "App/Data/Models/Comment/Comment";

import LiftAnalysisProfile from "App/Data/Models/LiftAnalysisProfile/LiftAnalysisProfile"

import LiftType from "App/Data/Models/Lift/LiftType";
import SettingsElement from "App/Data/Models/Settings/SettingsElement";
import JointType from "App/Data/Models/Joint/JointType";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import { EndPoint } from "UEye/Data/EndPoint";
import AnalysisRequest from "App/Data/Models/Analysis/AnalysisRequest";
import AnalysisResult from "App/Data/Models/Analysis/AnalysisResult";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: DetailResource<LiftFolder>
	= new DetailResource<LiftFolder>("LiftFolder");

	public static readonly Lifts: DetailResource<Lift> = new DetailResource<Lift>("Lift");

	public static readonly LiftAnalysisProfile: DetailResource<LiftAnalysisProfile>
	= new DetailResource("LiftAnalysisProfile");

	public static readonly LiftTypes: Resource<LiftType> = new Resource<LiftType>("LiftType");

	public static readonly SharedLifts: DetailResource<Lift> = new DetailResource<Lift>("SharedLift");

	public static readonly Comments: DetailResource<Comment> = new DetailResource<Comment>("Comment");

	public static readonly LiftComments: DetailResource<Comment>
	= new DetailResource<Comment>("Comment/Lift/{liftID}");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");

	public static readonly JointTypes: Resource<JointType> = new Resource<JointType>("JointType");

	public static readonly Settings: Resource<SettingsElement>
	= new Resource<SettingsElement>("Settings", true);

	public static readonly AnalysisTypes: Resource<AnalysisType>
	= new Resource<AnalysisType>("AnalysisTypes", true);

	public static readonly AnalysisPipe
	= new EndPoint<AnalysisRequest, AnalysisResult>("Analysis/Lift/{ID}");

	// DEMO
	// DataManager.AnalysisPipe.resource
	// 	.param("ID", lift.ID)
	// 	.create()
}
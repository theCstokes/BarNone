import { BaseDataManager } from "UEye/Data/BaseDataManager";
import User from "App/Data/Models/User/User";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import Lift from "App/Data/Models/Lift/Lift";
import { Resource, DetailResource } from "UEye/Data/Resource";
import Joint from "App/Data/Models/Joint/Joint";
import Comment from "App/Data/Models/Comment/Comment";

import LiftAnalysisProfile from "App/Data/Models/LiftAnalysisProfile/LiftAnalysisProfile"

import LiftType from "App/Data/Models/Lift/LiftType";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: DetailResource<LiftFolder> = new DetailResource<LiftFolder>("LiftFolder");

	public static readonly Lifts: DetailResource<Lift> = new DetailResource<Lift>("Lift");

	public static readonly LiftAnalysisProfile:DetailResource<LiftAnalysisProfile> = new DetailResource("LiftAnalysisProfile");

	public static readonly LiftTypes: Resource<LiftType> = new Resource<LiftType>("LiftType");


	public static readonly SharedLifts: DetailResource<Lift> = new DetailResource<Lift>("SharedLift");

	public static readonly Comments: DetailResource<Comment> = new DetailResource<Comment>("Comment");

	public static readonly LiftComments: DetailResource<Comment>
		= new DetailResource<Comment>("Comment/Lift/{liftID}");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");
}
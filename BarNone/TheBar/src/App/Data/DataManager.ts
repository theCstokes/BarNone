import { BaseDataManager } from "UEye/Data/BaseDataManager";
import User from "App/Data/Models/User/User";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import Lift from "App/Data/Models/Lift/Lift";
import { Resource, DetailResource } from "UEye/Data/Resource";
import Joint from "App/Data/Models/Joint/Joint";
import Comment from "App/Data/Models/Comment/Comment";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: DetailResource<LiftFolder> = new DetailResource<LiftFolder>("LiftFolder");

	public static readonly Lifts: DetailResource<Lift> = new DetailResource<Lift>("Lift");
	
	public static readonly SharedLifts: DetailResource<Lift> = new DetailResource<Lift>("SharedLift");

	public static readonly Comments: DetailResource<Comment> = new DetailResource<Comment>("Comment");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");
}
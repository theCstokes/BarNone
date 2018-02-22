import { BaseDataManager } from "UEye/Data/BaseDataManager";
import User from "App/Data/Models/User/User";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import Lift from "App/Data/Models/Lift/Lift";
import { Resource, DetailResource } from "UEye/Data/Resource";
import Joint from "App/Data/Models/Joint/Joint";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: DetailResource<LiftFolder> = new DetailResource<LiftFolder>("LiftFolder");

	public static readonly Lifts: DetailResource<Lift> = new DetailResource<Lift>("Lift");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");
}
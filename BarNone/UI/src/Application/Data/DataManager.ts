import { BaseDataManager } from "UEye/Data/BaseDataManager";
import User from "Application/Data/Models/User/User";
import Resource from "UEye/Data/Resource";
import LiftFolder from "Application/Data/Models/LiftFolder/LiftFolder";
import Joint from "Application/Data/Models/Body/Joint";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: Resource<LiftFolder> = new Resource<LiftFolder>("LiftFolder");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");
}
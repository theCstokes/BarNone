import { BaseDataManager } from "UEye/Data/BaseDataManager";
import Resource from "UEye/Data/Resource";
import User from "App/Data/Models/User/User";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import Joint from "App/Data/Models/Body/Joint";

export default class DataManager extends BaseDataManager {

	public static readonly Users: Resource<User> = new Resource<User>("User");

	public static readonly LiftFolders: Resource<LiftFolder> = new Resource<LiftFolder>("LiftFolder");

	public static readonly Joints: Resource<Joint> = new Resource<Joint>("Joints");
}
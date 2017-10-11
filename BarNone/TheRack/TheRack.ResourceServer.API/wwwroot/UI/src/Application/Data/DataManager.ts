import { BaseDataManager } from "Vee/Data/BaseDataManager";
import User from "Application/Data/Models/User/User";
import Resource from "Vee/Data/Resource";

export default class DataManager extends BaseDataManager {
	public static readonly Users: Resource<User> = new Resource<User>("User");
}
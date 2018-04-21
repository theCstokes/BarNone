import { BaseDataModel } from "App/Data/Models/Core/BaseDataModel";

export default class Permission extends BaseDataModel {
	public id: number | string;

	public liftID: number;

	public userID: number;
}
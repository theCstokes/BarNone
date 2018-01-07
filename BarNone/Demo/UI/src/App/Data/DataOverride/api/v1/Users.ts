import { BaseDataOverride } from "UEye/Data/BaseDataOverride";
import User from "App/Data/Models/User/User";

export default class Users extends BaseDataOverride<User> {
	public data: User[] = [
		{
			id: 1,
			name: "test",
			userName: "123",
			password: "321"
		}
	];
}
import { BaseDataOverride } from "Vee/Data/BaseDataOverride";
import User from "Application/Data/Models/User/User";

export class Users extends BaseDataOverride<User> {
	public data: User[] = [
		{
			id: 1,
			name: "test",
			userName: "123",
			password: "321"
		}
	];
}
import { BaseDataOverride } from "UEye/Data/BaseDataOverride";
import EditProfile from "App/Screens/Settings/EditProfile/EditProfileScreen";
import ChangePassword from "App/Screens/Settings/ChangePassword/ChangePasswordScreen";
import SettingsElement from "App/Data/Models/Settings/SettingsElement";

export default class Settings extends BaseDataOverride<SettingsElement> {
	public data: SettingsElement[] = [
		// {
		// 	id: 1,
		// 	name: "Edit Profile",
		// 	icon: "fa-edit",
		// 	screen: EditProfile	
		// },
		{
			id: 2,
			name: "Change Password",
			icon: "fa-key",
			screen: ChangePassword
		}
	];
}
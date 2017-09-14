import AppScreen from "Vee/Screen/AppScreen";
import UserEditView from "Application/User/Edit/UserEditView";

export default class UserEditScreen extends AppScreen {
	public constructor() {
		super(UserEditView);
	}
}
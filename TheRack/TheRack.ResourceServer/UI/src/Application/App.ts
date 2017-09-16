import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
import NavScreen from "Application/Nav/NavScreen";
import UserScreen from "Application/User/UserScreen";
import LoginScreen from "Application/Login/LoginScreen";

export default class App {
	public static async start(): Promise<void> {
		await Vee.push(LoginScreen);
	}
}
import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
import LoginScreen from "Application/Screens/Login/LoginScreen";

export default class App {
	public static async start(): Promise<void> {
		await Vee.push(LoginScreen);
	}
}
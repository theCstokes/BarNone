import UEye from "UEye/UEye";
import Core from "UEye/Elements/Core/Core";
import ControlTypes from "UEye/ControlTypes";
import LoginScreen from "Application/Screens/Login/LoginScreen";

export default class App {
	public static async start(): Promise<void> {
		await UEye.push(LoginScreen);
    }
}
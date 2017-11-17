import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
// import VideoScreen from "PlayGround/Screens/Video/VideoScreen";
import NavScreen from "PlayGround/Screens/Nav/NavScreen";

export default class App {
	public static async start(): Promise<void> {
		await Vee.push(NavScreen);
		// await Vee.push(VideoScreen);
	}
}
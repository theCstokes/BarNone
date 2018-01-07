import Vee from "Vee/Vee";
import Core from "Vee/Elements/Core/Core";
import ControlTypes from "Vee/ControlTypes";
// import VideoScreen from "PlayGround/Screens/Video/VideoScreen";
import NavScreen from "PlayGround/Screens/Nav/NavScreen";
import VideoScreen from "PlayGround/Screens/Video/VideoScreen";

export default class App {
	public static start(): void {
		Vee.push(NavScreen);
		Vee.push(VideoScreen);
	}
}
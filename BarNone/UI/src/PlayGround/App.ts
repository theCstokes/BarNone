import UEye from "UEye/UEye";
import Core from "UEye/Elements/Core/Core";
import ControlTypes from "UEye/ControlTypes";
// import VideoScreen from "PlayGround/Screens/Video/VideoScreen";
import NavScreen from "PlayGround/Screens/Nav/NavScreen";
import VideoScreen from "PlayGround/Screens/Video/VideoScreen";

export default class App {
	public static start(): void {
		UEye.push(NavScreen);
		// UEye.push(VideoScreen);
	}
}
import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import Vee from "Vee/Vee";
import Loader from "Vee/Elements/Core/Loader";
import VideoView from "PlayGround/Screens/Video/VideoView";
import { StateManager } from "PlayGround/Screens/Video/StateManager";

export default class VideoScreen extends BasicScreen<StateManager> {

	public constructor() {
		super(VideoView, StateManager, true);
		// Vee.onBack.on(() => this._backAction());
	}

	public onShow() {
		this.stateManager.init();
	}
}
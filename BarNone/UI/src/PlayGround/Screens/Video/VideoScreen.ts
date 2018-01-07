import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "UEye/Screen/ScreenBind";
import UEye from "UEye/UEye";
import Loader from "UEye/Elements/Core/Loader";
import VideoView from "PlayGround/Screens/Video/VideoView";
import { StateManager, State } from "PlayGround/Screens/Video/StateManager";

export default class VideoScreen extends BasicScreen<StateManager> {

	public constructor() {
		super(VideoView, StateManager, true);
		// UEye.onBack.on(() => this._backAction());
	}

	public videoPlayerBind = ScreenBind
		.create<State>(this, "videoPlayer")
		.onRender(async (original, current) => {
			// this.view.videoPlayer.frameData = current.lineData;
	});

	public onShow() {
		this.stateManager.init();
	}
}
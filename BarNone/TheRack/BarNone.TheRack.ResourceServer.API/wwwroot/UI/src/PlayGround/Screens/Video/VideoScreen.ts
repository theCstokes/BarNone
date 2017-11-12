import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import Vee from "Vee/Vee";
import Loader from "Vee/Elements/Core/Loader";
import VideoView from "PlayGround/Screens/Video/VideoView";
import { StateManager, State } from "PlayGround/Screens/Video/StateManager";

export default class VideoScreen extends BasicScreen<StateManager> {

	public constructor() {
		super(VideoView, StateManager, true);
		// Vee.onBack.on(() => this._backAction());
	}

	public videoPlayerBind = ScreenBind
		.create<State>(this, "videoPlayer")
		// .onSelect(async data => {
		// 	// console.log(data);
		// 	// Vee.popTo(this);
		// 	this.stateManager.selectionChange.trigger({
		// 		id: data.id
		// 	});
		// })
		.onRender(async (original, current) => {
			this.view.videoPlayer.frameData = current.lineData;
			// .map(d => {
			// 	return { x: d.x1, y: d.y1 };
			// });
	});

	public onShow() {
		this.stateManager.init();
	}
}
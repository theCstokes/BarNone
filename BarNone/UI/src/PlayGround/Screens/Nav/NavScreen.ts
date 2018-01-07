import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "UEye/Screen/ScreenBind";
import UEye from "UEye/UEye";
import Loader from "UEye/Elements/Core/Loader";
import { StateManager, State } from "PlayGround/Screens/Nav/StateManager";
import NavView from "PlayGround/Screens/Nav/NavView";

export default class NavScreen extends BasicScreen<StateManager> {

	public constructor() {
		super(NavView, StateManager, true);
		UEye.onBack.on(() => this._backAction());
	}

	public userListBind = ScreenBind
		.create<State>(this, "navList")
		.onSelect(async data => {
			// console.log(data);
			UEye.popTo(this);
			this.stateManager.selectionChange.trigger({
				id: data.id
			});
		})
		.onRender(async (original, current) => {
			this.view.navList.items = current.navElementList.map(item => {
				return {
					selected: (item.id === current.currentScreenId),
					id: item.id,
					name: item.name
				}
			});

			var navElement = current.navElementList.find(item => {
				return (item.id === current.currentScreenId);
			});
			if (navElement !== undefined) {
				var NextScreen = await Loader.sync(navElement.screenPath);
				await UEye.push(NextScreen.default);
			}
		});

	private _backAction() {
		UEye.popTo(this);
		this.stateManager.navigateBack.trigger();
	}

	public onShow() {
		this.stateManager.init();
	}
}
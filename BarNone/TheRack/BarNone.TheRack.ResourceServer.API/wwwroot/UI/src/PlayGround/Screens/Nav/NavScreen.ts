import BasicScreen from "Application/Core/BasicScreen";
import ScreenBind from "Vee/Screen/ScreenBind";
import Vee from "Vee/Vee";
import Loader from "Vee/Elements/Core/Loader";
import { StateManager, State } from "PlayGround/Screens/Nav/StateManager";
import NavView from "PlayGround/Screens/Nav/NavView";

export default class NavScreen extends BasicScreen<StateManager> {

	public constructor() {
		super(NavView, StateManager, true);
		Vee.onBack.on(() => this._backAction());
	}

	public userListBind = ScreenBind
		.create<State>(this, "navList")
		.onSelect(async data => {
			// console.log(data);
			Vee.popTo(this);
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
				await Vee.push(NextScreen.default);
			}
		});

	private _backAction() {
		Vee.popTo(this);
		this.stateManager.navigateBack.trigger();
	}

	public onShow() {
		this.stateManager.init();
	}
}
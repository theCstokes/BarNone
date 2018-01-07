import BasicScreen from "Application/Core/BasicScreen";
import NavView from "Application/Screens/Nav/NavView";
import ScreenBind from "UEye/Screen/ScreenBind";
import { State, StateManager } from "Application/Screens/Nav/StateManager";
import UEye from "UEye/UEye";
import Loader from "UEye/Elements/Core/Loader";

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
            //console.warn(data.name);
            
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
                //this.view.navBreadcrumbs.push({ value: navElement.name });
                this.view.navBreadcrumbs.items = [{ value: navElement.name }];
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
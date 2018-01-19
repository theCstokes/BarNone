import NavView from "App/Screens/Nav/NavView";
import Screen from "UEye/Screen/Screen";
import { StateManager, State } from "App/Screens/Nav/StateManager";
import UEye from "UEye/UEye";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";

export default class NavScreen extends Screen<NavView> {
	private _stateManager: StateManager;

	public constructor() {
		super(NavView);
		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));
		// super(NavView, StateManager, true);
		UEye.onBack.register(() => this._backAction());

		
	}

	private _onRender(current: State, original: State): void {
		this.view.navList.items = current.navElementList.map(item => {
			return {
				selected: (item.id === current.currentScreenId),
				id: item.id,
				name: item.name,
				icon: item.icon
			}
		});

		var navElement = current.navElementList.find(item => {
			return (item.id === current.currentScreenId);
		});
		if (navElement !== undefined) {
			//this.view.navBreadcrumbs.push({ value: navElement.name });
			this.view.navBreadcrumbs.items = [{ value: navElement.name }];
			// var NextScreen = Loader.sync(navElement.screenPath);
			UEye.push(navElement.screen);
		}
	}

	// public userListBind = ScreenBind
	// 	.create<State>(this, "navList")
	// 	.onSelect(async data => {
	// 		// console.log(data);
	// 		UEye.popTo(this);
	// 		//console.warn(data.name);

	// 		this._stateManager.SelectionChange.trigger({
	// 			id: data.id
	// 		});
	// 	})
	// 	.onRender(async (original, current) => {
	// 		this.view.navList.items = current.navElementList.map(item => {
	// 			return {
	// 				selected: (item.id === current.currentScreenId),
	// 				id: item.id,
	// 				name: item.name
	// 			}
	// 		});

	// 		var navElement = current.navElementList.find(item => {
	// 			return (item.id === current.currentScreenId);
	// 		});
	// 		if (navElement !== undefined) {
	// 			//this.view.navBreadcrumbs.push({ value: navElement.name });
	// 			this.view.navBreadcrumbs.items = [{ value: navElement.name }];
	// 			var NextScreen = await Loader.sync(navElement.screenPath);
	// 			await UEye.push(NextScreen.default);
	// 		}
	// 	});

	private _backAction() {
		UEye.popTo(this);
		// this.stateManager.navigateBack.trigger();
	}

	public onShow() {
		this.view.navList.onSelect = (data: IListItem) => {
			UEye.popTo(this);

			this._stateManager.SelectionChange.trigger({
				id: data.id
			});
		};
		this._stateManager.ResetState.trigger();
	}
}
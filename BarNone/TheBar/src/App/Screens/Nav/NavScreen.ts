import NavView from "App/Screens/Nav/NavView";
import Screen from "UEye/Screen/Screen";
import { StateManager, State } from "App/Screens/Nav/StateManager";
import UEye from "UEye/UEye";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import App from "App/App";
import { ContextStateManager, ContextState } from "App/Screens/Nav/ContextStateManager";
import LoginScreen from "App/Screens/Login/LoginScreen";
import DataManager from "App/Data/DataManager";

/**
 *  Represents NavScreen class .
 */
export default class NavScreen extends Screen<NavView> {
	private _stateManager: StateManager;
	private _contextStateManager: ContextStateManager;
	private _subScreen: Screen<any>
	private _subScreenID: number;

 /** Constructor intialized Screen Component and binds corresponding View and StateManager 
     * */
	public constructor() {
		
		super(NavView);
		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));

		this._contextStateManager = new ContextStateManager(this._stateManager);
		this._contextStateManager.bind(this._onContextRender.bind(this));
		// super(NavView, StateManager, true);
		UEye.onBack.register(() => this._backAction());		
	}

	private _onContextRender(current: ContextState, original: ContextState): void {
		this.view.navBreadcrumbs.items = current.crumbList.map(crumb => {
			return {
				id: crumb.id,
				value: crumb.value,
				onClick: crumb.onClick
			}
		});
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
		if (navElement !== undefined /*&& navElement.id !== this._subScreenID*/) {
			//this.view.navBreadcrumbs.push({ value: navElement.name });

			// [{
			// 	value: navElement.name,
			// 	onClick: () => {
			// 		if (navElement !== undefined) {
			// 			this.view.navBreadcrumbs.pop();
			// 			UEye.popTo(this);
			// 			UEye.push(navElement.screen);
			// 		}
			// 	}
			// }];
			// var NextScreen = Loader.sync(navElement.screenPath);
			this._subScreenID = navElement.id;
			UEye.popTo(this);
			this._subScreen = UEye.push(navElement.screen, navElement.initData);
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
	/** Method pops navigation on selecting back
     * */
	private _backAction() {
		UEye.popTo(this);
		// this.stateManager.navigateBack.trigger();
	}
		/** Method defines UI properties when shown
     * */
	public onShow() {
		App.breadcrumbs = this.view.navBreadcrumbs;
		App.Navigation = this._contextStateManager;
		// App.Toast = this.view.toast;

		this.view.logoutButton.onClick = () => {
			DataManager.logout();
			UEye.popAll();
			UEye.push(LoginScreen);
		}
		this.view.helpButton.onClick=() =>{
			 this.view.pageFrame.toggleHelpBar(true);
		}
		this.view.exitHelpButton.onClick=() =>{
			this.view.pageFrame.toggleHelpBar(false);
	   }
		this.view.navList.onSelect = (data: IListItem) => {
			UEye.popTo(this);

			this._stateManager.SelectionChange.trigger({
				id: data.id
			});
		};
		this._stateManager.ResetState.trigger();
	}
}
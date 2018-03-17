import UserView from "App/Screens/User/UserView";
import Screen from "UEye/Screen/Screen";
import UserEditScreen from "App/Screens/User/Edit/UserEditScreen";
import { StateManager, State } from "App/Screens/User/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";

/**
 * User Screen.
 */
export default class UserScreen extends Screen<UserView> {
	private subScreen: UserEditScreen;
	private _stateManager: StateManager;
	
	/**
	 * Initialize user screen.
	 */
	public constructor() {
		super(UserView);
		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));
	}

	/**
	 * Render state.
	 * @param current - current state for render
	 * @param original - original state for render
	 */
	private _onRender(current: State, original: State): void {
		this.view.userList.items = current.selectionList.map(item => {
			return {
				selected: (item.id === current.selectionId),
				id: item.id,
				name: item.name
			}
		});

		var userData = current.selectionList.find(item => {
			return (item.id === current.selectionId);
		});

		if (userData !== undefined) {
			this.subScreen.stateManager.ResetState.trigger({
				id: userData.id,
				name: userData.name,
				age: 0
			});
		}
	}

	/**
	 * onShow for user screen.
	 */
	public onShow() {
		this.view.userList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id })
		};

		this.subScreen = UEye.push(UserEditScreen) as UserEditScreen;
		this._stateManager.ResetState.trigger();

		// this.subScreen.stateManager.saveEvent.on(() => {
		// 	this.stateManager.init();
		// });

		// this.subScreen.cancelEvent.on(() => {
		// 	var current = this.stateManager.getCurrentState();
		// 	var userData = current.selectionList.find(item => {
		// 		return (item.id === current.selectionId);
		// 	});
		// 	this.subScreen.stateManager.resetState.trigger(userData);
		// });
		
		// this._stateManager.ResetState.trigger();

		// UEye.push(UserEditScreen);
	}
}
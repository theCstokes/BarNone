import BasicScreen from "Application/Core/BasicScreen";
import Vee from "Vee/Vee";
import LiftView from "Application/Screens/Lifts/LiftView";
import LiftEditScreen from "Application/Screens/Lifts/Edit/LiftEditScreen";
import { StateManager, State } from "Application/Screens/Lifts/StateManager";
import ScreenBind from "Vee/Screen/ScreenBind";

export default class LiftScreen extends BasicScreen<StateManager> {
	private subScreen: LiftEditScreen;
	
	public constructor() {
		super(LiftView, StateManager);
	}

	public userListBind = ScreenBind
		.create<State>(this, "userList")
		.onSelect(async data => {
			// console.log(data);
			// Vee.popTo(this);
			this.stateManager.selectionChange.trigger({
				id: data.id
			});
		})
		.onRender(async(original, current) => {
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
			this.subScreen.stateManager.resetState.trigger(userData);
			// await Vee.push(UserEditScreen, userData);
		});

	public async onShow() {
		this.subScreen = await Vee.push(LiftEditScreen) as LiftEditScreen;
		this.stateManager.init();

		this.subScreen.stateManager.saveEvent.on(() => {
			this.stateManager.init();
		});

		this.subScreen.cancelEvent.on(() => {
			var current = this.stateManager.getCurrentState();
			var userData = current.selectionList.find(item => {
				return (item.id === current.selectionId);
			});
			this.subScreen.stateManager.resetState.trigger(userData);
		});
		
		// Vee.push(UserEditScreen);
	}
}
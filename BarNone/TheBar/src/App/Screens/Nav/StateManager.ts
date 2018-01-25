import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import Screen from "UEye/Screen/Screen";
import UserScreen from "App/Screens/User/UserScreen";
import LiftScreen from "App/Screens/Lifts/LiftScreen";
import LiftScreen2 from "App/Screens/Lift2/LiftScreen";
import SettingsScreen from "App/Screens/Settings/SettingsScreen"
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { ContextState } from "App/Screens/Nav/ContextStateManager";
import ParentStateManager from "UEye/StateManager/ParentStateManager";

class NavElement {
	public id: number;
	public name: string;
	public icon: string;
	public screen: { new(): Screen<any> };
}


export class State {
	public currentScreenId: number;
	public navHistory: number[] = [];
	public context: ContextState = new ContextState();
	public navElementList: NavElement[] = [
		{
			id: 1,
			name: "Users",
			icon: "fa-user",
			screen: UserScreen
		},
		{
			id: 2,
			name: "Settings",
			icon: "fa-cog",
			screen: SettingsScreen
		},
		// {
		// 	id: 3,
		// 	name: "Videos",
		// 	icon: "fa-video-camera",
		// 	screen: UserScreen
		// },
		// {
		// 	id: 4,
		// 	name: "Lifts",
		// 	icon: "fa-universal-access",
		// 	screen: LiftScreen
		// },
		{
			id: 5,
			name: "Lifts2",
			icon: "fa-universal-access",
			screen: LiftScreen2
		}
	]
}

export class StateManager extends ParentStateManager<State> {
	public constructor() {
		super(State);
	}

	// public constructor() {
	// 	super();
	// }

	// public readonly _resetState = StateBind
	// 	.onCallable<State>(this, (state) => {
	// 		var nextState = Utils.clone(state);

	// 		nextState.current.currentScreenId = nextState.current.navElementList[0].id;
	// 		nextState.current.navHistory.push(nextState.current.currentScreenId);

	// 		return nextState;
	// 	}, { resetState: true });

	public readonly ResetState = StateBind
		.onCallable<State>(this, (state) => {
			var nextState = state.empty();

			nextState.current.currentScreenId = nextState.current.navElementList[0].id;
			nextState.current.navHistory.push(nextState.current.currentScreenId);

			return nextState.initialize();
		});

	public readonly SelectionChange = StateBind
		.onAction<State, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.currentScreenId = data.id;

			var selection = nextState.current.navElementList
				.find(e => e.id === nextState.current.currentScreenId);

			if (selection !== undefined) {
				nextState.current.context.crumbList = [{
					id: Utils.guid(),
					value: selection.name,
					onClick: this._onBaseCrumbElementClickHandler.bind(this)
				}];

				nextState.current.navHistory.push(nextState.current.currentScreenId);
			}

			return nextState;
		});

	public readonly NavigateBack = StateBind
		.onAction<State, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.navHistory.pop();

			if (nextState.current.navHistory.length > 0) {
				var lastIndex = (nextState.current.navHistory.length - 1);
				nextState.current.currentScreenId = nextState.current.navHistory[lastIndex];
			}

			return nextState;
		});

	private _onBaseCrumbElementClickHandler() {
		var current = this.getCurrentState();
		this.SelectionChange.trigger({ id: current.currentScreenId });
	}

	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	// public get resetState(): IDataBind {
	// 	return this._resetState.expose();
	// }

	// public get selectionChange(): IDataBind {
	// 	return this._selectionChange.expose();
	// }

	// public get navigateBack(): IDataBind {
	// 	return this._navigateBack.expose();
	// }

	// public init(): void {
	// 	this.resetState.trigger();
	// }

	// public onSave(): void {
	// 	throw new Error("Method not implemented.");
	// }
}
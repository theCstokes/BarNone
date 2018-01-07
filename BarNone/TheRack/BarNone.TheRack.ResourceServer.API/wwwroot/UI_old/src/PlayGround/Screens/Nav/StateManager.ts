import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";
import { AppScreen } from "Vee/Screen/AppScreen";

class NavElement {
	public id: number;
	public name: string;
	public screenPath: string;
}

export class State {
	public currentScreenId: number;
	public navHistory: number[] = [];
	public navElementList: NavElement[] = [
		{
			id: 1,
			name: "Videos",
			screenPath: "PlayGround/Screens/Video/VideoScreen"
		}
	]
}

export class StateManager extends BaseStateManager<State> {
	public readonly _resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.currentScreenId = nextState.navElementList[0].id;
			nextState.navHistory.push(nextState.currentScreenId);

			return nextState;
		});

	public readonly _selectionChange = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.currentScreenId = data.id;
			nextState.navHistory.push(nextState.currentScreenId);

			return nextState;
		});

	public readonly _navigateBack = StateBind
		.create<State>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.navHistory.pop();
			
			if (nextState.navHistory.length > 0) {
				var lastIndex = (nextState.navHistory.length - 1);
				nextState.currentScreenId = nextState.navHistory[lastIndex];
			}

			return nextState;
		});

	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public get resetState(): IDataBind {
		return this._resetState.expose();
	}

	public get selectionChange(): IDataBind {
		return this._selectionChange.expose();
	}

	public get navigateBack(): IDataBind {
		return this._navigateBack.expose();
	}

	public init(): void {
		this.resetState.trigger();
	}

	public onSave(): void {
		throw new Error("Method not implemented.");
	}
}
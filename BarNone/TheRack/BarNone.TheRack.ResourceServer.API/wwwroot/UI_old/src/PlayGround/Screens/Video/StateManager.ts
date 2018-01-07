import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";
import { AppScreen } from "Vee/Screen/AppScreen";
import DataManager from "Application/Data/DataManager";

class LineData {
	public x1: number;
	public y1: number;
	public x2: number;
	public y2: number;
}

export class State {
	public lineData: LineData[];
}

export class StateManager extends BaseStateManager<State> {
	public readonly _resetState = StateBind
		.create<State>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.lineData = data;
			//  [
			// 	{
			// 		x1: 50,
			// 		x2: 100,
			// 		y1: 50,
			// 		y2: 100
			// 	}
			// ]

			return nextState;
		});

	public constructor(screen: AppScreen) {
		super(screen, new State());
	}

	public get resetState(): IDataBind {
		return this._resetState.expose();
	}

	public async init(): Promise<void> {
		var data = await DataManager.Joints.load({ useOverride: true });
		this.resetState.trigger(data);
	}

	public onSave(): void {
		throw new Error("Method not implemented.");
	}
}
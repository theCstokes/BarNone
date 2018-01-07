import { BaseStateManager } from "Vee/StateManager/BaseStateManager";
import { AppScreen } from "Vee/Screen/AppScreen";
import DataManager from "Application/Data/DataManager";
import User from "Application/Data/Models/User/User";
import StateBind from "Vee/Core/DataBind/StateBind";
import { IDataBind } from "Vee/Core/DataBind/IDataBind";

// export class State {
// 	public userList: User[]
// 	public currentId: number;
// }

export interface ISelectionState<TData> {
	selectionList: TData[];
	selectionId: number;
}

export interface IListElement {
	id: number;
}

type SelectionListProvider<TData> = () => Promise<TData[]>;

export class SelectionStateManager<TData extends IListElement, TState extends ISelectionState<TData>>
	extends BaseStateManager<TState> {

		private _provider: SelectionListProvider<TData>;

	public constructor(screen: AppScreen, state: TState, provider: SelectionListProvider<TData>) {
		super(screen, state);
		this._provider = provider;
	}

	public readonly resetState = StateBind
		.create<TState>(this, true)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.selectionList = data;
			nextState.selectionId = nextState.selectionList[0].id;

			return nextState;
		}).expose();

		public readonly selectionChange = StateBind
		.create<TState>(this)
		.onAction((state, data) => {
			var nextState = Utils.clone(state);
			nextState.selectionId = data.id;

			return nextState;
		}).expose();

	public async init(): Promise<void> {
		var data = await this._provider();
		this.resetState.trigger(data);
	}

	public onSave(): void {
		throw new Error("Method not implemented.");
	}
}
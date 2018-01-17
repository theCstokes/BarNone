import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";

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

	public constructor(TStateType: { new(): TState }, provider: SelectionListProvider<TData>) {
		super(TStateType);
		this._provider = provider;
	}

	public readonly ResetState = StateBind
		.onAction<TState, TData[]>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.selectionList = data;
			nextState.current.selectionId = nextState.current.selectionList[0].id;

			return nextState;
		});

	public SelectionChange = StateBind
		.onAction<TState, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.selectionId = data.id;

			return nextState;
		});
	
	// public readonly resetState = StateBind
	// 	.create<TState>(this, true)
	// 	.onAction().expose();

	// public readonly selectionChange = StateBind
	// 	.create<TState>(this)
	// 	.onAction((state, data) => {
	// 		var nextState = Utils.clone(state);
	// 		nextState.selectionId = data.id;

	// 		return nextState;
	// 	}).expose();

	public async init(): Promise<void> {
		var data = await this._provider();
		this.ResetState.trigger(data);
	}

	public onSave(): void {
		throw new Error("Method not implemented.");
	}
}
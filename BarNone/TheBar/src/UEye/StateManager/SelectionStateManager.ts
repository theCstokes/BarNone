// import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";
// import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";

// export class SelectionState<SelectionItem extends IListItem> {
// 	public selectionList: SelectionItem;
// }

// export default abstract class SelectionStateManager<
// 	TState extends SelectionState<SelectionItem>,
// 	SelectionItem extends IListItem
// > extends BaseStateManager<TState> {

// 	public constructor(TStateType: { new(): TState }) {
// 		super(TStateType);
// 	}

// 	public Selection


// }

import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";

export interface ISelectionState<TData extends IListItem> {
	selectionList: TData[];
	selectionId: number | string;
}

export abstract class SelectionStateManager<
	TData extends IListItem,
	TState extends ISelectionState<TData>
	> extends BaseStateManager<TState> {

	private _TStateType: { new(): TState };

	public constructor(TStateType: { new(): TState }) {
		super(TStateType);
		this._TStateType = TStateType;
	}

	protected abstract async listProvider(): Promise<TData[]>;

	public readonly ResetState = StateBind
		.onAsyncCallable<TState>(this, async (state) => {
			var nextState = state.empty();
			nextState.current.selectionList = await this.listProvider();
			if (nextState.current.selectionList.length > 0) {
				nextState.current.selectionId = nextState.current.selectionList[0].id;
			}

			return nextState.initialize();
		});

	public SelectionChange = StateBind
		.onAction<TState, {
			id: number
		}>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.selectionId = data.id;

			return nextState;
		});

	// public async init(): Promise<void> {
	// 	var data = await this.onLoad();
	// 	this.ResetState.trigger(data);
	// }

	// public onSave(): void {
	// 	throw new Error("Method not implemented.");
	// }
}
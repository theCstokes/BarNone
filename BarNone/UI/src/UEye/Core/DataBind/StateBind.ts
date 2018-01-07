// import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

// type StateAction<TState, TData> = (state: TState, data: TData) => TState;

// export default class StateBind<TState, TData> {
//     private _stateManager: BaseStateManager<TState>;
//     private _action: StateAction<TState, TData>;

//     public constructor(stateManager: BaseStateManager<TState>, action: StateAction<TState, TData>) {
//         this._stateManager = stateManager;
//         this._action = action;
//     }

//     public trigger(data: TData): void {
//         var next_state = this._action(this._stateManager.getCurrentState(), data);
//         this._stateManager.updateState(next_state);
//     }
// }
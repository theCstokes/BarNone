// import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";
// import { ChildStateUpdater } from "UEye/StateManager/ChildStateManager";
// // import { start } from "repl";

// export default abstract class ParentStateManager<TState> extends BaseStateManager<TState> {
//     public constructor(TStateType: { new(): TState }) {
//         super(TStateType);
//     }

//     public updateSubState<TChildState>(updater: ChildStateUpdater<TChildState, TState>, state: StateTracker<TChildState>): void {
//         updater(this._stateTracker.current, state.current);
//         updater(this._stateTracker.original, state.original);
//     }
// }
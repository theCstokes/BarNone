import { BaseStateManager, StateTracker } from "UEye/StateManager/BaseStateManager";
// import { start } from "repl";
import ParentStateManager from "UEye/StateManager/ParentStateManager";

export type ChildStateAccessor<TState, TParentState> = (state: TParentState) => TState;
export type ChildStateUpdater<TState, TParentState> = (state: TParentState, data: TState) => void;

export default abstract class ChildStateManager<TState, TParentState> extends BaseStateManager<TState> {
    private _parentStateManager: ParentStateManager<TParentState>;
    private _TStateType: { new(): TState };
    private _accessor: ChildStateAccessor<TState, TParentState>;
    private _updater: ChildStateUpdater<TState, TParentState>;

    public constructor(parentStateManager: ParentStateManager<TParentState>, 
        TStateType: { new(): TState }, 
        accessor: ChildStateAccessor<TState, TParentState>,
        updater: ChildStateUpdater<TState, TParentState>) {
        super(TStateType);
        this._parentStateManager = parentStateManager;
        this._parentStateManager.bind(this._childRenderRouter.bind(this));
        this._TStateType = TStateType;
        this._accessor = accessor;
        this._updater = updater;
    }

    /**
	 * Gets state tracker object.
	 */
    public getState(): StateTracker<TState> {
        var stateTracker = new StateTracker<TState>(this._TStateType);
        // this._stateTracker.current
        var parent = this._parentStateManager.getState();
        stateTracker.current = this._accessor(parent.current);
        stateTracker.original = this._accessor(parent.original);
        
		return Utils.clone(stateTracker);
    }
    
    private _childRenderRouter(current: TParentState, original: TParentState): void {
        super.updateState(this.getState());
    }

    public updateState(state: StateTracker<TState>) {
        // var parent = this._parentStateManager.getState();
        // this._updater(parent.current, state.current);
        // this._updater(parent.original, state.original);

        // this._parentStateManager.updateState(parent);
        this._parentStateManager.updateSubState(this._updater, state);
        super.updateState(state);
		// if (this._stateTracker !== state) {
		// 	this._stateTracker = Utils.clone(state);
			
		// 	this._renderCallbackList.forEach(rc => rc(
        //         this.getCurrentState(),
        //         this.getOriginalState()
		// 	));
		// }
    }
    
    // private _get
}
import { BaseStateManager, StateTracker, RenderCallback } from "UEye/StateManager/BaseStateManager";
import { IStateBind } from "UEye/StateManager/StateBind";
// import { start } from "repl";
// import ParentStateManager from "UEye/StateManager/ParentStateManager";

export type ChildStateAccessor<TState, TParentState> = (state: TParentState) => TState;
export type ChildStateUpdater<TState, TParentState> = (state: TParentState, data: TState) => void;

export default abstract class ChildStateManager<TState, TParentState> extends BaseStateManager<TState> {
    private _parentStateManager: BaseStateManager<TParentState>;
    private _TStateType: { new(): TState };
    private _trackChildChanges: boolean;
    private _accessor: ChildStateAccessor<TState, TParentState>;
    private _updater: ChildStateUpdater<TState, TParentState>;

    /**
     * Construct new child state manager.
     * @param parentStateManager - parent state manager
     * @param TStateType - builder for child state
     * @param trackChildChanges - flag to trigger render on state update. 
     * if true render may be triggered if changes occurred
     * if false original state will be updated as well as current
     * @param accessor - get the child state object from parent state
     * @param updater - update the child state object from parent state
     */
    public constructor(
        parentStateManager: BaseStateManager<TParentState>, 
        TStateType: { new(): TState }, 
        trackChildChanges: boolean,
        accessor: ChildStateAccessor<TState, TParentState>,
        updater: ChildStateUpdater<TState, TParentState>) {
        super(TStateType);
        this._parentStateManager = parentStateManager;
        this._parentStateManager.bind(this._childRenderRouter.bind(this));
        this._trackChildChanges = trackChildChanges;
        this._TStateType = TStateType;
        this._accessor = accessor;
        this._updater = updater;
    }

    /** Gets state tracker object. 
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
        super.stateTransition(this.getState(), this.getState());
    }

    public stateTransition(
		originalState: StateTracker<TState>,
		nextState: StateTracker<TState>,
		id?: string
	) {
        var track = this._trackChildChanges;
        if (id !== undefined && this._trackChildChangesFrom.indexOf(id) > -1) {
            track = false;
        }

        this._parentStateManager.updateSubState(this._updater, nextState, track);
        super.stateTransition(originalState, nextState, id);
    }

    private _trackChildChangesFrom: string[] = [];
    public trackChildChangesFrom(bind: IStateBind) {
        this._trackChildChangesFrom.push(bind.id);
    }

    public bindToParent(renderCallback: RenderCallback<TState>) {
        this._parentStateManager.bind((current: TParentState, original: TParentState) => {
            let currentSub = this._accessor(current);
            let originalSub = this._accessor(original);
            renderCallback(currentSub, originalSub);
        });
    }
}
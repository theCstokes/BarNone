// import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import StateBind from "UEye/StateManager/StateBind";
import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State, StateManager } from "App/Screens/Nav/StateManager";

class CrumbElement {
    public value: string;
    public onClick: OnClickCallback;
}


export class ContextState {
    public crumbList: CrumbElement[] = [];
}

export class ContextStateManager extends ChildStateManager<ContextState, State> {
    public constructor(parentStateManager: StateManager) {
        super(parentStateManager, ContextState, 
            (state: State) => state.context,
            (state: State, data: ContextState) => state.context = data);
    }

    public readonly AddSubBreadcrumb = StateBind
        .onAction<ContextState, CrumbElement>(this, (state, data) => {
            var nextState = Utils.clone(state);

            nextState.current.crumbList.push(data);

            return nextState;
        });
}
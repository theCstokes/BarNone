// import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { OnClickCallback, OnSelectCallback, IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import StateBind from "UEye/StateManager/StateBind";
import ChildStateManager from "UEye/StateManager/ChildStateManager";
import { State, StateManager } from "App/Screens/Nav/StateManager";

class CrumbElement implements IListItem {
    public id: number | string;
    public value: string;
    public onClick: OnSelectCallback;
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

    public readonly PopSubBreadcrumbTo = StateBind
        .onAction<ContextState, IListItem>(this, (state, data) => {
            var nextState = Utils.clone(state);

            var targetIdx: number | undefined = undefined;

            // Remove all crumbs after the target.
            nextState.current.crumbList =
                nextState.current.crumbList.filter((crumb, idx) => {
                    if (crumb.id === data.id) {
                        targetIdx = idx;
                        return true;
                    }
                    return !(targetIdx !== undefined && idx > targetIdx);
                });

            return nextState;
        });
}
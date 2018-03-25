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

    public showHelp: boolean = false;
    
    public showNotifications: boolean = false;
}

export class ContextStateManager extends ChildStateManager<ContextState, State> {
    public constructor(parentStateManager: StateManager) {
        super(parentStateManager, ContextState,
            false,
            (state: State) => state.context,
            (state: State, data: ContextState) => state.context = data);
    }

    public async onInitialize(): Promise<void> { 	}

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

    public readonly ToggleShowHelp = StateBind
        .onCallable<ContextState>(this, (state) => {
            var nextState = Utils.clone(state);

            nextState.current.showNotifications = false;
            nextState.current.showHelp = (!state.current.showHelp);

            return nextState;
        });

    public readonly ToggleShowNotifications = StateBind
        .onCallable<ContextState>(this, (state)  => {
            var nextState = Utils.clone(state);
            
            nextState.current.showHelp = false;
            nextState.current.showNotifications = !state.current.showNotifications;

            return nextState;
        });
}
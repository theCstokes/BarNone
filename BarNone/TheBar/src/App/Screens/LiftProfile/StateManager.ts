import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
//import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import StateBind from "UEye/StateManager/StateBind";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";

export class State implements ISelectionState<LiftTypeItem> {
    public selectionList: LiftTypeItem[];
    public name: string;
    public selectionId: number;
    public parentID: number | null;
}

export class StateManager extends SelectionStateManager<LiftTypeItem, State> {

    public constructor() {
        super(State);

    }

    protected async listProvider(): Promise<LiftTypeItem[]> {
        return await DataManager.LiftTypes.all()
    }


    // public readonly ResetState = StateBind
    //     .onCallable<State>(this, (state) => {
    //         var nextState = state.empty();

    //         if (nextState.current.selectionList.length > 0) {
    //             var selection = nextState.current.selectionList[0];

    //             nextState.current.selectionId = selection.id;
    //         }

    //         return nextState.initialize();
    //     });

    // public readonly SelectionChange = StateBind
    //     .onAction<State, {
    //         id: number
    //     }>(this, (state, data) => {
    //         var nextState = Utils.clone(state);
    //         nextState.current.selectionId = data.id;

    //         // nextState.current.context.crumbList = [{
    //         //     id: Utils.guid(),
    //         //     value: selection.name,
    //         //     onClick: this._onBaseCrumbElementClickHandler.bind(this)
    //         // }];

    //         // nextState.current.navHistory.push(nextState.current.currentScreenId);


    //         return nextState;
    //     });


}
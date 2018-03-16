import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import { ISelectionState, SelectionStateManager } from "App/Core/StateManager/SelectionStateManager";
//import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import StateBind from "UEye/StateManager/StateBind";
import {LiftTypeItem} from "App/Screens/LiftProfile/Models";

export class State implements ISelectionState<LiftTypeItem> {
    public selectionList: LiftTypeItem[]=[
        {
			id: 1,
            name: "Snatch",
            
		
		},
		{
			id: 2,
			name: "Squat",
		
		},
		
		{
			id: 3,
			name: "Clean",
		
		},
		{	
			id: 4,
			name:"Jerk",
			
		}	
    ]
    ;
    public name: string;
	public selectionId: number;
	public parentID: number | null;
}

export class StateManager extends BaseStateManager< State> {
   
	public constructor() {
		super(State);
		
    }
    public readonly ResetState = StateBind
    .onCallable<State>(this, (state) => {
        var nextState = state.empty();

        if (nextState.current.selectionList.length > 0) {
            var selection = nextState.current.selectionList[0];

            nextState.current.selectionId = selection.id;
        }

        return nextState.initialize();
    });

public readonly SelectionChange = StateBind
    .onAction<State, {
        id: number
    }>(this, (state, data) => {
        var nextState = Utils.clone(state);
        nextState.current.selectionId = data.id;
    
            // nextState.current.context.crumbList = [{
            //     id: Utils.guid(),
            //     value: selection.name,
            //     onClick: this._onBaseCrumbElementClickHandler.bind(this)
            // }];

            // nextState.current.navHistory.push(nextState.current.currentScreenId);
      

        return nextState;
    });

       
}
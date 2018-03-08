import Screen from "UEye/Screen/Screen"
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView"
import UEye from "UEye/UEye";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import {StateManager, State} from "App/Screens/LiftProfile/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import EditScreen from "UEye/Screen/EditScreen";
import LiftProfileEditScreen from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditScreen"
import App from "App/App";
import LiftAnalysisProfile from "App/Data/Models/LiftAnalysisProfile/LiftAnalysisProfile";

export default class LiftProfileScreen extends Screen<LiftProfileView> {
    private subScreen: EditScreen<any, any>;
	private _stateManager: StateManager;
    //public static AnalysisProfile: DataEvent<AnalysisListItem>;
    public static TypeChange: DataEvent<LiftAnalysisProfile>;
    public constructor(){
        super(LiftProfileView);
        
		//LiftProfileScreen.TypeChange = new DataEvent();
		//LiftProfileScreen.TypeChange.on(this._onListOpenHandler.bind(this));
    }
    public onShow(): void {

		this._stateManager = new StateManager();
		this._stateManager.bind(this._onRender.bind(this));

		this.view.typeList.onSelect = (data: IListItem) => {
			this._stateManager.SelectionChange.trigger({ id: data.id });
		};

		//this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
		
		
		this._stateManager.ResetState.trigger();
    }
    private _onRender(current: State, original: State): void {
		this.view.typeList.items = current.selectionList.map(item => {
            console.log(item.name);
			return {
				selected: (item.id === current.selectionId),
				id: item.id,
				name: item.name,
				icon: "fa-universal-acesss"
			}
        });
        var userData = current.selectionList.find(item => {
			return (item.id === current.selectionId);
        });
        if (userData !== undefined) {
			if (this.subScreen !== undefined) {
				UEye.popTo(this);
            }
            this.subScreen = UEye.push(LiftProfileEditScreen, {
                id: userData.id,
                name: userData.name,
            }) as LiftProfileEditScreen;
        }
}


}
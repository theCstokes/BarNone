import Screen from "UEye/Screen/Screen"
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView"
import UEye from "UEye/UEye";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import { StateManager, State } from "App/Screens/LiftProfile/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import EditScreen from "UEye/Screen/EditScreen";
import LiftProfileEditScreen from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditScreen"
import App from "App/App";
import LiftAnalysisProfile from "App/Data/Models/LiftAnalysisProfile/LiftAnalysisProfile";
import { SelectionListScreen } from "UEye/Screen/SelectionListScreen";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";

export default class LiftProfileScreen
    extends SelectionListScreen<LiftProfileView, StateManager, LiftTypeItem, State> {

    // private subScreen: EditScreen<any, any>;
    // private _stateManager: StateManager;
    // //public static AnalysisProfile: DataEvent<AnalysisListItem>;
    // public static TypeChange: DataEvent<LiftAnalysisProfile>;
    public constructor() {
        super(LiftProfileView);
    }

    public onRenderEditScreen(data: LiftTypeItem): EditScreen<any, any> | undefined {
        return UEye.push(LiftProfileEditScreen, {
            id: data.id,
            name: data.name,
        }) as LiftProfileEditScreen;
    }

    public listTransform(item: LiftTypeItem): IListItem {
        return {
            selected: (item.id === this.stateManager.getCurrentState().selectionId),
            id: item.id,
            name: item.name,
            icon: "fa-universal-access",
            onOpen: () => {
                // this._onTypeSelectedHandler(item);
            }

        }
    }

    public async onShow(): Promise<void> {
        super.onShow();
        this.init(await StateManagerFactory.create(StateManager));
        this.stateManager.ResetState.trigger();
    }
}
import Screen from "UEye/Screen/Screen"
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView"
import UEye from "UEye/UEye";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import { StateManager, State } from "App/Screens/LiftProfile/StateManager";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import EditScreen from "UEye/Screen/EditScreen";
import LiftProfileEditScreen from "App/Screens/LiftProfile/LiftProfileEdit/LiftProfileEditScreen"
import { SelectionListScreen } from "UEye/Screen/SelectionListScreen";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { LiftTypeItem } from "App/Screens/LiftProfile/Models";
import LiftAnalysisProfile from "App/Data/Models/Lift/LiftAnalysisProfile";
import LiftProfileAddDialog from "App/Screens/LiftProfile/LiftProfileAdd/LiftProfileAddDialog";

export default class LiftProfileScreen
    extends SelectionListScreen<LiftProfileView, StateManager, LiftAnalysisProfile, State> {

    public constructor() {
        super(LiftProfileView, false);
    }

    public onRenderEditScreen(data: LiftAnalysisProfile): EditScreen<any, any> | undefined {
        if (this.subScreen === undefined) {
            return UEye.push(LiftProfileEditScreen, {
                liftProfileID: data.id,
                liftTypeID: data.liftTypeID
            }) as LiftProfileEditScreen;
        }

        this.subScreen.onShow({
            liftProfileID: data.id,
            liftTypeID: data.liftTypeID
        });
        return this.subScreen;
    }

    public listTransform(item: LiftAnalysisProfile): IListItem | undefined {
        let liftType = this.stateManager.s_LiftTypeList
            .find(type => (type.id === item.liftTypeID));
        if (liftType === undefined) return;

        return {
            selected: (item.id === this.stateManager.getCurrentState().selectionId),
            id: item.id,
            name: liftType.name,
            icon: "fa-universal-access",
            onOpen: () => {
                // this._onTypeSelectedHandler(item);
            }

        }
    }

    private _onRender(current: State, original: State) {
        // this.view.addButton
    }

    public async onShow(): Promise<void> {
        console.log("LiftProfileScreen Show.");

        this.view.addButton.onClick = () => {
            var dialog = UEye.push(LiftProfileAddDialog) as LiftProfileAddDialog;
            dialog.onAccept = async (state) => {
                if (state === undefined) return;
                if (state.liftTypeID === undefined) return;
                this.stateManager.LiftProfileAdd.trigger({ liftTypeID: state.liftTypeID });
            }
        }

        super.onShow();
        this.init(await StateManagerFactory.create(StateManager));
        this.stateManager.ResetState.trigger();
        this.stateManager.bind(this._onRender.bind(this));
    }
}
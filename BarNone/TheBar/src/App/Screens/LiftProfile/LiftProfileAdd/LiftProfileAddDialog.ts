import DialogScreen, { OnAcceptCallback } from "UEye/Screen/DialogScreen";
import LiftProfileDialogView from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LiftProfileAddView from "App/Screens/LiftProfile/LiftProfileAdd/LiftProfileAddView";
import { LiftProfileAddStateManager, LiftProfileAddState } from "App/Screens/LiftProfile/LiftProfileAdd/LiftProfileAddStateManager";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";

export default class LiftProfileAddDialog extends DialogScreen<
	LiftProfileAddView,
	LiftProfileAddStateManager,
	LiftProfileAddState>
{

	public constructor() {
		super(LiftProfileAddView);
	}

	private _pipeline = ScreenPipeLine.create()

		//#region Lift Type Drop Down.
		.onShow(() => {
			this.view.liftTypeDropDown.items = this.stateManager
				.s_LiftTypeList.filter(type => {
					let analysis = this.stateManager
						.s_LiftAnalysisProfile.find(p => p.liftTypeID === type.id);
					return (analysis === undefined);
				});

			this.view.liftTypeDropDown.onSelect = (data) => {
				this.stateManager.LiftTypeChange.trigger({ id: data.id });
			}
		})
		.onRender((current: LiftProfileAddState, original: LiftProfileAddState) => {
			this.view.liftTypeDropDown.modified = !Utils
				.equivalent(current.liftTypeID, original.liftTypeID);
			this.view.liftTypeDropDown.selected = this.view.liftTypeDropDown.items
				.find(item => item.id === current.liftTypeID)
		});
	//#endregion

	public async onShow(data: { onAccept: OnAcceptCallback<LiftProfileAddState> }): Promise<void> {
		this.stateManager = await StateManagerFactory.create(LiftProfileAddStateManager);
		super.onShow(data);
		this._pipeline.onShowInvokable();
		this.stateManager.bind(this._pipeline.onRenderInvokable);
		this.stateManager.CreateState.trigger();
	};
}



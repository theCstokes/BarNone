import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import JointType from "App/Data/Models/Joint/JointType";
import DataManager from "App/Data/DataManager";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";

export class LiftProfileDialogState {
	public analysisTypeID: number;
	public jointTypeIDA?: number
	public jointTypeIDB?: number
	public jointTypeIDC?: number
}

export class LiftProfileDialogStateManager extends BaseStateManager<LiftProfileDialogState> {

	public s_JointTypeList: JointType[];
	public s_AnalysisTypeList: AnalysisType[];

	public constructor() {
		super(LiftProfileDialogState);
	}

	public async onInitialize(): Promise<void> {
		this.s_JointTypeList = await DataManager.JointTypes.all();
		this.s_AnalysisTypeList = await DataManager.AnalysisTypes.all();
	}

	public readonly CreateState = StateBind
		.onCallable<LiftProfileDialogState>(this, (state) => {
			return Utils.clone(state);
		});

	public readonly AnalysisTypeChange = StateBind
		.onAction<LiftProfileDialogState, {
			id: number
		}>(this, (state, data) => {
			var next_state = Utils.clone(state);
			next_state.current.analysisTypeID = data.id;
			return next_state;
		});

	public readonly JointTypeChange = StateBind
		.onAction<LiftProfileDialogState, {
			jointTypeIDA?: number,
			jointTypeIDB?: number,
			jointTypeIDC?: number
		}>(this, (state, data) => {
			var next_state = Utils.clone(state);

			if (data.jointTypeIDA !== undefined) {
				next_state.current.jointTypeIDA = data.jointTypeIDA;
			}
			if (data.jointTypeIDB !== undefined) {
				next_state.current.jointTypeIDB = data.jointTypeIDB;
			}
			if (data.jointTypeIDC !== undefined) {
				next_state.current.jointTypeIDC = data.jointTypeIDC;
			}

			return next_state;
		});

}
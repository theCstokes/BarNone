import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import JointType from "App/Data/Models/Joint/JointType";
import DataManager from "App/Data/DataManager";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";
import LiftType from "App/Data/Models/Lift/LiftType";
import LiftAnalysisProfile from "App/Data/Models/Lift/LiftAnalysisProfile";

export class LiftProfileAddState {
	public liftTypeID?: number;
}

export class LiftProfileAddStateManager extends BaseStateManager<LiftProfileAddState> {

	public s_LiftTypeList: LiftType[];
	public s_LiftAnalysisProfile: LiftAnalysisProfile[]

	public constructor() {
		super(LiftProfileAddState);
	}

	public async onInitialize(): Promise<void> {
		this.s_LiftTypeList = await DataManager.LiftTypes.all();
		this.s_LiftAnalysisProfile = await DataManager.LiftAnalysisProfile.all();
	}

	public readonly CreateState = StateBind
		.onCallable<LiftProfileAddState>(this, (state) => {
			return Utils.clone(state);
		});

	public readonly LiftTypeChange = StateBind
		.onAction<LiftProfileAddState, {
			id: number
		}>(this, (state, data) => {
			var next_state = Utils.clone(state);
			next_state.current.liftTypeID = data.id;
			return next_state;
		});

		public async save() {

		}

}
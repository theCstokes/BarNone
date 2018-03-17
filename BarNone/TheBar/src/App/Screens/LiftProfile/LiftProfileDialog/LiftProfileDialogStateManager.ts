import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import JointType from "App/Data/Models/Joint/JointType";
import DataManager from "App/Data/DataManager";
import AnalysisType from "App/Data/Models/Analysis/AnalysisType";

export class State {

}

export class LiftProfileDialogStateManager extends BaseStateManager<State> {

	public s_JointTypeList: JointType[];
	public s_AnalysisTypeList: AnalysisType[];

	public constructor() {
		super(State);
	}

	public async onInitialize(): Promise<void> {
		this.s_JointTypeList = await DataManager.JointTypes.all();
	}

	public readonly CreateState = StateBind
		.onCallable<State>(this, (state) => {
			return state;
		});

}
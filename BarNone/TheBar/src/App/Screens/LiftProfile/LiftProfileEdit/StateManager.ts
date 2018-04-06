import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import StateBind from "UEye/StateManager/StateBind";
import DataManager from "App/Data/DataManager";
import { LiftProfileState } from "App/Screens/LiftProfile/LiftProfileEdit/Tabs/Profile/LiftProfileStateManager";
import AccelerationAnalysisCriteria from "App/Data/Models/Lift/Analysis/AccelerationAnalysisCriteria";
import SpeedAnalysisCriteria from "App/Data/Models/Lift/Analysis/SpeedAnalysisCriteria";
import PositionAnalysisCriteria from "App/Data/Models/Lift/Analysis/PositionAnalysisCriteria";
import AngleAnalysisCriteria from "App/Data/Models/Lift/Analysis/AngleAnalysisCriteria";
import LiftType from "App/Data/Models/Lift/LiftType";

export class State {
	public id: number;
	public name: string = "";
	public liftType: LiftType;
	public liftProfileState: LiftProfileState = new LiftProfileState();
}

export class StateManager extends BaseStateManager<State> {

	public constructor() {
		super(State);
	}

	public readonly ResetState = StateBind
		.onAsyncAction<State, {
			liftTypeID: number
		}>(this, async (state, data) => {
			var nextState = state.empty();

			nextState.current.id = data.liftTypeID;

			let liftType = await DataManager.LiftTypes.single(data.liftTypeID);
			nextState.current.name = liftType.name;
			nextState.current.liftType = liftType;

			return nextState.initialize();
		});

	public readonly NameChange = StateBind
		.onAction<State, string>(this, (state, data) => {
			var nextState = Utils.clone(state);
			nextState.current.name = data as string;

			return nextState;
		});


	// public constructor(screen: AppScreen) {
	// 	super(screen, new State());
	// }

	// public get resetState(): IDataBind {
	// 	return this._resetState.expose();
	// }

	// public get nameChange(): IDataBind {
	// 	return this._nameChange.expose();
	// }

	public init(): void {
		// var data = await DataManager.Lifts.single()
		// this.ResetState.trigger();
	}

	public async save(): Promise<void> {
		console.log("save");
		let current = this.getCurrentState();
		let accelerationCriteriaList: AccelerationAnalysisCriteria[]
			= current.liftProfileState.accelerationCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return <AccelerationAnalysisCriteria>{
						jointTypeID: p.jointTypeID
					}
				});

		let speedCriteriaList: SpeedAnalysisCriteria[]
			= current.liftProfileState.speedCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return <SpeedAnalysisCriteria>{
						jointTypeID: p.jointTypeID
					}
				});

		let positionCriteriaList: PositionAnalysisCriteria[]
			= current.liftProfileState.positionCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return <PositionAnalysisCriteria>{
						jointTypeID: p.jointTypeID
					}
				});

		let angleCriteriaList: AngleAnalysisCriteria[]
			= current.liftProfileState.angleCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return <AngleAnalysisCriteria>{
						jointTypeAID: p.jointTypeAID,
						jointTypeBID: p.jointTypeBID,
						jointTypeCID: p.jointTypeBID,
					}
				});

		let updateFilter = [];
		if (accelerationCriteriaList.length > 0) updateFilter.push("accelerationAnalysisCriteria");
		if (positionCriteriaList.length > 0) updateFilter.push("positionAnalysisCriteria");
		if (speedCriteriaList.length > 0) updateFilter.push("speedAnalysisCriteria");
		if (angleCriteriaList.length > 0) updateFilter.push("angleAnalysisCriteria");

		await DataManager.LiftAnalysisProfile.update(current.liftProfileState.liftProfileID, {
			id: current.id,
			updateFilter: updateFilter,
			details: {
				accelerationAnalysisCriteria: accelerationCriteriaList,
				positionAnalysisCriteria: positionCriteriaList,
				speedAnalysisCriteria: speedCriteriaList,
				angleAnalysisCriteria: angleCriteriaList
			}
		});

		// await DataManager.Users.update(currentState.id, {
		// 	id: currentState.id,
		// 	name: currentState.name,
		// 	userName: currentState.name,
		// 	password: ""
		// });
	}
}
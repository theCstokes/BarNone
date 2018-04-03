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
					return {
						jointTypeID: p.jointTypeID
					}
				}) as AccelerationAnalysisCriteria[];

		let speedCriteriaList: SpeedAnalysisCriteria[]
			= current.liftProfileState.speedCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return {
						jointTypeID: p.jointTypeID
					}
				}) as SpeedAnalysisCriteria[];

		let positionCriteriaList: PositionAnalysisCriteria[]
			= current.liftProfileState.positionCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return {
						jointTypeID: p.jointTypeID
					}
				}) as PositionAnalysisCriteria[];

		let angleCriteriaList: AngleAnalysisCriteria[]
			= current.liftProfileState.angleCriteriaList
				.filter(p => p.isNew)
				.map(p => {
					return {
						jointTypeIDA: p.jointTypeAID,
						jointTypeIDB: p.jointTypeBID,
						jointTypeIDC: p.jointTypeBID,
					}
				}) as AngleAnalysisCriteria[];

		await DataManager.LiftAnalysisProfile.update(current.id, {
			id: current.id,
			updateFilter: [
				"accelerationAnalysis",
				"positionAnalysisCriteria",
				"speedAnalysisCriteria",
				"angleAnalysisCriteria"
			],
			details: {
				accelerationAnalysis: accelerationCriteriaList,
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
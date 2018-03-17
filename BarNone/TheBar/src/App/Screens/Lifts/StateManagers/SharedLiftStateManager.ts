import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftListItem, LiftListType } from "App/Screens/Lifts/Models";
import StateBind from "UEye/StateManager/StateBind";
import { ISelectionState, SelectionStateManager } from "UEye/StateManager/SelectionStateManager";
import { BaseLiftStateManager } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";

export class SharedLiftStateManager extends BaseLiftStateManager {

	protected async listProvider(): Promise<LiftListItem[]> {
		return await this.onLoad();
	}

	protected async onLoad(parentID: number | null = null): Promise<LiftListItem[]> {
			let results = await Promise.all([
				DataManager.SharedLifts.all()
			]);
	
			return results[0].map(l => {
				return {
					id: l.id,
					name: l.name,
					type: LiftListType.Lift,
					parentID: l.parentID
				}
			});
	}
}
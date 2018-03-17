import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import LiftFolder from "App/Data/Models/LiftFolder/LiftFolder";
import DataManager from "App/Data/DataManager";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftListItem, LiftListType } from "App/Screens/Lifts/Models";
import StateBind from "UEye/StateManager/StateBind";
import { ISelectionState, SelectionStateManager } from "UEye/StateManager/SelectionStateManager";
import { BaseLiftStateManager } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";

export class LiftStateManager extends BaseLiftStateManager {

	protected async listProvider(): Promise<LiftListItem[]> {
		return await this.onLoad();
	}

	protected async onLoad(parentID: number | null = null): Promise<LiftListItem[]> {
		let results = await Promise.all([
			DataManager.LiftFolders.all({
				filter: {
					property: (l) => l.parentID,
					comparisons: "eq",
					value: parentID
				}
			}
			),
			DataManager.Lifts.all({
				filter: {
					property: (l) => l.parentID,
					comparisons: "eq",
					value: parentID
				}
			})
		]);

		return results[0].map(lf => {
			return {
				id: lf.id,
				name: lf.name,
				type: LiftListType.Folder,
				parentID: lf.parentID
			}
		}).concat(results[1].map(l => {
			return {
				id: l.id,
				name: l.name,
				type: LiftListType.Lift,
				parentID: l.parentID
			}
		}));

	}
}
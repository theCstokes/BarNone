import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

export default class StateManagerFactory {
	public static async create<
		TStateManager extends BaseStateManager<TState>,
		TState>(TSateManagerBuilder: { new(...args: any[]): TStateManager }, ...args: any[]): Promise<TStateManager> {
			var StateManager = new TSateManagerBuilder(...args);
			await StateManager.initialize();
			return StateManager;
	}

	// public create<
	// TStateManager extends BaseStateManager<TState>,
	// 	TState>(TSateManagerBuilder: { new(): TStateManager }): TStateManager {
	// 		var StateManager = new TSateManagerBuilder();
	// 		StateManager.onInitialize();
	// 		return StateManager;
	// }
}
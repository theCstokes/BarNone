import { AppScreen } from "UEye/Screen/AppScreen";
import ControlTypes from "UEye/ControlTypes";
import { View } from "UEye/View/View";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

export default class BasicScreen<TStateManager extends BaseStateManager<any>> extends AppScreen {
	private _stateManager: TStateManager;

	public constructor(ViewType: { new(): View },
		StateManagerType: { new(screen: AppScreen): TStateManager },
		isTrackScreen: boolean = false) {
		super(ViewType, isTrackScreen);
		this._stateManager = new StateManagerType(this);
	}

	public get content(): any[] {
		return [
			{
				instance: ControlTypes.Screen,
				content: this.view.content
			}
		]
	}

	public get stateManager(): TStateManager {
		return this._stateManager;
	}

	public onShow(data?: any): void {
		this.stateManager.init();
	}
}
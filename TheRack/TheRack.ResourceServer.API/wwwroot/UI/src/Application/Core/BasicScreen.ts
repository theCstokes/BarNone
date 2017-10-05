import { AppScreen } from "Vee/Screen/AppScreen";
import ControlTypes from "Vee/ControlTypes";
import { View } from "Vee/View/View";

export default class BasicScreen<TStateManager> extends AppScreen {
	public onShow(data?: any): void {
		
	}
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
}
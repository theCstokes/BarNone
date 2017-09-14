import { View } from "Vee/View/View";
import Core from "Vee/Elements/Core/Core";

export default class AppScreen {
	// private _viewType: { new(screen: Screen): View };
	private _view: View;
	
	public constructor(viewType: { new(): View }) {
		// this._viewType = viewType;
		this._view = new viewType();
	}

	public get view(): View {
		return this._view;
	}

	// public create(): void {
	// 	Core.inflate
	// }
}
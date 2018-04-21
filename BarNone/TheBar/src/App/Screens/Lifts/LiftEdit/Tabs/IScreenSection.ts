
import { View } from "UEye/View/View";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

export abstract class ScreenSection<
	TView extends View,
	TStateManager extends BaseStateManager<any>
	> {

		private _view: TView;
		private _parentStateManager: TStateManager;

		public constructor(view: TView, parentStateManager: TStateManager) {
			this._view = view;
			this._parentStateManager = parentStateManager;
		}

		public get view(): TView { return this._view; }
		public get parentStateManager(): TStateManager { return this._parentStateManager; }

		abstract async onShow(data?: any): Promise<void>;
}
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import { DialogView } from "UEye/View/DialogView";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { OnClickCallback, OnActionCallback } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

export type OnAcceptCallback<TData> = (data?: TData) => void;

export default class DialogScreen<
	TView extends DialogView,
	TStateManager extends BaseStateManager<TState>,
	TState
	> extends Screen<TView> {

	private _onAccept: OnAcceptCallback<TState>;
	protected stateManager: TStateManager;

	public constructor(ViewType: { new(): TView }) {
		super(ViewType);
	}

	public configure(): IScreenConfig {
		return {
			addScreenToHistory: true,
			fullScreen: true
		}
	}

	// public set onCancel(value: OnClickCallback) {
	// 	this._onCancel = value;
	// }

	public set onAccept(value: OnAcceptCallback<TState>) {
		this._onAccept = value;
	}

	private _onAcceptHandler() {
		if (this._onAccept !== undefined) {
			if (this.stateManager !== undefined) {
				this._onAccept(this.stateManager.getCurrentState());
			} else {
				this._onAccept();
			}
			UEye.pop();
		}
	}

	private _basePipeline = ScreenPipeLine.create()
		.onShow(() => {
			this.view.cancelButton.onClick = () => UEye.pop();
			this.view.acceptButton.onClick = this._onAcceptHandler.bind(this);
		});

	public onShow(data?: { onAccept: OnAcceptCallback<TState> }): void {
		if (data !== undefined) {
			this._onAccept = data.onAccept;
		}

		this._basePipeline.onShowInvokable();

		for (var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "blur(5px)";
		}
	}

	public destroy(): void {
		for (var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "";
		}
		super.destroy();
	}
}
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import { DialogView } from "UEye/View/DialogView";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";

export default class DialogScreen<TView extends DialogView> extends Screen<TView> {
	// private _onCancel: OnClickCallback;
	private _onAccept: OnClickCallback;

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

	public set onAccept(value: OnClickCallback) {
		this._onAccept = value;
	}

	private _onAcceptHandler() {
		if (this._onAccept !== undefined) {
			this._onAccept();
		}
	}

	private _basePipeline = ScreenPipeLine.create()
	.onShow(() => {
		this.view.cancelButton.onClick = () => UEye.pop();
		this.view.acceptButton.onClick = this._onAcceptHandler.bind(this);
	});

	public onShow(): void {
		this._basePipeline.onShowInvokable();

		for(var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "blur(5px)";
		}
	}

	public destroy(): void {
		for(var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "";
		}
		super.destroy();
	}
}
import DialogScreen from "UEye/Screen/DialogScreen";
import CancelDialogView from "UEye/View/CancelDialog/CancelDialogView";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";

export default class CancelDialogScreen extends DialogScreen<CancelDialogView> {
	private _onNo: OnClickCallback;
	private _onYes: OnClickCallback;

	public constructor() {
		super(CancelDialogView);
	}

	public onShow() {
		super.onShow();
		this._pipeline.onShowInvokable();
	}

	private _pipeline = ScreenPipeLine.create()
	.onShow(() => {
		this.view.noButton.onClick = this._onNoHandler.bind(this);
		this.view.yesButton.onClick = this._onYesHandler.bind(this);
	})

	private _onNoHandler() {
		if (this._onNo !== undefined) {
			this._onNo();
		}
	}

	private _onYesHandler() {
		if (this._onYes !== undefined) {
			this._onYes();
		}
	}

	public set onNo(value: OnClickCallback) {
		this._onNo = value;
	}

	public set onYes(value: OnClickCallback) {
		this._onYes = value;
	}
}
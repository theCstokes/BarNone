import DialogScreen from "UEye/Screen/DialogScreen";
import CancelDialogView from "UEye/View/CancelDialog/CancelDialogView";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";

export default class CancelDialogScreen extends DialogScreen<CancelDialogView, any, any> {
	// private _onNo: OnClickCallback;
	// private _onAccept: OnClickCallback;

	public constructor() {
		super(CancelDialogView);
	}

	public onShow() {
		super.onShow();
		this.view.acceptButton.enabled = true;
		// this._pipeline.onShowInvokable();
	}

	// private _pipeline = ScreenPipeLine.create()
	// .onShow(() => {
	// 	// this..onClick = this._onNoHandler.bind(this);
	// 	this.onAccept = this._onAcceptHandler.bind(this);
	// })

	// private _onNoHandler() {
	// 	if (this._onNo !== undefined) {
	// 		this._onNo();
	// 	}
	// }

	// private _onAcceptHandler() {
	// 	if (this._onAccept !== undefined) {
	// 		this._onAccept();
	// 	}
	// }

	// public set onNo(value: OnClickCallback) {
	// 	this._onNo = value;
	// }

	// public set onAccept(value: OnClickCallback) {
	// 	this._onAccept = value;
	// }
}
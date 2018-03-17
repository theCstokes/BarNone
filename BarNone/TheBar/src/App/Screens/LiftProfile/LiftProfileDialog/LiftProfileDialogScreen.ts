import DialogScreen from "UEye/Screen/DialogScreen";
import LiftProfileDialogView from "App/Screens/LiftProfile/LiftProfileDialog/LiftProfileDialogView";
import DataManager from "App/Data/DataManager";
import UEye from "UEye/UEye";
import LiftProfileScreen from "App/Screens/LiftProfile/LiftProfileScreen";

export default class LiftProfileDialogScreen extends DialogScreen<LiftProfileDialogView> {
	public constructor() {
		super(LiftProfileDialogView);
	}

	public onShow(): void {
		super.onShow();
		// TODO - remove hard code login.


		// this.view.cancelButton.onClick = () => {
		// 	UEye.pop();
		// }


	};
}



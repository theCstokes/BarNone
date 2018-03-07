import Screen from "UEye/Screen/Screen"
import LiftProfileView from "App/Screens/LiftProfile/LiftProfileView"
import UEye from "UEye/UEye";

export default class LiftProfileScreen extends Screen<LiftProfileView> {

    public constructor(){
        super(LiftProfileView);
    }
    public onShow(data?: any): void {
        throw new Error("Method not implemented.");
    }
}
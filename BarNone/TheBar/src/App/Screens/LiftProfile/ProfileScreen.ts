import Screen from "UEye/Screen/Screen"
import ProfileView from "App/Screens/LiftProfile/ProfileView"
import UEye from "UEye/UEye";

export default class ProfileScreen extends Screen<ProfileView> {

    public constructor(){
        super(ProfileView);
    }
    public onShow(data?: any): void {
        throw new Error("Method not implemented.");
    }
}
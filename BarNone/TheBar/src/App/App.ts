import UEye from "UEye/UEye";
import NavScreen from "App/Screens/Nav/NavScreen";
import LoginScreen from "App/Screens/Login/LoginScreen";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
// import { StateManager } from "App/Screens/Nav/StateManager";
import { ContextStateManager } from "App/Screens/Nav/ContextStateManager";
import DataManager from "App/Data/DataManager";
import RefreshTokenScreen from "App/Screens/RefreshToken/RefreshTokenScreen";
// import HomeScreen from "App/Screens/HomeScreen";

export default class App {
    public static Navigation: ContextStateManager;
    public static breadcrumbs: Breadcrumb;

    public static start(): void {

        DataManager.onAuthExpire.register(() => {
            UEye.push(RefreshTokenScreen);
        });

        // UEye.push(NavScreen);
        UEye.push(LoginScreen);
    }
}
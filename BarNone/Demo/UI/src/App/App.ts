import UEye from "UEye/UEye";
import NavScreen from "App/Screens/Nav/NavScreen";
import LoginScreen from "App/Screens/Login/LoginScreen";
// import HomeScreen from "App/Screens/HomeScreen";

export default class App {
    public static start(): void {
        // UEye.push(NavScreen);
        UEye.push(LoginScreen);
    }
}
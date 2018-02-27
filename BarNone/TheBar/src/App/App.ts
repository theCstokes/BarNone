import UEye from "UEye/UEye";
import NavScreen from "App/Screens/Nav/NavScreen";
import LoginScreen from "App/Screens/Login/LoginScreen";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
// import { StateManager } from "App/Screens/Nav/StateManager";
import { ContextStateManager } from "App/Screens/Nav/ContextStateManager";
import DataManager from "App/Data/DataManager";
import RefreshTokenScreen from "App/Screens/RefreshToken/RefreshTokenScreen";
import Toast from "UEye/Elements/Components/Toast/Toast";
import { BaseApp } from "UEye/BaseApp";
// import HomeScreen from "App/Screens/HomeScreen";

export default class App extends BaseApp {
    public static Navigation: ContextStateManager;
    public static breadcrumbs: Breadcrumb;

    public constructor() {
        super("app");
    }

    protected onStartup(): void {

        DataManager.onAuthExpire.register(() => {
			console.log("open");
            UEye.push(RefreshTokenScreen);
        });

        UEye.push(LoginScreen);

        // var ws = new WebSocket("ws://localhost:58428");   
		// ws.onopen = () => {
		// 		console.log("onopen");

		// 		// ws.send("Hello World!");
		// };    
		// ws.onmessage = function(e) {
		// 		console.log("echo from server : " + e.data);
		// 		App.Toast.showMessage(e.data);   
		// };

		// ws.onclose = function() {   
		// 		console.log("onclose");
		// };
		// ws.onerror = function() {
		// 		console.log("onerror");    
		// };
    }
}
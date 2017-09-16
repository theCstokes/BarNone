var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Screen/AppScreen", "Vee/Screen/ScreenBind", "Application/Login/LoginView", "Vee/Vee", "Application/Nav/NavScreen", "Application/User/UserScreen", "Vee/DataManager"], function (require, exports, AppScreen_1, ScreenBind_1, LoginView_1, Vee_1, NavScreen_1, UserScreen_1, DataManager_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    // import { StateManager, State } from "Application/User/Edit/StateManager";
    class LoginScreen extends AppScreen_1.default {
        // private _stateManager: StateManager;
        constructor() {
            super(LoginView_1.default);
            this.loginBind = ScreenBind_1.default
                .create(this, "loginButton")
                .onClick(() => __awaiter(this, void 0, void 0, function* () {
                if (yield DataManager_1.default.authorize(this.view.usernameInput.text, this.view.passwordInput.text)) {
                    Vee_1.default.pop();
                    yield Vee_1.default.push(NavScreen_1.default);
                    yield Vee_1.default.push(UserScreen_1.default);
                }
            }));
            // this._stateManager = new StateManager(this);
        }
    }
    exports.default = LoginScreen;
});

//# sourceMappingURL=LoginScreen.js.map

define(["require", "exports", "Vee/Screen/AppScreen", "PlayGround/User/UserView", "Vee/Vee", "PlayGround/User/Edit/UserEditScreen"], function (require, exports, AppScreen_1, UserView_1, Vee_1, UserEditScreen_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserScreen extends AppScreen_1.default {
        constructor() {
            super(UserView_1.default);
        }
        onShow() {
            Vee_1.default.push(UserEditScreen_1.default);
        }
    }
    exports.default = UserScreen;
});

//# sourceMappingURL=UserScreen.js.map

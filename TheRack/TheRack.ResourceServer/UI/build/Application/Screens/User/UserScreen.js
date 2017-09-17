var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Screen/AppScreen", "Vee/Vee", "Application/Screens/User/UserView", "Application/Screens/User/Edit/UserEditScreen", "Application/Data/DataManager"], function (require, exports, AppScreen_1, Vee_1, UserView_1, UserEditScreen_1, DataManager_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserScreen extends AppScreen_1.default {
        constructor() {
            super(UserView_1.default);
        }
        onShow() {
            return __awaiter(this, void 0, void 0, function* () {
                Vee_1.default.push(UserEditScreen_1.default);
                console.log(yield DataManager_1.default.Users.load());
            });
        }
    }
    exports.default = UserScreen;
});

//# sourceMappingURL=UserScreen.js.map

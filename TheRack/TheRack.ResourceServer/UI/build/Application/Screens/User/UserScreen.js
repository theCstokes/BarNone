var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Screen/AppScreen", "Vee/Vee", "Application/Screens/User/UserView", "Application/Screens/User/Edit/UserEditScreen", "Vee/Screen/ScreenBind", "Application/Screens/User/StateManager"], function (require, exports, AppScreen_1, Vee_1, UserView_1, UserEditScreen_1, ScreenBind_1, StateManager_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserScreen extends AppScreen_1.default {
        constructor() {
            super(UserView_1.default);
            this.userListBind = ScreenBind_1.default
                .create(this, "userList")
                .onSelect((data) => __awaiter(this, void 0, void 0, function* () {
                // console.log(data);
                Vee_1.default.pop();
                this._stateManager.selectionChange.trigger({
                    id: data.id
                });
            }))
                .onRender((original, current) => __awaiter(this, void 0, void 0, function* () {
                this.view.userList.items = current.userList.map(item => {
                    return {
                        selected: (item.id === current.currentId),
                        id: item.id,
                        name: item.name
                    };
                });
                var userData = current.userList.find(item => {
                    return (item.id === current.currentId);
                });
                yield Vee_1.default.push(UserEditScreen_1.default, userData);
            }));
            this._stateManager = new StateManager_1.StateManager(this);
        }
        onShow() {
            return __awaiter(this, void 0, void 0, function* () {
                this._stateManager.init();
                // Vee.push(UserEditScreen);
            });
        }
    }
    exports.default = UserScreen;
});

//# sourceMappingURL=UserScreen.js.map

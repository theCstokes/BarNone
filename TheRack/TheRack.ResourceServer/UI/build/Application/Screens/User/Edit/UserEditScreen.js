define(["require", "exports", "Vee/Screen/AppScreen", "Vee/Screen/ScreenBind", "Application/Screens/User/Edit/StateManager", "Application/Screens/User/Edit/UserEditView"], function (require, exports, AppScreen_1, ScreenBind_1, StateManager_1, UserEditView_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserEditScreen extends AppScreen_1.default {
        constructor() {
            super(UserEditView_1.default);
            this.nameBind = ScreenBind_1.default
                .create(this, "nameInput")
                .onChange(data => {
                this._stateManager.nameChange.trigger(data);
            })
                .onRender((original, current) => {
                this.view.nameInput.text = current.name;
                this.view.nameInput.modified = (original.name !== current.name);
            });
            this.panelBind = ScreenBind_1.default
                .create(this, "editPanel")
                .onRender((original, current) => {
                this.view.editPanel.modified = (JSON.stringify(original) !== JSON.stringify(current));
            });
            this._stateManager = new StateManager_1.StateManager(this);
        }
    }
    exports.default = UserEditScreen;
});

//# sourceMappingURL=UserEditScreen.js.map

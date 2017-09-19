var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/StateManager/BaseStateManager", "Vee/StateManager/StateBind", "Application/Data/DataManager"], function (require, exports, BaseStateManager_1, StateBind_1, DataManager_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class State {
    }
    exports.State = State;
    class StateManager extends BaseStateManager_1.BaseStateManager {
        constructor(screen) {
            super(screen, new State());
            this.resetState = StateBind_1.default
                .create(this, true)
                .onAction((state, data) => {
                var nextState = Utils.clone(state);
                nextState.userList = data;
                nextState.currentId = nextState.userList[0].id;
                return nextState;
            });
            this.selectionChange = StateBind_1.default
                .create(this)
                .onAction((state, data) => {
                var nextState = Utils.clone(state);
                nextState.currentId = data.id;
                return nextState;
            });
        }
        init() {
            return __awaiter(this, void 0, void 0, function* () {
                var data = yield DataManager_1.default.Users.load();
                this.resetState.trigger(data);
            });
        }
    }
    exports.StateManager = StateManager;
});

//# sourceMappingURL=StateManager.js.map

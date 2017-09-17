define(["require", "exports", "Vee/StateManager/BaseStateManager", "Vee/StateManager/StateBind"], function (require, exports, BaseStateManager_1, StateBind_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class State {
        constructor() {
            this.name = "";
        }
    }
    exports.State = State;
    class StateManager extends BaseStateManager_1.default {
        constructor(screen) {
            super(screen, new State());
            this.nameChange = StateBind_1.default
                .create(this)
                .onAction((state, data) => {
                var nextState = Utils.clone(state);
                nextState.name = data;
                return nextState;
            });
        }
    }
    exports.StateManager = StateManager;
});

//# sourceMappingURL=StateManager.js.map

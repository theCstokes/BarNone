define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class StateBind {
        constructor(stateManager) {
            this._stateManager = stateManager;
        }
        onAction(callback) {
            this._callback = callback;
            return this;
        }
        trigger(data) {
            var nextState = this._callback(this._stateManager.getCurrentState(), data);
            this._stateManager.updateState(nextState);
        }
        static create(stateManager) {
            return new StateBind(stateManager);
        }
    }
    exports.default = StateBind;
});

//# sourceMappingURL=StateBind.js.map

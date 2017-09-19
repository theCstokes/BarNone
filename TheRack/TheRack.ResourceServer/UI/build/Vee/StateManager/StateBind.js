define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class StateBind {
        constructor(stateManager, reset = false) {
            this._stateManager = stateManager;
            this._reset = reset;
        }
        onAction(callback) {
            this._callback = callback;
            return this;
        }
        trigger(data) {
            var nextState = this._callback(this._stateManager.getCurrentState(), data);
            this._stateManager.updateState(nextState, this._reset);
        }
        static create(stateManager, reset = false) {
            return new StateBind(stateManager, reset);
        }
    }
    exports.default = StateBind;
});

//# sourceMappingURL=StateBind.js.map

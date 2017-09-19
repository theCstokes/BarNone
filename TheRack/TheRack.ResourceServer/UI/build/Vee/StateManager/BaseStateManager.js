define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseStateManager {
        constructor(screen, state) {
            this._screen = screen;
            this._currentState = Utils.clone(state);
            this._originalState = Utils.clone(state);
        }
        getCurrentState() {
            return this._currentState;
        }
        getOriginalState() {
            return this._originalState;
        }
        updateState(state, reset = false) {
            if (this._currentState !== state) {
                this._currentState = Utils.clone(state);
                if (reset) {
                    this._originalState = Utils.clone(state);
                }
                this._screen.trigger("onRender", this._originalState, this._currentState);
            }
        }
        get screen() {
            return this._screen;
        }
    }
    exports.BaseStateManager = BaseStateManager;
});

//# sourceMappingURL=BaseStateManager.js.map

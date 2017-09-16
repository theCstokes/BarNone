define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ScreenBind {
        constructor(screen, controlName) {
            this._screen = screen;
            this._controlName = controlName;
            this._controlCallbacks = {};
            this._screen.bind("onShow", () => {
                for (var key in this._controlCallbacks) {
                    if (!this._controlCallbacks.hasOwnProperty(key))
                        continue;
                    if (this._screen.view[this._controlName] === undefined)
                        continue;
                    this._screen.view[this._controlName][key] = this._controlCallbacks[key];
                }
            });
            this._screen.bind("onRender", (original, current) => {
                this._onRenderCallback(original, current);
            });
        }
        onChange(callback) {
            this._controlCallbacks["onChange"] = callback;
            return this;
        }
        onClick(callback) {
            this._controlCallbacks["onClick"] = callback;
            return this;
        }
        onRender(callback) {
            this._onRenderCallback = callback;
            return this;
        }
        static create(screen, controlName) {
            return new ScreenBind(screen, controlName);
        }
    }
    exports.default = ScreenBind;
});

//# sourceMappingURL=ScreenBind.js.map

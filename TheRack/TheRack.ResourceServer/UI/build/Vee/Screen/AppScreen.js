define(["require", "exports", "Vee/ControlTypes"], function (require, exports, ControlTypes_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class AppScreen {
        constructor(viewType) {
            // this._viewType = viewType;
            this._view = new viewType();
            this._eventBinds = {};
        }
        get view() {
            return this._view;
        }
        get screenContent() {
            return [
                {
                    id: "screenControl",
                    instance: ControlTypes_1.default.Screen,
                    content: this.view.content
                }
            ];
        }
        get screenControl() {
            return this._screenControl;
        }
        set screenControl(value) {
            this._screenControl = value;
        }
        bind(name, callback) {
            if (this._eventBinds[name] === undefined) {
                this._eventBinds[name] = [];
            }
            this._eventBinds[name].push(callback);
        }
        trigger(name, ...data) {
            if (name in this) {
                this[name].apply(this, data);
            }
            if (this._eventBinds[name] !== undefined) {
                this._eventBinds[name].forEach((callback) => {
                    callback.apply(this, data);
                });
            }
        }
    }
    exports.default = AppScreen;
});

//# sourceMappingURL=AppScreen.js.map

define(["require", "exports", "Vee/Elements/Core/Core"], function (require, exports, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseElement {
        constructor(parent, ...styles) {
            this._element = Core_1.default.create('div', parent, ...styles);
        }
        get element() {
            return this._element;
        }
        get id() {
            return this._id;
        }
        set id(value) {
            this._id = value;
        }
        get modified() {
            return this._modified;
        }
        set modified(value) {
            this._modified = value;
            this.onModifiedChange();
        }
        get readonly() {
            return this._readonly;
        }
        set readonly(value) {
            this._readonly = value;
            this.onReadonlyChange();
        }
        get error() {
            return this._error;
        }
        set error(value) {
            this._error = value;
            this.onErrorChange();
        }
        onShow() {
        }
    }
    exports.BaseElement = BaseElement;
});

//# sourceMappingURL=BaseElement.js.map

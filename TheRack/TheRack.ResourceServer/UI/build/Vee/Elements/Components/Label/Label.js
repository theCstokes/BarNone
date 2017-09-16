define(["require", "exports", "Vee/Elements/Core/BaseComponent/BaseComponent", "Vee/Elements/Core/Core"], function (require, exports, BaseComponent_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Label extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Label");
        }
        set text(value) {
            this._text = value;
            this.element.textContent = this._text;
        }
        get text() {
            return this._text;
        }
        onModifiedChange() {
            throw new Error("Method not implemented.");
        }
        onReadonlyChange() {
            throw new Error("Method not implemented.");
        }
        onErrorChange() {
            throw new Error("Method not implemented.");
        }
    }
    exports.default = Label;
});

//# sourceMappingURL=Label.js.map

define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class OrderLayout extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Order-Layout");
            // this._content = UEyeCore.create("div", this.element);
            this.linkComponentContainer("content", this.element);
        }
        set content(value) {
            this.setComponentContainer("content", value);
        }
        get content() {
            return this.getComponentContainer("content");
        }
        set rightToLeft(value) {
            this._rightToLeft = value;
            if (this._rightToLeft) {
                Core_1.default.addClass(this.element, "Right-To-Left");
            }
            else {
                Core_1.default.removeClass(this.element, "Right-To-Left");
            }
        }
        get rightToLeft() {
            return this._rightToLeft;
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
    exports.default = OrderLayout;
});

//# sourceMappingURL=OrderLayout.js.map

define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Column extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Column");
            // this._columnElements = UEyeCore.create("div", this.element, "Column-Elements");
            this.linkComponentContainer("content", this.element);
        }
        set content(value) {
            this.setComponentContainer("content", value);
        }
        get content() {
            return this.getComponentContainer("content");
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
    exports.default = Column;
});

//# sourceMappingURL=Column.js.map

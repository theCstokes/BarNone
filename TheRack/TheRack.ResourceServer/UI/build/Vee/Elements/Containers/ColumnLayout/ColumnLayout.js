define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ColumnLayout extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Column-Layout");
            this.linkComponentContainer("columns", this.element);
        }
        set columns(value) {
            this.setComponentContainer("columns", value);
        }
        get columns() {
            return this.getComponentContainer("columns");
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
    exports.default = ColumnLayout;
});

//# sourceMappingURL=ColumnLayout.js.map

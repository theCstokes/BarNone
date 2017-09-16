define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class PartitionLayout extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Partition-Layout");
            this._leftSide = Core_1.default.create("div", this.element, "Left-Side");
            this.linkComponentContainer("leftSide", this._leftSide);
            this._holder = Core_1.default.create("div", this.element, "Holder");
            this._rightSide = Core_1.default.create("div", this._holder, "Right-Side");
            this.linkScreenContainer("rightSide", this._rightSide);
        }
        set leftSide(value) {
            this.setComponentContainer("leftSide", value);
        }
        get leftSide() {
            return this.getComponentContainer("leftSide");
        }
        set rightSide(value) {
            this.setScreenContainer("rightSide", value);
        }
        get rightSide() {
            return this.getScreenContainer("rightSide");
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
    exports.default = PartitionLayout;
});

//# sourceMappingURL=PartitionLayout.js.map

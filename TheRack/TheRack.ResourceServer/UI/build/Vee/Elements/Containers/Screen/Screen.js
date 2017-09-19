define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Screen extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "Vee-Screen");
            this.linkComponentContainer("content", this.element);
            this._bottomDockElement = Core_1.default.create("div", this.element, "Bottom-Dock");
            this.linkComponentContainer("bottomDock", this._bottomDockElement);
        }
        set content(value) {
            this.setScreenContainer("content", value);
        }
        get content() {
            return this.getScreenContainer("content");
        }
        set dockBottom(value) {
            this.setComponentContainer("dockBottom", value);
        }
        get dockBottom() {
            return this.getComponentContainer("dockBottom");
        }
        destroy() {
            var parentNode = this.element.parentNode;
            if (parentNode !== null) {
                parentNode.removeChild(this.element);
            }
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
    exports.default = Screen;
});

//# sourceMappingURL=Screen.js.map

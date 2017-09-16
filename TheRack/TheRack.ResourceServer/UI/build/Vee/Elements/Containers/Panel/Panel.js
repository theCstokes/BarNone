define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Panel extends BaseContainer_1.BaseContainer {
        // End Region.
        // Public Constructor(s).
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Panel");
            this._topDockElement = Core_1.default.create("div", this.element, "Top-Dock");
            this._captionElement = Core_1.default.create("div", this._topDockElement, "Name");
            this._modeElement = Core_1.default.create("div", this._topDockElement, "Mode");
            this._contentElement = Core_1.default.create("div", this.element, "Content");
            this.linkComponentContainer("content", this._contentElement);
            this._bottomDockElement = Core_1.default.create("div", this.element, "Bottom-Dock");
            this.linkComponentContainer("bottomDock", this._bottomDockElement);
        }
        // End Region
        // Region Public Property(s).
        set caption(value) {
            this._caption = value;
            this._captionElement.textContent = this._caption;
            if (!Utils.isNullOrWhitespace(this._caption)) {
                Core_1.default.addClass(this._topDockElement, "Has-Caption");
            }
            else {
                Core_1.default.removeClass(this._topDockElement, "Has-Caption");
            }
        }
        get caption() {
            return this._caption;
        }
        set content(value) {
            this.setComponentContainer("content", value);
        }
        get content() {
            return this.getComponentContainer("content");
        }
        set dockBottom(value) {
            this.setComponentContainer("dockBottom", value);
        }
        get dockBottom() {
            return this.getComponentContainer("dockBottom");
        }
        // End Region
        // Region Protected Member(s).
        onModify() {
        }
        onReadonly() {
        }
        // End Region
        // Region Private Member(s).
        renderMode(mode, flag) {
            if (mode) {
                Core_1.default.addClass(this._topDockElement, flag);
                this._modeElement.textContent = flag;
                Core_1.default.addClass(this._modeElement, flag);
            }
            else {
                Core_1.default.removeClass(this._topDockElement, flag);
                this._modeElement.textContent = "";
                Core_1.default.removeClass(this._modeElement, flag);
            }
        }
        onModifiedChange() {
            this.renderMode(this.modified, "Modified");
        }
        onReadonlyChange() {
            this.renderMode(this.readonly, "Readonly");
        }
        onErrorChange() {
            throw new Error("Method not implemented.");
        }
    }
    exports.default = Panel;
});

//# sourceMappingURL=Panel.js.map

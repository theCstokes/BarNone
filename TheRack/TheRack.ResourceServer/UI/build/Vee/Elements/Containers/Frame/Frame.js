define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Frame extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Frame");
            this._globalDock = Core_1.default.create("div", this.element, "Global-Dock");
            this.linkComponentContainer("globalDock", this._globalDock);
            this._contextDock = Core_1.default.create("div", this.element, "Context-Dock");
            this.linkComponentContainer("contextDock", this._contextDock);
            this._statusArea = Core_1.default.create("div", this.element, "Status-Area");
            var _statusImageArea = Core_1.default.create("div", this._statusArea, "Status-Image-Area");
            this._statusImageElement = Core_1.default.create("img", _statusImageArea, "Status-Image");
            this._statusTitleElement = Core_1.default.create("div", this._statusArea, "Status-Title");
            var statusImageHoverElement = Core_1.default.create("div", this._statusArea, "Status-Image-Hover");
            this._statusImageButtonElement = Core_1.default.create("div", statusImageHoverElement, "Status-Image-Button");
            this._statusImageButtonElement.textContent = "Test";
            this._navDock = Core_1.default.create("div", this.element, "Nav-Dock");
            this.linkComponentContainer("navDock", this._navDock);
            this._content = Core_1.default.create("div", this.element, "Content");
            this.linkScreenContainer("content", this._content);
        }
        set globalDock(value) {
            this.setComponentContainer("globalDock", value);
        }
        get globalDock() {
            return this.getComponentContainer("globalDock");
        }
        set contextDock(value) {
            this.setComponentContainer("contextDock", value);
        }
        get contextDock() {
            return this.getComponentContainer("contextDock");
        }
        get statusTitle() {
            return this._statusTitle;
        }
        set statusTitle(value) {
            if (this._statusTitle !== value) {
                this._statusTitle = value;
                this._statusTitleElement.textContent = this._statusTitle;
            }
        }
        get statusImageSource() {
            return this._statusImageSource;
        }
        set statusImageSource(value) {
            if (this._statusImageSource !== value) {
                this._statusImageSource = value;
                this._statusImageElement.src = this._statusImageSource;
            }
        }
        set navDock(value) {
            this.setComponentContainer("navDock", value);
        }
        get navDock() {
            return this.getComponentContainer("navDock");
        }
        set content(value) {
            this.setScreenContainer("content", value);
        }
        get content() {
            return this.getScreenContainer("content");
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
    exports.default = Frame;
});

//# sourceMappingURL=Frame.js.map

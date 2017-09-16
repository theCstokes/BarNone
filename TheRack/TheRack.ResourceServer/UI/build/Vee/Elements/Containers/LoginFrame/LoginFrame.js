define(["require", "exports", "Vee/Elements/Core/BaseContainer/BaseContainer", "Vee/Elements/Core/Core"], function (require, exports, BaseContainer_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class LoginFrame extends BaseContainer_1.BaseContainer {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "Vee-Login-Frame");
            this._backgroundImage = Core_1.default.create("img", this.element, "Background");
            this._content = Core_1.default.create("div", this.element, "Content");
            this.linkComponentContainer("content", this._content);
        }
        set content(value) {
            this.setScreenContainer("content", value);
        }
        get content() {
            return this.getScreenContainer("content");
        }
        get background() {
            return this._backgroundImageSource;
        }
        set background(value) {
            if (this._backgroundImageSource !== value) {
                this._backgroundImageSource = value;
                this._backgroundImage.src = this._backgroundImageSource;
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
    exports.default = LoginFrame;
});

//# sourceMappingURL=LoginFrame.js.map

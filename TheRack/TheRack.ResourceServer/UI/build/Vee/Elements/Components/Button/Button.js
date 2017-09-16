define(["require", "exports", "Vee/Elements/Core/BaseComponent/BaseComponent"], function (require, exports, BaseComponent_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Button extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent, "Vee-Button");
            this.element.onclick = this.onClickHandler.bind(this);
        }
        get text() {
            return this._text;
        }
        set text(value) {
            this._text = value;
            this.element.textContent = this._text;
        }
        get onClick() {
            return this._onClickCallback;
        }
        set onClick(value) {
            this._onClickCallback = value;
        }
        onModifiedChange() {
            // Not needed for button.
        }
        onReadonlyChange() {
            // Not needed for button.
        }
        onErrorChange() {
            // Not needed for button.
        }
        onClickHandler() {
            if (this._onClickCallback !== undefined) {
                this._onClickCallback();
            }
        }
    }
    exports.default = Button;
});

//# sourceMappingURL=Button.js.map

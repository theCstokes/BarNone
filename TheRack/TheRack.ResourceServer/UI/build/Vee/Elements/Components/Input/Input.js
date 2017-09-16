define(["require", "exports", "Vee/Elements/Core/Core", "Vee/Elements/Core/BaseComponent/BaseComponent"], function (require, exports, Core_1, BaseComponent_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Input extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Input");
            this._content = Core_1.default.create("div", this.element, "Content");
            this._action = Core_1.default.create("div", this.element, "Action");
            this._hint = Core_1.default.create("div", this._content, "Hint");
            this._input = Core_1.default.create("input", this._content, "Input");
            this._input.oninput = this.onInputHandler.bind(this);
            this._input.onfocus = this.onFocusHandler.bind(this);
            this._input.onblur = this.onBlurHandler.bind(this);
        }
        set hint(value) {
            this._hintText = value;
            this._input.placeholder = this._hintText;
            this._hint.textContent = this._hintText;
            this.updateHint();
        }
        get hint() {
            return this._hintText;
        }
        set text(value) {
            this._text = value;
            this._input.value = value;
            this.updateHint();
        }
        get text() {
            return this._text;
        }
        get onChange() {
            return this._onChangeCallback;
        }
        set onChange(value) {
            this._onChangeCallback = value;
        }
        onModifiedChange() {
            if (this.modified) {
                Core_1.default.addClass(this.element, "Modified");
            }
            else {
                Core_1.default.removeClass(this.element, "Modified");
            }
        }
        onReadonlyChange() {
            if (this.readonly) {
                Core_1.default.addClass(this.element, "Readonly");
                this._input.readOnly = true;
            }
            else {
                Core_1.default.removeClass(this.element, "Readonly");
                this._input.readOnly = false;
            }
        }
        onErrorChange() {
            throw new Error("Method not implemented.");
        }
        // Region Private Member(s).
        updateHint() {
            if (!Utils.isNullOrWhitespace(this._text)) {
                Core_1.default.addClass(this._hint, "Has-And-Text");
            }
            else {
                Core_1.default.removeClass(this._hint, "Has-And-Text");
            }
        }
        onInputHandler() {
            this._text = this._input.value;
            if (this._onChangeCallback !== undefined) {
                this._onChangeCallback(this._text);
            }
            this.updateHint();
        }
        onFocusHandler() {
            Core_1.default.addClass(this.element, "Focused");
        }
        onBlurHandler() {
            Core_1.default.removeClass(this.element, "Focused");
        }
    }
    exports.default = Input;
});

//# sourceMappingURL=Input.js.map

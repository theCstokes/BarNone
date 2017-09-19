define(["require", "exports", "Vee/Elements/Core/BaseComponent/BaseComponent", "Vee/Elements/Core/Core"], function (require, exports, BaseComponent_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseListItem extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-List-Item");
            this.element.onclick = this.onSelectCallback.bind(this);
        }
        get selected() {
            return this._selected;
        }
        set selected(value) {
            this._selected = value;
            if (this._selected) {
                Core_1.default.addClass(this.element, "Selected");
            }
            else {
                Core_1.default.removeClass(this.element, "Selected");
            }
        }
        get onSelect() {
            return this._onSelectCallback;
        }
        set onSelect(value) {
            this._onSelectCallback = value;
        }
        onSelectCallback() {
            this._selected = true;
            if (this._onSelectCallback !== undefined) {
                this._onSelectCallback(this._selected);
            }
        }
    }
    exports.BaseListItem = BaseListItem;
});

//# sourceMappingURL=BaseListItem.js.map

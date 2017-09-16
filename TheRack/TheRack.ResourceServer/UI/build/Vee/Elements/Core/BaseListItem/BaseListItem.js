define(["require", "exports", "Vee/Elements/Core/BaseComponent/BaseComponent", "Vee/Elements/Core/Core"], function (require, exports, BaseComponent_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseListItem extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-List-Item");
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
        get selected() {
            return this._selected;
        }
    }
    exports.BaseListItem = BaseListItem;
});

//# sourceMappingURL=BaseListItem.js.map

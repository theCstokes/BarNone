define(["require", "exports", "Vee/Elements/Core/Core", "Vee/Elements/Core/BaseListItem/BaseListItem"], function (require, exports, Core_1, BaseListItem_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ContactListItem extends BaseListItem_1.BaseListItem {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-Contact-List-Item");
            this._nameElement = Core_1.default.create("div", this.element, "Name");
        }
        set name(value) {
            this._name = value;
            this._nameElement.textContent = this._name;
        }
        get name() {
            return this._name;
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
    exports.default = ContactListItem;
});

//# sourceMappingURL=ContactListItem.js.map

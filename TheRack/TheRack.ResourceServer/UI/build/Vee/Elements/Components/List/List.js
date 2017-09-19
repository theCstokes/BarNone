var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Elements/Core/BaseComponent/BaseComponent", "Vee/Elements/Core/Core", "Vee/Elements/Core/ViewInflater"], function (require, exports, BaseComponent_1, Core_1, ViewInflater_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UEyeList extends BaseComponent_1.BaseComponent {
        constructor(parent) {
            super(parent);
            Core_1.default.addClass(this.element, "UEye-List");
            // this.element.onclick = this.onSelectCallback.bind(this);
            this._elementList = Core_1.default.create("ul", this.element, "Element-List");
        }
        set items(value) {
            this.destroyItems();
            this._items = value;
            this.refreshItems();
        }
        get items() {
            return this._items;
        }
        set style(value) {
            this._style = value;
        }
        get style() {
            return this._style;
        }
        destroyItems() {
            if (this._listElements !== undefined) {
                this._listElements.forEach(listElement => {
                    var parentNode = listElement.parentNode;
                    if (parentNode !== null) {
                        parentNode.removeChild(listElement);
                    }
                });
            }
        }
        refreshItems() {
            this._listElements = [];
            this._items.forEach((element) => __awaiter(this, void 0, void 0, function* () {
                var listElement = Core_1.default.create("li", this._elementList, "Element");
                var instance = yield ViewInflater_1.default
                    .InflateByPath(this._style, listElement, element);
                // if (this._selected !== instance && instance.selected) {
                //     instance.selected = false;
                //     console.warn("Item is already selected");
                // } else 
                if (instance.selected) {
                    this._selected = instance;
                }
                listElement.onclick = (e) => {
                    if (this._selected !== undefined) {
                        this._selected.selected = false;
                    }
                    instance.selected = true;
                    this._selected = instance;
                    this.onSelectCallback();
                };
                // TODO - add events to component.
                this._listElements.push(listElement);
            }));
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
        get selected() {
            return this._selected;
        }
        get onSelect() {
            return this._onSelectCallback;
        }
        set onSelect(value) {
            this._onSelectCallback = value;
        }
        onSelectCallback() {
            if (this._onSelectCallback !== undefined) {
                this._onSelectCallback(this._selected);
            }
        }
    }
    exports.default = UEyeList;
});

//# sourceMappingURL=List.js.map

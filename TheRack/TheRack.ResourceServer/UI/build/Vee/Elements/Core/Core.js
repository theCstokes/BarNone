var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Elements/Core/ViewInflater"], function (require, exports, ViewInflater_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Core {
        static create(type, parent, ...cssClasses) {
            var element = document.createElement(type);
            Core.addClass(element, ...cssClasses);
            parent.appendChild(element);
            return element;
        }
        static addClass(element, ...cssClasses) {
            cssClasses.forEach(name => {
                var items = name.match(/\S+/g) || [];
                items.forEach(function (item) {
                    var itemName = item + " ";
                    var reg = new RegExp(itemName);
                    if (!reg.test(element.className)) {
                        element.className += (itemName);
                    }
                });
            });
        }
        static removeClass(element, ...cssClasses) {
            cssClasses.forEach(name => {
                var items = name.match(/\S+/g) || [];
                items.forEach(function (item) {
                    var itemName = item + " ";
                    var reg = new RegExp(itemName);
                    element.className = element.className.replace(reg, "");
                });
            });
        }
        static replaceClass(element, original, cssClass) {
            Core.removeClass(element, original);
            Core.addClass(element, cssClass);
        }
        static replaceAllClasses(element, original, cssClass) {
            original.forEach(function (styleClass) {
                Core.removeClass(element, styleClass);
            });
            cssClass.forEach(function (styleClass) {
                Core.addClass(element, styleClass);
            });
        }
        static inflate(parent, configList) {
            return __awaiter(this, void 0, void 0, function* () {
                var map = {};
                var mountingPoints = [];
                yield ViewInflater_1.default.inflate(parent, configList, map, mountingPoints);
                return { map: map, mountingPoints: mountingPoints };
            });
        }
    }
    exports.default = Core;
});

//# sourceMappingURL=Core.js.map

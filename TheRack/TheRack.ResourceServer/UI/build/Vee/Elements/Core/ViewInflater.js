var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Elements/Core/Loader"], function (require, exports, Loader_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ViewInflater {
        static inflate(parent, elements, map, mountingPoints) {
            return __awaiter(this, void 0, void 0, function* () {
                var result = [];
                for (var i = 0; i < elements.length; i++) {
                    var element = elements[i];
                    if (element.hasOwnProperty(ViewInflater.INSTANCE_KEY)) {
                        // Create instance.
                        var instanceTypePath = element[ViewInflater.INSTANCE_KEY];
                        var instanceType = yield ViewInflater.createInstance(instanceTypePath);
                        var instance = new instanceType(parent);
                        if (ViewInflater.isContainer(instance)) {
                            mountingPoints.push.apply(mountingPoints, instance.getScreenMountingPoints());
                        }
                        //  Apply and transform properties.
                        for (var key in element) {
                            if (key === ViewInflater.INSTANCE_KEY)
                                continue;
                            var property = element[key];
                            if (!(key in instance))
                                console.warn("No property called " + key);
                            if (ViewInflater.isContainer(instance) && ViewInflater.isObjectDefinition(property)) {
                                // Find the parent for the new elements.
                                var containerElement = instance.getComponentContainerElement(key);
                                if (containerElement !== undefined) {
                                    // Inflate the elements.
                                    instance[key] = yield ViewInflater.inflationChain(containerElement, property, map, mountingPoints);
                                    continue;
                                }
                            }
                            instance[key] = property;
                        }
                        instance.onShow();
                        // Add to map.
                        if (instance.id !== undefined) {
                            map[instance.id] = instance;
                        }
                        // Add to inflation results.
                        result.push(instance);
                    }
                }
                return result;
            });
        }
        static InflateByPath(path, parent, obj) {
            return __awaiter(this, void 0, void 0, function* () {
                var instanceType = yield ViewInflater.createInstance(path);
                var instance = new instanceType(parent);
                for (var key in obj) {
                    var property = obj[key];
                    instance[key] = property;
                }
                instance.onShow();
                return instance;
            });
        }
        static createInstance(path) {
            return __awaiter(this, void 0, void 0, function* () {
                var module = yield Loader_1.default.sync([path]);
                // Return the default class.
                return module.default;
            });
        }
        static isContainer(obj) {
            return ("getComponentContainerElement" in obj && "getScreenMountingPoints" in obj);
        }
        static isObjectDefinition(obj) {
            if (Array.isArray(obj))
                return true;
            if (typeof obj === "object") {
                return obj.hasOwnProperty(ViewInflater.INSTANCE_KEY);
            }
            return false;
        }
        static inflationChain(parent, value, map, mountingPoints) {
            return __awaiter(this, void 0, void 0, function* () {
                if (!Array.isArray(value))
                    value = [value];
                return yield ViewInflater.inflate(parent, value, map, mountingPoints);
            });
        }
    }
    ViewInflater.INSTANCE_KEY = "instance";
    exports.default = ViewInflater;
});

//# sourceMappingURL=ViewInflater.js.map

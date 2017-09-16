define(["require", "exports", "lodash"], function (require, exports, _) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    window.Utils = {};
    Utils.clone = function (obj) {
        return _.cloneDeep(obj);
    };
    Utils.isNullOrUndefined = function (obj) {
        return (obj === null || obj === undefined);
    };
    Utils.isNullOrWhitespace = function (obj) {
        return (obj === null || obj === undefined || obj === "");
    };
    exports.default = {
        Utils: Utils
    };
});

//# sourceMappingURL=Vee.config.d.js.map

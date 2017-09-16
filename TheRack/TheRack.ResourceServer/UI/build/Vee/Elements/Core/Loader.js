var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Loader {
        static async(files, callback) {
            require(files, callback);
        }
        static sync(files) {
            return __awaiter(this, void 0, void 0, function* () {
                return yield new Promise(function (resolve) {
                    Loader.async(files, resolve);
                });
            });
        }
    }
    Loader.fileCache = {};
    exports.default = Loader;
});

//# sourceMappingURL=Loader.js.map

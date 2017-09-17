var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Data/BaseDataManager", "Vee/Data/RequestBuilder"], function (require, exports, BaseDataManager_1, RequestBuilder_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class Resource {
        constructor(route) {
            this._route = route;
        }
        load() {
            return __awaiter(this, void 0, void 0, function* () {
                var result = yield RequestBuilder_1.default
                    .GET(BaseDataManager_1.DEFAULT_URL + this._route)
                    .header("Authorization", "Bearer " + BaseDataManager_1.BaseDataManager.auth.access_token)
                    .execute();
                var data = JSON.parse(result);
                return data;
            });
        }
    }
    exports.default = Resource;
});

//# sourceMappingURL=Resource.js.map

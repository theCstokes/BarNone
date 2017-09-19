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
    exports.AUTH_URL = "http://localhost:61761/api/v1/authorize/login";
    exports.DEFAULT_URL = "http://localhost:63202/api/v1/";
    class BaseDataManager {
        static authorize(username, password) {
            return __awaiter(this, void 0, void 0, function* () {
                var http = new XMLHttpRequest();
                var args = "username=" + username +
                    "&password=" + password +
                    "&grant_type=" + BaseDataManager.grant_type +
                    "&client_id=" + BaseDataManager.client_id;
                http.open("POST", exports.AUTH_URL, true);
                // http.setRequestHeader("Access-Control-Allow-Origin", "*");
                // http.setRequestHeader("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                var result = new Promise((resolve, reject) => {
                    http.onreadystatechange = function () {
                        if (http.readyState == 4 && http.status == 200) {
                            BaseDataManager._auth = JSON.parse(http.responseText);
                            resolve(true);
                        }
                        else if (http.readyState == 4) {
                            resolve(false);
                        }
                    };
                });
                http.send(args);
                return result;
            });
        }
        static get auth() {
            return this._auth;
        }
    }
    BaseDataManager.grant_type = "password";
    BaseDataManager.client_id = "099153c2625149bc8ecb3e85e03f0022";
    exports.BaseDataManager = BaseDataManager;
    class Auth {
    }
    exports.Auth = Auth;
});

//# sourceMappingURL=BaseDataManager.js.map

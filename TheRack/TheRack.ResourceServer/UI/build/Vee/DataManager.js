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
    class DataManager {
        static authorize(username, password) {
            return __awaiter(this, void 0, void 0, function* () {
                var http = new XMLHttpRequest();
                var args = "username=" + username +
                    "&password=" + password +
                    "&grant_type=" + DataManager.grant_type +
                    "&client_id=" + DataManager.client_id;
                http.open("POST", DataManager.authURL, true);
                http.setRequestHeader("Access-Control-Allow-Origin", "*");
                http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
                var result = new Promise((resolve, reject) => {
                    http.onreadystatechange = function () {
                        if (http.readyState == 4 && http.status == 200) {
                            console.log(http.responseText);
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
    }
    DataManager.authURL = "http://localhost:61761/api/v1/authorize/login";
    // private static readonly authURL = "http://localhost:63202/api/v1/authorize/login";
    DataManager.defaultURL = "http://localhost:63202/api/v1/";
    DataManager.grant_type = "password";
    DataManager.client_id = "099153c2625149bc8ecb3e85e03f0022";
    exports.default = DataManager;
});

//# sourceMappingURL=DataManager.js.map

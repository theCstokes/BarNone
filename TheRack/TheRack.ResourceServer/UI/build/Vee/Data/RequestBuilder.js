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
    class RequestBuilder {
        constructor(verb, route) {
            this._verb = verb;
            this._route = route;
            this._headers = {};
        }
        static GET(route) {
            return new RequestBuilder("GET", route);
        }
        header(key, value) {
            this._headers[key] = value;
            return this;
        }
        execute(data = null) {
            return __awaiter(this, void 0, void 0, function* () {
                return new Promise((resolve, reject) => {
                    var xhr = new XMLHttpRequest();
                    xhr.open(this._verb, this._route, true);
                    for (var key in this._headers) {
                        if (!this._headers.hasOwnProperty(key))
                            continue;
                        xhr.setRequestHeader(key, this._headers[key]);
                    }
                    xhr.onload = () => {
                        if (xhr.readyState === 4) {
                            if (xhr.status === 200) {
                                resolve(xhr.responseText);
                            }
                            else {
                                console.warn(xhr.statusText);
                                reject();
                            }
                        }
                    };
                    xhr.onerror = () => {
                        console.warn(xhr.statusText);
                        reject();
                    };
                    xhr.send(null);
                });
            });
        }
    }
    exports.default = RequestBuilder;
});

//# sourceMappingURL=RequestBuilder.js.map

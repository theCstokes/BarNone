var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Vee", "Application/Login/LoginScreen"], function (require, exports, Vee_1, LoginScreen_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class App {
        static start() {
            return __awaiter(this, void 0, void 0, function* () {
                yield Vee_1.default.push(LoginScreen_1.default);
            });
        }
    }
    exports.default = App;
});

//# sourceMappingURL=App.js.map

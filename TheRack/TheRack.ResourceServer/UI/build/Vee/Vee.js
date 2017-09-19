var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
define(["require", "exports", "Vee/Elements/Core/Core"], function (require, exports, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ScreenMountPoint {
    }
    class Vee {
        static start() {
            var base = document.getElementById("app");
            if (base !== null) {
                Vee._base = base;
            }
            Vee._screens = [];
            Vee._screenMounts = [];
        }
        static get base() {
            return this._base;
        }
        static push(AppScreenType, data) {
            return __awaiter(this, void 0, void 0, function* () {
                var screen = new AppScreenType();
                var parent = this._base;
                if (this._screenMounts.length > 0) {
                    var screenMount = this._screenMounts.find(screenMount => {
                        return (screenMount.isUsed === false);
                    });
                    if (screenMount === undefined) {
                        console.warn("Could not find screen mounting point. Defaulting to base.");
                    }
                    else {
                        parent = screenMount.element;
                        screenMount.isUsed = true;
                        screenMount.screen = screen;
                    }
                }
                var results = yield Core_1.default.inflate(parent, screen.screenContent);
                this._screenMounts = this._screenMounts.concat(results.mountingPoints.map(element => {
                    var screenMount = new ScreenMountPoint();
                    screenMount.element = element;
                    screenMount.isUsed = false;
                    // screenMount.screen = screen;
                    return screenMount;
                }));
                for (var key in results.map) {
                    if (!results.map.hasOwnProperty(key))
                        continue;
                    screen.view[key] = results.map[key];
                }
                screen.screenControl = results.map.screenControl;
                Vee._screens.push(screen);
                screen.trigger("onShow", data);
            });
        }
        static pop() {
            var lastScreen = Vee._screens.pop();
            if (lastScreen === undefined)
                return;
            this._screenMounts = this._screenMounts.map((mount) => {
                if (mount.screen === lastScreen) {
                    mount.isUsed = false;
                    mount.screen = undefined;
                    return mount;
                }
                return mount;
            });
            lastScreen.screenControl.destroy();
        }
    }
    exports.default = Vee;
});

//# sourceMappingURL=Vee.js.map

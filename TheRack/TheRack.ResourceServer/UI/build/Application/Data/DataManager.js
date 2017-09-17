define(["require", "exports", "Vee/Data/BaseDataManager", "Vee/Data/Resource"], function (require, exports, BaseDataManager_1, Resource_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class DataManager extends BaseDataManager_1.BaseDataManager {
    }
    DataManager.Users = new Resource_1.default("User");
    exports.default = DataManager;
});

//# sourceMappingURL=DataManager.js.map

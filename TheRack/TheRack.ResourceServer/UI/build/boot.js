"use strict";
require.config({
    baseUrl: "build",
    paths: {
        data: "../data",
        lodash: '../node_modules/lodash/lodash'
    },
});
define([
    "Vee/Vee",
    "Vee/Vee.config.d",
    "Application/App",
    "PlayGround/App"
], function (Vee, Vee_config_d, App, PlayGround) {
    // Flags.
    var launchPlayGround = false;
    // Start.
    Vee.default.start();
    if (launchPlayGround) {
        PlayGround.default.start();
    }
    else {
        App.default.start();
    }
});

//# sourceMappingURL=boot.js.map

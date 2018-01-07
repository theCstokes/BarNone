require.config({
	baseUrl: "build/release",
	paths: {
		data: "../../data",
		lodash: '../../node_modules/lodash/lodash'
	},
	// urlArgs: "v=" + (new Date()).getTime()
});

define([
	"UEye/UEye",
	"UEye/UEye.config.d",
	"Application/App",
	"PlayGround/App"
], function (UEye, UEye_config_d, App, PlayGround) {

	// Flags.
	var launchPlayGround = true;

	// Start.
	UEye.default.start();
	if (launchPlayGround) {
		PlayGround.default.start();
	} else {
		App.default.start();
	}
});
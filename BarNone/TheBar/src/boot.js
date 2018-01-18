require.config({
	baseUrl: "build/release",
	paths: {
		data: "../../data",
		lodash: '../../node_modules/lodash/lodash',
		chartjs: '../../libs/Chart'
	},
	// urlArgs: "v=" + (new Date()).getTime()
});

define([
	"UEye/UEye",
	"UEye/UEye.config.d",
	"App/App",
	"App/App", // TODO - Play

	// Preload modules.
	"chartjs"
], function (UEye, UEye_config_d, App, PlayGround) {
	
	// Flags.
	var launchPlayGround = false;

	// Start.
	UEye.default.start();
	if (launchPlayGround) {
		PlayGround.default.start();
	} else {
		App.default.start();
	}
});
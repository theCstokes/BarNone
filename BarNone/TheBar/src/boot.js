requirejs.config({
	baseUrl: "build/release",
	paths: {
		data: "../../data",
		lodash: '../../node_modules/lodash/lodash',
		chartjs: '../../libs/Chart',
		momentjs: '../../libs/moment'
	},
	// urlArgs: "v=" + (new Date()).getTime()
});

// TODO - Debug.
var $App = null;
var $UEye = null;

define([
	"UEye/UEye",
	"UEye/UEye.config.d",
	"App/App",
	"App/App", // TODO - Play

	// Preload modules.
	"chartjs",
	"momentjs"
], function (UEye, UEye_config_d, App, PlayGround) {
	// TODO - Debug.
	$App = App.default;
	$UEye = UEye.default;
	
	// Flags.
	var launchPlayGround = false;

	// Start.
	if (launchPlayGround) {
		new PlayGround.default();
	} else {
		new App.default();
	}
});
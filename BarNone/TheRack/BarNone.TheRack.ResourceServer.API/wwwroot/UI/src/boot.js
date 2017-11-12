require.config({
	baseUrl: "build/release",
	paths: {
		data: "../../data",
		lodash: '../../node_modules/lodash/lodash'
	},
	// urlArgs: "v=" + (new Date()).getTime()
});

define([
	"Vee/Vee",
	"Vee/Vee.config.d",
	"Application/App",
	"PlayGround/App"
], function (Vee, Vee_config_d, App, PlayGround) {

	// Flags.
	var launchPlayGround = true;

	// Start.
	Vee.default.start();
	if (launchPlayGround) {
		PlayGround.default.start();
	} else {
		App.default.start();
	}
});
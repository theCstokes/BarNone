require.config({
	baseUrl: "build",
	paths: {
		data: "../data",
		lodash: '../node_modules/lodash/lodash'
	},
	urlArgs: "v=" + (new Date()).getTime()
});

define(["Vee/Vee", "Vee/Vee.config.d", "Application/App"], function(Vee, Vee_config_d, App) {

	Vee.default.start();
	App.default.start();


	// UEye.default.app = document.getElementById("app")
	// UEye.default.start();
	// App.default.main();
});
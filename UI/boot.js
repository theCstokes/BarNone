require.config({
	baseUrl: "build",
	paths: {
		data: "../data"
	},
	urlArgs: "v=" + (new Date()).getTime()
});

define(["UEye/UEye", "Application/App"], function(UEye, App) {

	if (!Array.prototype.combine) {
		Array.prototype.combine = function(element) {
			if(Array.isArray(element)) {
				for (var i = 0; i < element.length; i++) {
					this[this.length] = element[i];
				}
			} else {
				this[this.length] = element;
			}
			return this.length;
		};
	}


	UEye.default.app = document.getElementById("app")
	UEye.default.start();
	App.default.main();
});
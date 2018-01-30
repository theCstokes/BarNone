import * as _ from 'lodash';

window.Utils = {};
Utils.clone = function (obj) {
	return _.cloneDeep(obj);
}
Utils.isNullOrUndefined = function (obj) {
	return (obj === null || obj === undefined);
}
Utils.isNullOrWhitespace = function (obj) {
	return (obj === null || obj === undefined || obj === "");
}
Utils.guid = function () {
	function s4() {
		return Math.floor((1 + Math.random()) * 0x10000)
			.toString(16)
			.substring(1);
	}
	return s4() + s4() +
		"-" + s4() +
		"-" + s4() +
		"-" + s4() +
		"-" + s4() + s4() + s4();
}
export default {
	Utils: Utils
}
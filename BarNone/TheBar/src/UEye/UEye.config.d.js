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
Utils.compare = function (obj1, obj2, ignore) {
	if (ignore === undefined) ignore = [];
	// _.differenceWith(source, target, _.isEqual);
	const diff = Object.keys(obj1)
		.filter(key => ignore.indexOf(key) === -1)
		.reduce((result, key) => {
			if (!obj2.hasOwnProperty(key) && !(key in obj2)) {
				result.push(key);
			} else if (_.isEqual(obj1[key], obj2[key])) {
				const resultKeyIndex = result.indexOf(key);
				result.splice(resultKeyIndex, 1);
			}
			return result;
		},
		Object.keys(obj2)
			.filter(key => ignore.indexOf(key) === -1));

	return diff.length === 0;
}
export default {
	Utils: Utils
}
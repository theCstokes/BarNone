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
Utils.equivalent = function (obj1, obj2, ignore) {

	obj1 = Utils.clone(obj1);
	obj2 = Utils.clone(obj2);

	if (ignore !== undefined) {
		ignore.forEach(key => delete obj1[key]);
		ignore.forEach(key => delete obj2[key]);
	}

	return (JSON.stringify(obj1) === JSON.stringify(obj2));

	// let keys1 = [];
	// let keys2 = [];
	// if (ignore === undefined) ignore = [];

	// if (obj1 === undefined && obj2 === undefined) return true;

	// if (obj1 !== undefined) {
	// 	keys1 = Object.keys(obj1).filter(key => ignore.indexOf(key) === -1);
	// } else {
	// 	obj1 = {};
	// }
	// if (obj2 !== undefined) {
	// 	keys2 = Object.keys(obj2).filter(key => ignore.indexOf(key) === -1);
	// } else {
	// 	obj2 = {};
	// }

	// if(keys1.length === 0 && keys2.length === 0) return obj1 === obj2;

	// const diff = keys1
	// 	.reduce((result, key) => {
	// 		if (!obj2.hasOwnProperty(key) && !(key in obj2)) {
	// 			result.push(key);
	// 		} else if (_.isEqual(obj1[key], obj2[key])) {
	// 			const resultKeyIndex = result.indexOf(key);
	// 			result.splice(resultKeyIndex, 1);
	// 		}
	// 		return result;
	// 	}, keys2);

	// return diff.length === 0;
}

if (!Array.prototype.first) {
  Object.defineProperty(Array.prototype, 'first', {
    value: function(predicate) {
     // 1. Let O be ? ToObject(this value).
      if (this == null) {
        throw new TypeError('"this" is null or not defined');
      }

      var o = Object(this);

      // 2. Let len be ? ToLength(? Get(O, "length")).
			var len = o.length >>> 0;
			
			// No elements return undefined.
			if (len === 0) return undefined;

			// No predicate return first element.
			if (predicate === undefined) return o[0];

      // 3. If IsCallable(predicate) is false, throw a TypeError exception.
      if (typeof predicate !== 'function') {
        throw new TypeError('predicate must be a function');
			}
			
			var filterO = o.find(predicate);

			var len = o.length >>> 0;
			
			// No elements return undefined.
			if (len === 0) return undefined;

			return o[0];
    },
    configurable: true,
    writable: true
  });
}

export default {
	Utils: Utils
}
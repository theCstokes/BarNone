/**
 * String helper functions.
 */
export default class StringUtils {
	/**
	 * Inserts arguments into string with index values.
	 * {0} is used to reference the first argument.
	 * @param data - string format
	 * @param args - args to insert
	 */
	public static format(data: string, ...args: any[]) : string {
		return args.reduce((result, item, idx) => {
			var key = "{" + idx + "}";
			return result.replace(key, item);
		}, data);
	}

	public static replace(data: string, params?: { [key: string]: string|number }) : string {
		if (params === undefined) return data;

		for(var name in params) {
			var key = "{" + name + "}";
			return data.replace(key, String(params[name]));
		}
		return data;
	}
}
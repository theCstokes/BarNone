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
}
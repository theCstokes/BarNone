export default class StringUtils {
	public static format(data: string, ...args: any[]) : string {
		return args.reduce((result, item, idx) => {
			var key = "{" + idx + "}";
			return result.replace(key, item);
		}, data);
	}
}
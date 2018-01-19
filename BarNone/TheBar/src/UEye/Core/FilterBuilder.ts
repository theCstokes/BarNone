/**
 * Object filter property resolver.
 * Used to resolve the property to compare to.
 */
export type FilterPropertyFunc<T> = (t: T) => any;

/**
 * Filter comparisons.
 */
export type FilterComparisons = "eq";

/**
 * Filter request object.
 */
export class Filter<T> {
	/**
	 * Filter property resolver.
	 */
	public property: FilterPropertyFunc<T>;

	/**
	 * Comparison
	 */
	public comparisons?: FilterComparisons;

	/**
	 * Value to compare to.
	 */
	public value: any;
}

/**
 * Where filter object for header filter request.
 */
export interface IWhereFilter {
	/**
	 * Filter property name.
	 */
	property: string;

	/**
	 * Where comparison.
	 */
	op: FilterComparisons;

	/**
	 * Target value.
	 */
	value: any;
}

/**
 * Filter header object.
 */
export interface IFilterHeader {
	/**
	 * Filter search.
	 */
	where: IWhereFilter[];
}

/**
 * Filter header builder.
 * Converts filter objects to the header objects.
 */
export class FilterBuilder {
	/**
	 * Property name regex.
	 */
	private static MEMBER_NAME_REG_EXP = /(?:\.)([a-zA-Z0-9\-_]+)/g;

	/**
	 * Converts the filter object to a filter header.
	 * @param f - filter object.
	 */
	public static getHeader<T>(f: Filter<T>): IFilterHeader {
		var accessFuncString = f.property.toString();
		var path = [];
		var match;
		while (match = FilterBuilder.MEMBER_NAME_REG_EXP.exec(accessFuncString)) {
			path.push(match[1]);
		}

		var propertyPath = path.reduce((result, p, idx) => {
			if (idx === 0) return p;
			return (result + "." + p);
		}, "")

		return {
			where: [
				{
					property: propertyPath,
					op: (f.comparisons == undefined ? "eq" : f.comparisons),
					value: f.value
				}
			]
		}
	}
}
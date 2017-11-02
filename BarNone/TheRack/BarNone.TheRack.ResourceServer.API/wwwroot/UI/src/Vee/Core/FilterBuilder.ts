
export type FilterPropertyFunc<T> = (t: T) => any;

export type FilterComparisons = "eq";

export class Filter<T> {
	public property: FilterPropertyFunc<T>;
	public comparisons?: FilterComparisons;
	public value: any;
}

export interface IWhereFilter {
	property: string;
	op: FilterComparisons;
	value: any;
}

export interface IFilterHeader {
	where: IWhereFilter[];
}

export class FilterBuilder {
	private static MEMBER_NAME_REG_EXP = /(?:\.)([a-zA-Z0-9\-_]+)/g;

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
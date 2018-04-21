/**
 * Global Utils definition
 */
export interface IUtils {
	/**
	 * Clone object.
	 */
	clone<T>(t: T): T;

	/**
	 * Is null or undefined
	 */
	isNullOrUndefined: (obj: any | null | undefined) => boolean;

	/**
	 * Is string null or whitespace.
	 */
	isNullOrWhitespace: (obj: any | null | undefined) => boolean;

	guid(): string;

	equivalent(source: any, target: any, ignore?: string[]): boolean;
}


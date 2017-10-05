export interface IUtils {
	clone<T>(t: T): T;
	isNullOrUndefined: (obj: any | null | undefined) => boolean;
	isNullOrWhitespace: (obj: any | null | undefined) => boolean;
}


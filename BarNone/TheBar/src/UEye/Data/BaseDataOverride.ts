/**
 * Provides api override with local data.
 */
export abstract class BaseDataOverride<TData> {
	/**
	 * Data set for api override.
	 */
	public abstract get data(): TData[];

	/**
	 * API response.
	 */
	public get response(): string {
		var entities = {
			entities: this.data
		};
		return JSON.stringify(entities);
	}

	public get listResult(): any {
		return {
			entities: this.data
		};
	}
}
export abstract class BaseDataOverride<TData> {
	public abstract get data(): TData[];

	public get response(): string {
		var entities = {
			entities: this.data
		};
		return JSON.stringify(entities);
	}
}
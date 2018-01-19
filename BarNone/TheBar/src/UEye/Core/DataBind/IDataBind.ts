/**
 * Exposing interface for data binds.
 */
export interface IDataBind {
	/**
	 * Trigger event with data object.
	 */
	trigger(data?: any): void;
}
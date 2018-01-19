/**
 * Public event handler interface.
 */
export interface IDataEvent<T> {
	/**
	 * Add event handler.
	 */
	on(handler: { (data?: T): void }): void;

	/**
	 * Remove event handler.
	 */
	off(handler: { (data?: T): void }): void;
}
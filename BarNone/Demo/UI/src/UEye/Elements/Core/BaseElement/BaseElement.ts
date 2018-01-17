import Core from "UEye/Elements/Core/Core";

export abstract class BaseElement {
	private _element: HTMLElement;
	private _id: any;
	private _instance: any;
	private _modified: boolean;
	private _readonly: boolean;
	private _error: string;

	public constructor(parent: HTMLElement, ...styles: string[]) {
		this._element = Core.create('div', parent, ...styles);
	}

	public get element(): HTMLElement {
		return this._element;
	}

	public get id(): any {
		return this._id;
	}
	public set id(value: any) {
		this._id = value;
	}

	public get instance(): any {
		return this._instance;
	}
	public set instance(value: any) {
		this._instance = value;
	}

	public get modified(): boolean {
		return this._modified;
	}
	public set modified(value: boolean) {
		this._modified = value;
		this.onModifiedChange();
	}

	public get readonly(): boolean {
		return this._readonly;
	}
	public set readonly(value: boolean) {
		this._readonly = value;
		this.onReadonlyChange();
	}

	public get error(): string {
		return this._error;
	}
	public set error(value: string) {
		this._error = value;
		this.onErrorChange();
	}

	public destroy(): void {
		var parentNode = this.element.parentNode;
		if (parentNode !== null) {
			parentNode.removeChild(this.element);
		}
	}

	public onModifiedChange(): void {
		throw("No onModifiedChange implemented for component.")
	}

	public onReadonlyChange(): void  {
		throw("No onModifiedChange implemented for component.")
	}

	public onErrorChange(): void {
		throw("No onModifiedChange implemented for component.")
	}
}
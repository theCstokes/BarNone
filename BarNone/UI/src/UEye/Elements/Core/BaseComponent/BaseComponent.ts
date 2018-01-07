import { BaseElement } from "UEye/Elements/Core/BaseElement/BaseElement";

export abstract class BaseComponent extends BaseElement {

	private _enabled: boolean;

	public constructor(parent: HTMLElement, ... styles: string[]) {
		super(parent, ...styles);
	}

	public get enabled(): boolean {
		return this._enabled;
	}
	public set enabled(value: boolean) {
		if (value !== this._enabled) {
			this._enabled = value;
			this.onEnabledChange();
		}
	}

	public abstract onEnabledChange(): void;
	
}
import { IUtils } from 'Vee/Vee.config';

declare global {
	const Utils: IUtils
}

export default class Vee {
	public static _base: HTMLElement;

	public static start(): void {
		var base = document.getElementById("app");
		if (base !== null) {
			Vee._base = base;
		}
	}

	public static get base(): HTMLElement {
		return this._base;
	}

	public static get Button(): string {
		return "Vee/Elements/Components/Button/Button";
	}
}
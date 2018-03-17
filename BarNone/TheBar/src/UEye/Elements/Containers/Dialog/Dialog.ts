import { BaseContainer } from "UEye/Elements/Core/BaseContainer/BaseContainer";
import Core from "UEye/Elements/Core/Core";

export enum EDialogSize {
	Small = "Small",
	Normal = "Normal"
}

export default class Dialog extends BaseContainer {
	//#region Private DOM Element(s).
	private e_content: HTMLElement;
	private _size: EDialogSize;
	//#endregion

	public constructor(parent: HTMLElement) {
		super(parent, "UEye-Dialog");

		this.e_content = Core.create("div", this.element, "Content");
		this.linkComponentContainer("content", this.e_content);
	}

	public get content(): any[] {
		return this.getScreenContainer("content");
	}
	public set content(value: any[]) {
		this.setScreenContainer("content", value);
	}

	public get size(): EDialogSize {
		return this._size;
	}
	public set size(value: EDialogSize) {
		if (this._size !== value) {
			Core.replaceClass(this.e_content, this.size, value);
			this._size = value;
		}
	}
}
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import { DialogView } from "UEye/View/DialogView";

export default class DialogScreen<TView extends DialogView> extends Screen<TView> {
	
	public constructor(ViewType: { new(): TView }) {
		super(ViewType);
	}

	public configure(): IScreenConfig {
		return {
			addScreenToHistory: true,
			fullScreen: true
		}
	}

	public onShow(): void {
		for(var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "blur(5px)";
		}
	}

	public destroy(): void {
		for(var i = 0; i < this.screenObj.parent.children.length; i++) {
			var item = this.screenObj.parent.children[i] as HTMLElement;
			if (item === this.screenObj.element) continue;
			item.style.webkitFilter = "";
		}
		super.destroy();
	}
}
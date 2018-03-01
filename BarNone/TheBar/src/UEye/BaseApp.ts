import UEye from "UEye/UEye";
import Toast from "UEye/Elements/Components/Toast/Toast";
import StringUtils from "UEye/Core/StringUtils";

export abstract class BaseApp {
	// private _base: HTMLElement;
	
	public static _toast: Toast;

	public constructor(name: string) {
		var base = document.getElementById(name);
		if (base !== null) {
			UEye.start(base);
			BaseApp._toast = new Toast(base);

			this.onStartup();
		} else {
			console.warn(StringUtils.format("No element name {0}", name));
		}
	}

	public static get Toast(): Toast {
		return BaseApp._toast;
	}
	
	protected abstract onStartup(): void;
}
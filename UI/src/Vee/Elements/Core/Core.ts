import ViewInflater from "Vee/Elements/Core/ViewInflater";

export default class Core {
	public static create(type: string, parent: HTMLElement, ...cssClasses: string[]): HTMLElement | HTMLInputElement {
		var element = document.createElement(type);
		Core.addClass(element, ...cssClasses);
		parent.appendChild(element);
		return element;
	}

	public static addClass(element: HTMLElement, ...cssClasses: string[]): void {
		cssClasses.forEach(name => {
			var items = name.match(/\S+/g) || [];
			items.forEach(function (item) {
				var itemName = item + " ";
				var reg = new RegExp(itemName);
				if (!reg.test(element.className)) {
					element.className += (itemName);
				}
			});
		});
	}

	public static removeClass(element: HTMLElement, ...cssClasses: string[]): void {
		cssClasses.forEach(name => {
			var items = name.match(/\S+/g) || [];
			items.forEach(function (item) {
				var itemName = item + " ";
				var reg = new RegExp(itemName);
				element.className = element.className.replace(reg, "");
			});
		});
	}

	public static replaceClass(element: HTMLElement, original: string, cssClass: string): void {
		Core.removeClass(element, original);
		Core.addClass(element, cssClass);
	}

	public static replaceAllClasses(element: HTMLElement, original: string[], cssClass: string[]): void {
		original.forEach(function (styleClass) {
			Core.removeClass(element, styleClass);
		});

		cssClass.forEach(function (styleClass) {
			Core.addClass(element, styleClass);
		});
	}

	public static async inflate(parent: HTMLElement, configList: any[]): Promise<{ map: { [id:string]: any }, mountingPoints: HTMLElement[] }> {
        var map: { [id:string]: any } = {};
        var mountingPoints: HTMLElement[] = [];
        await ViewInflater.inflate(parent, configList, map, mountingPoints);
        return { map: map, mountingPoints: mountingPoints };
    }
}
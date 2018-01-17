export default class Core {
	public static create(type: string, parent: HTMLElement, ...cssClasses: string[]): HTMLElement | HTMLInputElement {
		var element = document.createElement(type);
		Core.addClass(element, ...cssClasses);
		parent.appendChild(element);
		return element;
	}

	public static addClass(element: HTMLElement, ...cssClasses: string[]): void {
		cssClasses.forEach(name => {
			if (name === undefined) return;
			var items = name.match(/\S+/g) || [];
			items.forEach(function (item) {
				var itemName = " " + item + " ";
				var reg = new RegExp(itemName);
				if (!reg.test(element.className)) {
					element.className += (itemName);
				}
			});
		});
	}

	public static removeClass(element: HTMLElement, ...cssClasses: string[]): void {
		cssClasses.forEach(name => {
			if (name === undefined) return;
			var items = name.match(/\S+/g) || [];
			items.forEach(function (item) {
				var itemName = " " + item + " ";
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
}
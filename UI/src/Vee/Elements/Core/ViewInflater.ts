import Loader from "Vee/Elements/Core/Loader";

type InflationChain = (parent: HTMLElement, value: any, obj: any, map: { [id: string]: any },
	mountingPoints: HTMLElement[]) => any;

export default class ViewInflater {
	private static readonly INSTANCE_KEY = "instance";
	
	public static async inflate(parent: HTMLElement, elements: any[], map: { [id: string]: any },
		mountingPoints: HTMLElement[]): Promise<any[]> {
		var result = [];
		for (var i = 0; i < elements.length; i++) {
			var element = elements[i];
			if (element.hasOwnProperty(ViewInflater.INSTANCE_KEY)) {
				// Create instance.
				var instanceTypePath = element[ViewInflater.INSTANCE_KEY];
				var instanceType = await ViewInflater.createInstance(instanceTypePath);
				var instance = new instanceType(parent);
				if (ViewInflater.isContainer(instance)) {
					mountingPoints.push.apply(mountingPoints, instance.getScreenMountingPoints());
				}

				//  Apply and transform properties.
				for (var key in element) {
					if (key === ViewInflater.INSTANCE_KEY) continue;

					var property = element[key];

					if (!(key in instance)) console.warn("No property called " + key);

					if (ViewInflater.isContainer(instance) && ViewInflater.isObjectDefinition(property)) {
						// Find the parent for the new elements.
						var containerElement = instance.getComponentContainerElement(key);
						if (containerElement !== undefined) {
							// Inflate the elements.
							instance[key] = await ViewInflater.inflationChain(containerElement, property, map,
								mountingPoints);
							continue;
						}
					}

					instance[key] = property;
				}
				instance.onShow();
				// Add to map.
				if (instance.id !== undefined) {
					map[instance.id] = instance;
				}
				// Add to inflation results.
				result.push(instance);
			}
		}
		return result;
	}

	public static async InflateByPath(path: string, parent: HTMLElement, obj: any): Promise<any> {
		var instanceType = await ViewInflater.createInstance(path);
		var instance = new instanceType(parent);
		for (var key in obj) {
			var property = obj[key];

			instance[key] = property;
		}
		instance.onShow();
		return instance;
	}

	private static async createInstance(path: string): Promise<{ new(parent: HTMLElement): any }> {
		var module = await Loader.sync([path]);
		// Return the default class.
		return module.default;
	}

	private static isContainer(obj: any): boolean {
		return ("getComponentContainerElement" in obj && "getScreenMountingPoints" in obj);
	}

	private static isObjectDefinition(obj: any): boolean {
		if (Array.isArray(obj)) return true;
		if (typeof obj === "object") {
			return obj.hasOwnProperty(ViewInflater.INSTANCE_KEY);
		}
		return false;
	}

	private static async inflationChain(parent: HTMLElement, value: any, map: { [id: string]: any },
		mountingPoints: HTMLElement[]): Promise<any[]> {
		if (!Array.isArray(value)) value = [value];
		return await ViewInflater.inflate(parent, value, map, mountingPoints);
	}
}
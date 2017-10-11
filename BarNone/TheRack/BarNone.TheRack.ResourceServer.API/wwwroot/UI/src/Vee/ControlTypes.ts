export default class ControlTypes {
	private static componentRegistry: string[] = [];
	private static containerRegistry: string[] = [];

	// Components.
	public static readonly Button: string = ControlTypes.getComponentPath("Button");
	public static readonly ContactListItem: string = ControlTypes.getComponentPath("ContactListItem");
	public static readonly NavigationListItem: string = ControlTypes.getComponentPath("NavigationListItem");
	public static readonly Input: string = ControlTypes.getComponentPath("Input");
	public static readonly Label: string = ControlTypes.getComponentPath("Label");
	public static readonly List: string = ControlTypes.getComponentPath("List");

	// Containers.
	public static readonly Column: string = ControlTypes.getContainerPath("Column");
	public static readonly ColumnLayout: string = ControlTypes.getContainerPath("ColumnLayout");
	public static readonly Frame: string = ControlTypes.getContainerPath("Frame");
	public static readonly LoginFrame: string = ControlTypes.getContainerPath("LoginFrame");
	public static readonly OrderLayout: string = ControlTypes.getContainerPath("OrderLayout");
	public static readonly Panel: string = ControlTypes.getContainerPath("Panel");
	public static readonly PartitionLayout: string = ControlTypes.getContainerPath("PartitionLayout");
	public static readonly Screen: string = ControlTypes.getContainerPath("Screen");

	public static isComponent(path: string): boolean {
		return (ControlTypes.componentRegistry.indexOf(path) > -1);
	}

	public static isContainer(path: string): boolean {
		return (ControlTypes.containerRegistry.indexOf(path) > -1);
	}

	private static getComponentPath(name: string): string {
		var path = "Vee/Elements/Components/" + name + "/" + name;
		ControlTypes.componentRegistry.push(path);
		return path;
	}

	private static getContainerPath(name: string): string {
		var path = "Vee/Elements/Containers/" + name + "/" + name;
		ControlTypes.containerRegistry.push(path);
		return path;
	}
}
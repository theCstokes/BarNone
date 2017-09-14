class ControlTypeHelper {
	public static getComponentPath(name: string): string {
		return "Vee/Elements/Components/" + name + "/" + name;
	}

	public static getContainerPath(name: string): string {
		return "Vee/Elements/Containers/" + name + "/" + name;
	}
}

export default class ControlTypes {

	// Components.
	public static readonly Button: string = ControlTypeHelper.getComponentPath("Button");
	public static readonly ContactListItem: string = ControlTypeHelper.getComponentPath("ContactListItem");
	public static readonly Input: string = ControlTypeHelper.getComponentPath("Input");
	public static readonly Label: string = ControlTypeHelper.getComponentPath("Label");
	public static readonly List: string = ControlTypeHelper.getComponentPath("List");

	// Containers.
	public static readonly Column: string = ControlTypeHelper.getContainerPath("Column");
	public static readonly ColumnLayout: string = ControlTypeHelper.getContainerPath("ColumnLayout");
	public static readonly Frame: string = ControlTypeHelper.getContainerPath("Frame");
	public static readonly OrderLayout: string = ControlTypeHelper.getContainerPath("OrderLayout");
	public static readonly Panel: string = ControlTypeHelper.getContainerPath("Panel");
	public static readonly PartitionLayout: string = ControlTypeHelper.getContainerPath("PartitionLayout");
}
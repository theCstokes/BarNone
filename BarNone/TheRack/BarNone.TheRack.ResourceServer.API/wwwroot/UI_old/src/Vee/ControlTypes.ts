import ComponentType from "Vee/ComponentType";
import ContainerType from "Vee/ContainerType";

import Button from "Vee/Elements/Components/Button/Button";
import ContactListItem from "Vee/Elements/Components/ContactListItem/ContactListItem";
import NavigationListItem from "Vee/Elements/Components/NavigationListItem/NavigationListItem";
import Input from "Vee/Elements/Components/Input/Input";
import PasswordInput from "Vee/Elements/Components/PasswordInput/PasswordInput";
import Label from "Vee/Elements/Components/Label/Label";
import List from "Vee/Elements/Components/List/List";
import Breadcrumb from "Vee/Elements/Components/Breadcrumb/Breadcrumb";
import Video from "Vee/Elements/Components/Video/Video";
import Range from "Vee/Elements/Components/Range/Range";

import Column from "Vee/Elements/Containers/Column/Column";
import ColumnLayout from "Vee/Elements/Containers/ColumnLayout/ColumnLayout";
import Frame from "Vee/Elements/Containers/Frame/Frame";
import LoginFrame from "Vee/Elements/Containers/LoginFrame/LoginFrame";
import OrderLayout from "Vee/Elements/Containers/OrderLayout/OrderLayout";
import Panel from "Vee/Elements/Containers/Panel/Panel";
import PartitionLayout from "Vee/Elements/Containers/PartitionLayout/PartitionLayout";
import Screen from "Vee/Elements/Containers/Screen/Screen";

export default class ControlTypes {

	public static readonly Button = new ComponentType(p => new Button(p));
	public static readonly ContactListItem = new ComponentType(p => new Button(p));
	public static readonly NavigationListItem = new ComponentType(p => new Button(p));
	public static readonly Input = new ComponentType(p => new Button(p));
	public static readonly PasswordInput = new ComponentType(p => new Button(p));
	public static readonly Label = new ComponentType(p => new Button(p));
	public static readonly List = new ComponentType(p => new Button(p));
	public static readonly Breadcrumb = new ComponentType(p => new Button(p));
	public static readonly Video = new ComponentType(p => new Button(p));
	public static readonly Range = new ComponentType(p => new Range(p));


	public static readonly Column = new ContainerType(p => new Column(p));
	public static readonly ColumnLayout = new ContainerType(p => new ColumnLayout(p));
	public static readonly Frame = new ContainerType(p => new Frame(p));
	public static readonly LoginFrame = new ContainerType(p => new LoginFrame(p));
	public static readonly OrderLayout = new ContainerType(p => new OrderLayout(p));
	public static readonly Panel = new ContainerType(p => new Panel(p));
	public static readonly PartitionLayout = new ContainerType(p => new PartitionLayout(p));
	public static readonly Screen = new ContainerType(p => new Screen(p));
	

	// private static componentRegistry: string[] = [];
	// private static containerRegistry: string[] = [];

	// // Components.
	// public static readonly Button: string = ControlTypes.getComponentPath("Button");
	// public static readonly ContactListItem: string = ControlTypes.getComponentPath("ContactListItem");
	// public static readonly NavigationListItem: string = ControlTypes.getComponentPath("NavigationListItem");
	// public static readonly Input: string = ControlTypes.getComponentPath("Input");
	// public static readonly PasswordInput: string = ControlTypes.getComponentPath("PasswordInput");
	// public static readonly Label: string = ControlTypes.getComponentPath("Label");
	// public static readonly List: string = ControlTypes.getComponentPath("List");
	// public static readonly Breadcrumb: string = ControlTypes.getComponentPath("Breadcrumb");
	// public static readonly Video: string = ControlTypes.getComponentPath("Video");
	// public static readonly Range: string = ControlTypes.getComponentPath("Range");

	// // Containers.
	// public static readonly Column: string = ControlTypes.getContainerPath("Column");
	// public static readonly ColumnLayout: string = ControlTypes.getContainerPath("ColumnLayout");
	// public static readonly Frame: string = ControlTypes.getContainerPath("Frame");
	// public static readonly LoginFrame: string = ControlTypes.getContainerPath("LoginFrame");
	// public static readonly OrderLayout: string = ControlTypes.getContainerPath("OrderLayout");
	// public static readonly Panel: string = ControlTypes.getContainerPath("Panel");
	// public static readonly PartitionLayout: string = ControlTypes.getContainerPath("PartitionLayout");
	// public static readonly Screen: string = ControlTypes.getContainerPath("Screen");

	// public static isComponent(path: string): boolean {
	// 	return (ControlTypes.componentRegistry.indexOf(path) > -1);
	// }

	// public static isContainer(path: string): boolean {
	// 	return (ControlTypes.containerRegistry.indexOf(path) > -1);
	// }

	// private static getComponentPath(name: string): string {
	// 	var path = "Vee/Elements/Components/" + name + "/" + name;
	// 	ControlTypes.componentRegistry.push(path);
	// 	return path;
	// }

	// private static getContainerPath(name: string): string {
	// 	var path = "Vee/Elements/Containers/" + name + "/" + name;
	// 	ControlTypes.containerRegistry.push(path);
	// 	return path;
	// }
}
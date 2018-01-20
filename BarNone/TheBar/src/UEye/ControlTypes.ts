import ComponentType from "UEye/Elements/Inflater/ComponentInflater";
import ContainerType from "UEye/Elements/Inflater/ContainerInflater";

import Button from "UEye/Elements/Components/Button/Button";
import ContactListItem from "UEye/Elements/Components/ContactListItem/ContactListItem";
import NavigationListItem from "UEye/Elements/Components/NavigationListItem/NavigationListItem";
import Input from "UEye/Elements/Components/Input/Input";
import PasswordInput from "UEye/Elements/Components/PasswordInput/PasswordInput";
import Label from "UEye/Elements/Components/Label/Label";
import List from "UEye/Elements/Components/List/List";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
import Video from "UEye/Elements/Components/Video/Video";
// import Stream from "UEye/Elements/Components/Stream/Stream";
import Range from "UEye/Elements/Components/Range/Range";
import Graph from "UEye/Elements/Components/Graph/Graph";

import Column from "UEye/Elements/Containers/Column/Column";
import ColumnLayout from "UEye/Elements/Containers/ColumnLayout/ColumnLayout";
import Frame from "UEye/Elements/Containers/Frame/Frame";
import LoginFrame from "UEye/Elements/Containers/LoginFrame/LoginFrame";
import OrderLayout from "UEye/Elements/Containers/OrderLayout/OrderLayout";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import PartitionLayout from "UEye/Elements/Containers/PartitionLayout/PartitionLayout";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
/**
 *  Control Types Class binds the Components and Containers designed to read only properties. These properties are used to declare object types in screens for render and state management.
 */
export default class ControlTypes {
	/**  Represents Button Component
	 * @param p HTMLElement
	 * @returns Button
	*/
	public static readonly Button = new ComponentType(p => new Button(p));
	/**  Represents ContactListItem Component
	 * @param p HTMLElement
	 * @returns ContactListItem
	*/
	public static readonly ContactListItem = new ComponentType(p => new ContactListItem(p));
	/**  Represents NavigationListItem Component
	 * @param p HTMLElement
	 * @returns NavigationListItem
	*/
	public static readonly NavigationListItem = new ComponentType(p => new NavigationListItem(p));
	/**  Represents Input Component
	 * @param p HTMLElement
	 * @returns Input
	*/
	public static readonly Input = new ComponentType(p => new Input(p));
	/**  Represents PasswordInput Component
	 * @param p HTMLElement
	 * @returns PasswordInput
	*/
	public static readonly PasswordInput = new ComponentType(p => new PasswordInput(p));
	/**  Represents Label Component
	 * @param p HTMLElement
	 * @returns Label
	*/
	public static readonly Label = new ComponentType(p => new Label(p));
	/**  Represents List Component
	 * @param p HTMLElement
	 * @returns List 
	*/
	public static readonly List = new ComponentType(p => new List(p));
	/**  Represents Breadcrumb Component
	 * @param p HTMLElement
	 * @returns Breadcrumb
	*/
	public static readonly Breadcrumb = new ComponentType(p => new Breadcrumb(p));
	/**  Represents Video Component\
	 * @param p HTMLElement
	 * @returns Video
	*/
	public static readonly Video = new ComponentType(p => new Video(p));
	// public static readonly Stream = new ComponentType(p => new Stream(p));
	/**  Represents Range Component
	 * @param p HTMLElement
	 * @returns Range
	*/
	public static readonly Range = new ComponentType(p => new Range(p));
	/**  Represents Graph Component
	 * @param p HTMLElement
	 * @returns Graph
	*/
	public static readonly Graph = new ComponentType(p => new Graph(p));

	/**  Represents Column Container
	 * @param p HTMLElement
	 * @returns Column
	*/
	public static readonly Column = new ContainerType(p => new Column(p));
		/**  Represents ColumnLayout Container
		 *  @param p HTMLElement
	 * @returns ColumnLayout
		*/
	public static readonly ColumnLayout = new ContainerType(p => new ColumnLayout(p));
		/**  Represents Frame Container
		 *  @param p HTMLElement
	 	*  @returns Frame
		*/
	public static readonly Frame = new ContainerType(p => new Frame(p));
		/**  Represents LoginFrame Container
		 *  @param p HTMLElement
	 * @returns LoginFrame
		*/
	public static readonly LoginFrame = new ContainerType(p => new LoginFrame(p));
		/**  Represents OrderLayout Container
		 *  @param p HTMLElement
	 * @returns OrderLayout
		*/
	public static readonly OrderLayout = new ContainerType(p => new OrderLayout(p));
		/**  Represents Panel Container
		 *  @param p HTMLElement
	 * @returns Panel
		*/
	public static readonly Panel = new ContainerType(p => new Panel(p));
		/**  Represents PartitionLayout Container'
		 *  @param p HTMLElement
	 * @returns PartitionLayout
		*/
	public static readonly PartitionLayout = new ContainerType(p => new PartitionLayout(p));
		/**  Represents ContentContainer Container
		 *  @param p HTMLElement
	 * @returns ContentContainer
		*/
	public static readonly ContentContainer = new ContainerType(p => new ContentContainer(p));
	

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
	// 	var path = "UEye/Elements/Components/" + name + "/" + name;
	// 	ControlTypes.componentRegistry.push(path);
	// 	return path;
	// }

	// private static getContainerPath(name: string): string {
	// 	var path = "UEye/Elements/Containers/" + name + "/" + name;
	// 	ControlTypes.containerRegistry.push(path);
	// 	return path;
	// }
}
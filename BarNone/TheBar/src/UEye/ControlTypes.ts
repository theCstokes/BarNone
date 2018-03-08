import ComponentType from "UEye/Elements/Inflater/ComponentInflater";
import ContainerType from "UEye/Elements/Inflater/ContainerInflater";

import Button from "UEye/Elements/Components/Button/Button";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import ContactListItem from "UEye/Elements/Components/ContactListItem/ContactListItem";
import LiftFolderListItem from "UEye/Elements/Components/LiftFolderListItem/LiftFolderListItem";
import AnalysisListItem from "UEye/Elements/Components/AnalysisListItem/AnalysisListItem";
import DataListItem from "UEye/Elements/Components/DataListItem/DataListItem";
import NavigationListItem from "UEye/Elements/Components/NavigationListItem/NavigationListItem";
import Input from "UEye/Elements/Components/Input/Input";
import PasswordInput from "UEye/Elements/Components/PasswordInput/PasswordInput";
import Label from "UEye/Elements/Components/Label/Label";
import List from "UEye/Elements/Components/List/List";
import Breadcrumb from "UEye/Elements/Components/Breadcrumb/Breadcrumb";
import Video from "UEye/Elements/Components/Video/Video";
import Range from "UEye/Elements/Components/Range/Range";
import Graph from "UEye/Elements/Components/Graph/Graph";
import Info from "UEye/Elements/Components/Info/Info";
import Toast from "UEye/Elements/Components/Toast/Toast";
import HTMLContent from "UEye/Elements/Components/HTMLContent/HTMLContent";
import Messenger from "UEye/Elements/Components/Messenger/Messenger";
import MessageListItem from "UEye/Elements/Components/MessageListItem/MessageListItem";
import Checkbox from "UEye/Elements/Components/Checkbox/Checkbox";
import SearchTag from "UEye/Elements/Components/SearchTag/SearchTag";
import SearchBar from "UEye/Elements/Components/SearchBar/SearchBar";

import Column from "UEye/Elements/Containers/Column/Column";
import ColumnLayout from "UEye/Elements/Containers/ColumnLayout/ColumnLayout";
import Frame from "UEye/Elements/Containers/Frame/Frame";
import LoginFrame from "UEye/Elements/Containers/LoginFrame/LoginFrame";
import OrderLayout from "UEye/Elements/Containers/OrderLayout/OrderLayout";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import PartitionLayout from "UEye/Elements/Containers/PartitionLayout/PartitionLayout";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
import TabLayout from "UEye/Elements/Containers/TabLayout/TabLayout";
import Tab from "UEye/Elements/Containers/Tab/Tab";
import Dialog from "UEye/Elements/Containers/Dialog/Dialog";
import SideBarLayout from "UEye/Elements/Containers/SideBarLayout/SideBarLayout";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import DropDownListItem from "UEye/Elements/Components/DropDownListItem/DropDownListItem";

export default class ControlTypes {

	//---------------------Components--------------------

	/**Button Component Type */
	public static readonly Button = new ComponentType(p => new Button(p));
	/**Button Component Type */
	public static readonly IconButton = new ComponentType(p => new IconButton(p));
	/**Contact List Item Component Type */
	public static readonly ContactListItem = new ComponentType(p => new ContactListItem(p));
	/**Lift Folder List Item Component Type */
	public static readonly LiftFolderListItem = new ComponentType(p => new LiftFolderListItem(p));
	/**Lift Folder List Item Component Type */
	public static readonly AnalysisListItem = new ComponentType(p => new AnalysisListItem(p));
	/**Lift Folder List Item Component Type */
	public static readonly DataListItem = new ComponentType(p => new DataListItem(p));
	/**Drop Down List Item Component Type */
	public static readonly DropDownListItem = new ComponentType(p => new DropDownListItem(p));
	/**Lift Folder List Item Component Type */
	public static readonly MessageListItem = new ComponentType(p => new MessageListItem(p));
	/**Navigation List Item Component Type */
	public static readonly NavigationListItem = new ComponentType(p => new NavigationListItem(p));
	/**Input Component Type */
	public static readonly Input = new ComponentType(p => new Input(p));
	/**DropDownInput Component Type */
	public static readonly DropDownInput = new ComponentType(p => new DropDownInput(p));
	/**Password Input Component Type */
	public static readonly PasswordInput = new ComponentType(p => new PasswordInput(p));
	/**Label Component Type */
	public static readonly Label = new ComponentType(p => new Label(p));
	/**List Component Type */
	public static readonly List = new ComponentType(p => new List(p));
	/**Breadcrumb Component Type */
	public static readonly Breadcrumb = new ComponentType(p => new Breadcrumb(p));
	/**Video Component Type */
	public static readonly Video = new ComponentType(p => new Video(p));
	/**Range Component Type */
	public static readonly Range = new ComponentType(p => new Range(p));
	/**Graph Component Type */
	public static readonly Graph = new ComponentType(p => new Graph(p));
	/**Info Component Type */
	public static readonly Info = new ComponentType(p => new Info(p));
	/**Toast Component Type */
	public static readonly Toast = new ComponentType(p => new Toast(p));
	/**Toast Component Type */
	public static readonly HTMLContent = new ComponentType(p => new HTMLContent(p));
	/**Messenger Component Type */
	public static readonly Messenger = new ComponentType(p => new Messenger(p));
	/**Checkbox Component Type */
	public static readonly Checkbox = new ComponentType(p => new Checkbox(p));
	/**SearchTag Component Type */
	public static readonly SearchTag = new ComponentType(p => new SearchTag(p));
	/**SearchBar Component Type */
	public static readonly SearchBar = new ComponentType(p => new SearchBar(p));
	//---------------------Containers--------------------

	/**Column Container Type */
	public static readonly Column = new ContainerType(p => new Column(p));
	/**Column Layout Container Type */
	public static readonly ColumnLayout = new ContainerType(p => new ColumnLayout(p));
	/**Frame Container Type */
	public static readonly Frame = new ContainerType(p => new Frame(p));
	/**Login Frame Container Type */
	public static readonly LoginFrame = new ContainerType(p => new LoginFrame(p));
	/**Order Layout Container Type */
	public static readonly OrderLayout = new ContainerType(p => new OrderLayout(p));
	/**Panel Container Type */
	public static readonly Panel = new ContainerType(p => new Panel(p));
	/**Partition Layout Container Type */
	public static readonly PartitionLayout = new ContainerType(p => new PartitionLayout(p));
	/**Content Container Container Type */
	public static readonly ContentContainer = new ContainerType(p => new ContentContainer(p));
	/**Tab Layout Container Type */
	public static readonly TabLayout = new ContainerType(p => new TabLayout(p));
	/**Tab Container Type */
	public static readonly Tab = new ContainerType(p => new Tab(p));
	/**Dialog Container Type */
	public static readonly Dialog = new ContainerType(p => new Dialog(p));
	/**Dialog Container Type */
	public static readonly SideBarLayout = new ContainerType(p => new SideBarLayout(p));
}
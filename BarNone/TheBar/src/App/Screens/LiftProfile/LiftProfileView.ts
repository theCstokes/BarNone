import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Panel from "UEye/Elements/Containers/Panel/Panel";
import List from "UEye/Elements/Components/List/List";
import { SelectionListView } from "UEye/View/SelectionListView";
import ComponentType from "UEye/Elements/Inflater/ComponentInflater";

export default class LiftProfileView extends SelectionListView {
	public caption: string = "Lift Profiles";
	public listType: ComponentType = ControlTypes.LiftFolderListItem;
	public errorMessage: string = "No available Lift Profiles.";
}
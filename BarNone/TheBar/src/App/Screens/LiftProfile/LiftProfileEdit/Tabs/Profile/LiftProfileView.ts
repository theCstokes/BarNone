import ControlTypes from "UEye/ControlTypes";
import { IList } from "UEye/Elements/Components/List/List";
import IconButton from "UEye/Elements/Components/IconButton/IconButton";
import AnalysisListItem from "UEye/Elements/Components/AnalysisListItem/AnalysisListItem";
import Tab from "UEye/Elements/Containers/Tab/Tab";
import Info from "UEye/Elements/Components/Info/Info";

export interface ILiftProfileView {
	addButton: IconButton;
	criteriaList: IList<AnalysisListItem>;
	criteriaTab: Tab;
	criteriaListInfo: Info;
}

export class LiftProfileTab {
	public static get content(): any {
		return {
			id: "criteriaTab",
			title: "Criteria",
			actions: [
				{
					id: "addButton",
					text: "New",
					icon: "fa-plus"
				}
			],
			content: [
				{
					instance: ControlTypes.List,
					id: "criteriaList",
					isSelectionList: true,
					style: ControlTypes.AnalysisListItem
				},
				{
					instance: ControlTypes.Info,
					id: "criteriaListInfo",
					title: "No Criteria",
					message: "Press the + button to add criteria."
				}
			]
		};
	}
}
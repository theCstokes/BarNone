import Screen from "UEye/Screen/Screen"
import LiftView from "App/Screens/Lifts/LiftView";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import UEye from "UEye/UEye";
import EditScreen from "UEye/Screen/EditScreen";
import { LiftListType, LiftListItem } from "App/Screens/Lifts/Models";
import LiftFolderEditScreen from "App/Screens/Lifts/LiftFolderEdit/LiftFolderEditScreen";
import LiftEditScreen from "App/Screens/Lifts/LiftEdit/LiftEditScreen";
import App from "App/App";
import DataEvent from "UEye/Core/DataEvent/DataEvent";
import Lift from "App/Data/Models/Lift/Lift";
import { LiftFolderHelp } from "App/Help/Lifts/LiftFolderEdit/helpDemo";
import NotificationManager from "UEye/NotificationManager";
import { SelectionListScreen } from "UEye/Screen/SelectionListScreen";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { BaseLiftStateManager, ELiftType, State } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";
import { LiftStateManager } from "App/Screens/Lifts/StateManagers/LiftStateManager";
import { SharedLiftStateManager } from "App/Screens/Lifts/StateManagers/SharedLiftStateManager";

export default class LiftScreen
	extends SelectionListScreen<LiftView, BaseLiftStateManager, LiftListItem, State> {

	private _type: ELiftType;

	public static ParentChange: DataEvent<LiftListItem>;
	public static LiftChange: DataEvent<Lift>;

	public constructor() {
		super(LiftView);

		LiftScreen.ParentChange = new DataEvent();
		LiftScreen.ParentChange.on(this._onFolderOpenHandler.bind(this));

		LiftScreen.LiftChange = new DataEvent();
		LiftScreen.LiftChange.on(this._onListOpenHandler.bind(this));
	}

	public onRenderEditScreen(data: LiftListItem): EditScreen<any, any> | undefined {
		if (data.type === LiftListType.Lift) {
			return UEye.push(LiftEditScreen, {
				id: data.id,
				name: data.name,
				type: this._type
			}) as LiftEditScreen;
		} else if (data.type === LiftListType.Folder) {
			return UEye.push(LiftFolderEditScreen, {
				id: data.id,
				name: data.name,
				type: this._type
			}) as LiftFolderEditScreen;
		}
		return undefined;
	}
	public listTransform(item: LiftListItem): IListItem {
		return {
			selected: (item.id === this.stateManager.getCurrentState().selectionId),
			id: item.id,
			name: item.name,
			icon: (item.type === LiftListType.Folder) ? "fa-folder" : "fa-universal-access",
			onOpen: () => {
				console.log(item);
				if (item.type === LiftListType.Folder) {
					this._onFolderOpenHandler(item);
				}
			}
		}
	}

	public async onShow(type: ELiftType): Promise<void> {
		super.onShow();
		this._type = type;
		if (type === ELiftType.Lift) {
			this.init(await StateManagerFactory.create(LiftStateManager));
		} else if (type === ELiftType.Shared) {
			this.init(await StateManagerFactory.create(SharedLiftStateManager));	
		}
		
		this.stateManager.ResetState.trigger();
	}

	private _onFolderOpenHandler(item: LiftListItem) {
		App.Navigation.AddSubBreadcrumb.trigger({
			id: Utils.guid(),
			value: item.name,
			onClick: (crumb) => {
				this.stateManager.ParentChange.trigger({ parentID: item.id });
				App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
			}
		});

		this.stateManager.ParentChange.trigger({ parentID: item.id });
	}

	private _onListOpenHandler(item: Lift) {
		if (item.details.parent !== undefined) {
			App.Navigation.AddSubBreadcrumb.trigger({
				id: Utils.guid(),
				value: item.details.parent.name,
				onClick: (crumb) => {
					this.stateManager.ParentChange.trigger({ parentID: item.id });
					App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
				}
			});
		}

		this.stateManager.ParentChange.trigger({
			parentID: item.parentID,
			selectionId: item.id
		});
	}
}
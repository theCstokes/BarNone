import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Lifts/LiftFolderEdit/StateManager";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";
import LiftFolderEditView from "App/Screens/Lifts/LiftFolderEdit/LiftFolderEditView";
import LiftScreen from "App/Screens/Lifts/LiftScreen";
import { LiftListType } from "App/Screens/Lifts/Models";
import { LiftFolderHelp } from "App/Help/Lifts/LiftFolderEdit/helpDemo";
import StateManagerFactory from "UEye/StateManager/StateManagerFactory";
import { ELiftType } from "App/Screens/Lifts/StateManagers/BaseLiftStateManager";

export default class LiftFolderEditScreen extends EditScreen<LiftFolderEditView, StateManager> {
	public constructor() {
		super(LiftFolderEditView, LiftFolderHelp);
	}

	private _onRender(current: State, original: State) {
		console.log(current);
		this.view.nameInput.text = current.name;
		this.view.nameInput.modified = (original.name !== current.name);

		this.view.liftList.items = current.folder.details.subFolders
			.map(item => {
				return {
					// selected: (item.id === current.selectionId),
					id: item.id,
					name: item.name,
					icon: "fa-folder",
					onOpen: () => {
						// alert("open");
						LiftScreen.ParentChange.trigger({
							id: item.id,
							name: item.name,
							parentID: item.parentID,
							type: LiftListType.Folder
						});
					}
				}
			}).concat(
			current.folder.details.lifts
				.map(item => {
					return {
						// selected: (item.id === current.selectionId),
						id: item.id,
						name: item.name,
						icon: "fa-universal-access",
						onOpen: () => {
							// alert("open");
							LiftScreen.LiftChange.trigger(item);
						}
					}
				})
			);

		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
		this.view.editPanel.modified = isModified;
	}

	public async onShow(data: { id: number, name: string, type: ELiftType }): Promise<void> {
		super.onShow(data);	
		this.init(await StateManagerFactory.create(StateManager));
		this.stateManager.bind(this._onRender.bind(this));

		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};
		
		this.view.tab.onClick= () => {
		
		}
		
		await this.stateManager.ResetState.trigger({ id: data.id, name: data.name });
		// this.view.player.play();
	}

	// public nameBind = ScreenBind
	// 	.create<State>(this, "nameInput")
	// 	.onChange(data => {
	// 		this.stateManager.nameChange.trigger(data);
	// 	})
	// 	.onRender((original, current) => {
	// 		this.view.nameInput.text = current.name;
	// 		this.view.nameInput.modified = (original.name !== current.name);
	// 	});

	// public panelBind = ScreenBind
	// 	.create<State>(this, "editPanel")
	// 	.onRender((original, current) => {
	// 		var isModified = (JSON.stringify(original) !== JSON.stringify(current));
	// 		this.view.editPanel.modified = isModified;
	// 		// this.isDirty = isModified;
	// 	});

	// public onShow(data: any): void {
	// 	console.log(data);
	// 	// this.stateManager.resetState.trigger(data);
	// }

	public save(): void {

	}
}
// import LiftEditView from "App/Screens/Lifts/Edit/LiftEditView";
import EditScreen from "UEye/Screen/EditScreen";
import { StateManager, State } from "App/Screens/Lift2/LiftFolderEdit/StateManager";
import { SkeletonBuilder } from "App/Screens/Lifts/Edit/SkeletonBuilder";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import StringUtils from "UEye/Core/StringUtils";
import LiftFolderEditView from "App/Screens/Lift2/LiftFolderEdit/LiftFolderEditView";
import LiftScreen from "App/Screens/Lift2/LiftScreen";
import { LiftListType } from "App/Screens/Lift2/Models";

// import EditScreen from "Application/Core/EditScreen";
// import ScreenBind from "UEye/Screen/ScreenBind";
// import LiftEditView from "Application/Screens/Lifts/Edit/LiftEditView";
// import { StateManager, State } from "Application/Screens/Lifts/Edit/StateManager";

export default class LiftFolderEditScreen extends EditScreen<LiftFolderEditView, StateManager> {
	public constructor() {
		super(LiftFolderEditView, StateManager);
		this.stateManager.bind(this._onRender.bind(this));
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
					icon: "fa-folder-o",
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

	public onShow(): void {
		this.view.nameInput.onChange = (data) => {
			this.stateManager.NameChange.trigger(data);
		};
		this.view.tab.onClick= () =>{
		
		}
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
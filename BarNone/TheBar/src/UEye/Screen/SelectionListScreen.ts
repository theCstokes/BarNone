import { SelectionListView } from "UEye/View/SelectionListView";
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";

export abstract class SelectionListScreen<TView extends SelectionListView> extends Screen<TView> {

	public constructor(ViewType: { new(): TView }, ) {
		super(ViewType);
	}

	public abstract listTransform<TListItem extends IListItem>(): (item: TListItem) => any;

	// private _basePipeLine = ScreenPipeLine.create()
	// 	//#region Panel
	// 	.onShow(() => {
	// 		this.view.cancelButton.onClick = this._onCancelHandler.bind(this);
	// 	})
	// 	.onRender((current: any, original: any) => {
	// 		var isModified = (JSON.stringify(original) !== JSON.stringify(current));

	// 		this.view.cancelButton.enabled = isModified;
	// 		this.view.saveButton.enabled = isModified;
	// 	})

	// private _onRender(current: State, original: State): void {
	// 	this.view.selectionList.items = current.selectionList.map(item => {
	// 		return {
	// 			selected: (item.id === current.selectionId),
	// 			id: item.id,
	// 			name: item.name,
	// 			icon: (item.type === LiftListType.Folder) ? "fa-folder" : "fa-universal-access",
	// 			onOpen: () => {
	// 				console.log(item);
	// 				if (item.type === LiftListType.Folder) {
	// 					this._onFolderOpenHandler(item);
	// 				}
	// 			}
	// 		}
	// 	});
	// 	this.view.liftListInfo.visible = (current.selectionList.length < 0);

	// 	var userData = current.selectionList.find(item => {
	// 		return (item.id === current.selectionId);
	// 	});

	// 	if (userData !== undefined) {
	// 		if (this.subScreen !== undefined) {
	// 			UEye.popTo(this);
	// 		}
	// 		// this.view.mainPanel.content=this.
	// 		if (userData.type === LiftListType.Lift) {
	// 			this.subScreen = UEye.push(LiftEditScreen, {
	// 				id: userData.id,
	// 				name: userData.name,
	// 				type: this._type
	// 			}) as LiftEditScreen;
	// 		} else if (userData.type === LiftListType.Folder) {
	// 			this.subScreen = UEye.push(LiftFolderEditScreen, {
	// 				id: userData.id,
	// 				name: userData.name,
	// 				type: this._type
	// 			}) as LiftFolderEditScreen;
	// 		}

	// 		// this.subScreen.stateManager.ResetState.trigger({
	// 		// 	id: userData.id,
	// 		// 	name: userData.name,
	// 		// 	age: 0
	// 		// });
	// 	}
	// }

	// public onShow(type: ELiftType): void {
	// 	this._type = type;
	// 	this._stateManager = new StateManager(type);
	// 	this._stateManager.bind(this._onRender.bind(this));

	// 	this.view.liftList.onSelect = (data: IListItem) => {
	// 		this._stateManager.SelectionChange.trigger({ id: data.id });
	// 	};

	// 	// this.subScreen = UEye.push(LiftEditScreen) as LiftEditScreen;
	// 	this._stateManager.init();
	// }

	// private _onFolderOpenHandler(item: LiftListItem) {
	// 	App.Navigation.AddSubBreadcrumb.trigger({
	// 		id: Utils.guid(),
	// 		value: item.name,
	// 		onClick: (crumb) => {
	// 			this._stateManager.ParentChange.trigger({ parentID: item.id });
	// 			App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
	// 		}
	// 	});

	// 	this._stateManager.ParentChange.trigger({ parentID: item.id });
	// }

	// private _onListOpenHandler(item: Lift) {
	// 	if (item.details.parent !== undefined) {
	// 		App.Navigation.AddSubBreadcrumb.trigger({
	// 			id: Utils.guid(),
	// 			value: item.details.parent.name,
	// 			onClick: (crumb) => {
	// 				this._stateManager.ParentChange.trigger({ parentID: item.id });
	// 				App.Navigation.PopSubBreadcrumbTo.trigger(crumb);
	// 			}
	// 		});
	// 	}

	// 	this._stateManager.ParentChange.trigger({
	// 		parentID: item.parentID,
	// 		selectionId: item.id
	// 	});
	// }
}
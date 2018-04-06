import { SelectionListView } from "UEye/View/SelectionListView";
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { SelectionStateManager, ISelectionState } from "UEye/StateManager/SelectionStateManager";
import UEye from "UEye/UEye";
import EditScreen from "UEye/Screen/EditScreen";
import CancelDialogScreen from "UEye/Screen/CancelDialog/CancelDialogScreen";

export abstract class SelectionListScreen<
	TView extends SelectionListView,
	TStateManager extends SelectionStateManager<TListItem, TState>,
	TListItem extends IListItem,
	TState extends ISelectionState<TListItem>
	> extends Screen<TView> {

	protected subScreen: EditScreen<any, any> | undefined

	protected stateManager: TStateManager;

	private _popOnSelection: boolean;

	public constructor(ViewType: { new(): TView }, popOnSelection: boolean = true) {
		super(ViewType);
		this._popOnSelection = popOnSelection;
	}

	private _basePipeLine = ScreenPipeLine.create()
		.onShow(() => {
			this.view.selectionList.onSelect = this._onSelectionHandler.bind(this);
		})
		.onRender((current: TState, original: TState) => {
			this.view.selectionList.items = current.selectionList.reduce((result, x) => {
				let item = this.listTransform.bind(this)(x);
				if (item !== undefined) {
					result.push(item);
				}
				return result;
			}, new Array<IListItem>());

			this.view.selectionListInfo.visible = (current.selectionList.length < 0);

			var userData = current.selectionList.find(item => {
				return (item.id === current.selectionId);
			});

			if (userData !== undefined) {
				if (this._popOnSelection && this.subScreen !== undefined) {
					UEye.popTo(this);
				}

				var sub = this.onRenderEditScreen(userData);
				if (sub !== undefined) {
					this.subScreen = sub
					this.subScreen.onSave.register(() => {
						let selected = this.view.selectionList.selected;
						if (selected === undefined) return;

						this.stateManager.SelectionChange.trigger({
							id: selected.id
						});
					})
				}
			}
		});

	public abstract onRenderEditScreen(data: TListItem): EditScreen<any, any> | undefined;

	public abstract listTransform(item: TListItem): IListItem | undefined;

	public init(stateManager: TStateManager) {
		this.stateManager = stateManager;
		this.stateManager.bind(this._basePipeLine.onRenderInvokable.bind(this));
	}

	public onShow(data?: any): void {
		this._basePipeLine.onShowInvokable();
	}

	private async _onSelectionHandler(data: TListItem) {
		if (this.subScreen === undefined) return;

		var current = this.subScreen.stateManager.getCurrentState();
		var original = this.subScreen.stateManager.getOriginalState();
		if (JSON.stringify(current) === JSON.stringify(original)) {
			this.stateManager.SelectionChange.trigger({ id: data.id });
		} else {
			var dialog = UEye.push(CancelDialogScreen) as CancelDialogScreen;

			dialog.onAccept = async () => {
				this.stateManager.SelectionChange.trigger({ id: data.id });
			}
		}
	}
}
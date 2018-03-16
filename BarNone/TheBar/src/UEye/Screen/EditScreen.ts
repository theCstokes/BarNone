import { EditView } from "UEye/View/EditView";
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import UEye from "UEye/UEye";
import CancelDialogScreen from "UEye/Screen/CancelDialog/CancelDialogScreen";

/**
 * Edit base screen.
 */
export default abstract class EditScreen<
    TView extends EditView,
    TStateManager extends BaseStateManager<any>> extends Screen<TView> {
    /**
     * Edit screen state manager.
     */
    private _stateManager: TStateManager;

    /**
     * Initialize edit screen.
     * @param ViewType - view builder
     * @param StateManagerType - state manager builder
     */
    public constructor(ViewType: { new(): TView },
        StateManagerType: { new(): TStateManager } | null = null) {
        super(ViewType);
        if (StateManagerType !== null) {
            this._stateManager = new StateManagerType();
            this._stateManager.bind(this._basePipeLine.onRenderInvokable.bind(this));
        }
    }

    public init(stateManager: TStateManager) {
        this._stateManager = stateManager;
        this._stateManager.bind(this._basePipeLine.onRenderInvokable.bind(this));
    }

    private _basePipeLine = ScreenPipeLine.create()
        //#region Panel
        .onShow(() => {
            this.view.cancelButton.onClick = this._onCancelHandler.bind(this);
        })
        .onRender((current: any, original: any) => {
            var isModified = (JSON.stringify(original) !== JSON.stringify(current));

            this.view.cancelButton.enabled = isModified;
            this.view.saveButton.enabled = isModified;
        })
    //#endregion

    /**
     * Get screen state manager.
     */
    public get stateManager(): TStateManager {
        return this._stateManager;
    }

    private async _onCancelHandler() {
        var current = this.stateManager.getCurrentState();
        var original = this.stateManager.getOriginalState();
        if (JSON.stringify(current) === JSON.stringify(original)) {
            await this.stateManager.Reset.trigger();
        } else {
            var dialog = UEye.push(CancelDialogScreen) as CancelDialogScreen;

            dialog.onNo = () => {
                UEye.pop();
            }   
            dialog.onYes = async () => {
                await this.stateManager.Reset.trigger();
                UEye.pop();
            }
        }
    }

    /**
     * Render state for default edit screen components.
     * @param current - current state
     * @param original - original state
     */
    // private _onBaseRender(current: any, original: any) {
    //     var isModified = (JSON.stringify(original) !== JSON.stringify(current));

    //     this.view.cancelButton.enabled = isModified;
    //     this.view.saveButton.enabled = isModified;
    // }

    public configure(): IScreenConfig {
        return {
            addScreenToHistory: false,
            fullScreen: false
        }
    }

    /**
     * Screen on show.
     */
    public onShow(data?: any): void {
        this._basePipeLine.onShowInvokable();
    }

    // public cancelBind = ScreenBind
    //     .create<any>(this, "cancelButton")
    //     .onRender((original, current) => {
    //         var isModified = (JSON.stringify(original) !== JSON.stringify(current));
    //         this.view.cancelButton.enabled = isModified;
    //         // this.isDirty = isModified;
    //     })
    //     .onClick(() => {
    //         // this.stateManager.init();
    //         this._cancelEvent.trigger();
    //     });

    // public saveBind = ScreenBind
    //     .create<any>(this, "saveButton")
    //     .onRender((original, current) => {
    //         var isModified = (JSON.stringify(original) !== JSON.stringify(current));
    //         this.view.saveButton.enabled = isModified;
    //         // this.isDirty = isModified;
    //     })
    //     .onClick(() => {
    //         this.stateManager.save();
    //     });
}
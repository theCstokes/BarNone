import { EditView } from "UEye/View/EditView";
import Screen, { IScreenConfig } from "UEye/Screen/Screen";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";
import ScreenPipeLine from "UEye/Screen/ScreenPipeLineStage";
import UEye from "UEye/UEye";
import CancelDialogScreen from "UEye/Screen/CancelDialog/CancelDialogScreen";
import { IHelp } from "App/Help/HelpCore";
import { ScreenSection } from "App/Screens/Lifts/LiftEdit/Tabs/IScreenSection";
import CallableEvent, { ICallableEvent } from "UEye/CallableEvent/CallableEvent";

/**
 * Edit base screen.
 */
export default abstract class EditScreen<
    TView extends EditView,
    TStateManager extends BaseStateManager<any>
> extends Screen<TView> {
    /**
     * Edit screen state manager.
     */
    private _stateManager: TStateManager;
    
    private _onSave: CallableEvent;

    /**
     * Initialize edit screen.
     * @param ViewType - view builder
     * @param StateManagerType - state manager builder
     */
    public constructor(ViewType: { new(): TView }, HelpType?: { new(): IHelp }) {
        // StateManagerType: { new(): TStateManager } | null = null) {
        super(ViewType, HelpType);
        this._onSave = new CallableEvent();
        // if (StateManagerType !== null) {
        //     this._stateManager = new StateManagerType();
        //     this._stateManager.bind(this._basePipeLine.onRenderInvokable.bind(this));
        // }
    }

    public init(stateManager: TStateManager) {
        this._stateManager = stateManager;
        this._stateManager.bind(this._basePipeLine.onRenderInvokable.bind(this));
    }

    public isModified(current: any, original: any): boolean {
        return JSON.stringify(original) !== JSON.stringify(current);
    }

    private _basePipeLine = ScreenPipeLine.create()
        //#region Panel
        .onShow(() => {
            this.view.saveButton.onClick = async() => {
                await this.stateManager.save();
                this._onSave.trigger();
            };
            this.view.cancelButton.onClick = this._onCancelHandler.bind(this);
        })
        .onRender((current: any, original: any) => {
            var isModified = this.isModified(current, original);

            this.view.editPanel.modified = isModified;
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
 
            dialog.onAccept = async () => {
                await this.stateManager.Reset.trigger();
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

    private _sections: { new(view: TView, stateManager: TStateManager): ScreenSection<TView, TStateManager> }[] = [];
    public bindSections(...sections: { new(view: TView, stateManager: TStateManager): ScreenSection<TView, TStateManager> }[]) {
        this._sections.push(...sections);
    }

    /**
     * Screen on show.
     */
    public onShow(data?: any): void {
        this._sections.forEach(SectionType => {
            let s = new SectionType(this.view, this.stateManager);
            s.onShow(data);
        });
        
        this._basePipeLine.onShowInvokable();
    }

    public get onSave(): ICallableEvent {
        return this._onSave.expose();
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
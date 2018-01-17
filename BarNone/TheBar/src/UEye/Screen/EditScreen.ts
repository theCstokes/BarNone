import { EditView } from "UEye/View/EditView";
import Screen from "UEye/Screen/Screen";
import { BaseStateManager } from "UEye/StateManager/BaseStateManager";

export default abstract class EditScreen<TView extends EditView, TStateManager extends BaseStateManager<any>> extends Screen<TView> {
    private _stateManager: TStateManager;

    public constructor(ViewType: { new(): TView }, StateManagerType: { new(): TStateManager }) {
        super(ViewType);
        this._stateManager = new StateManagerType();
        this._stateManager.bind(this._onBaseRender.bind(this));
    }

    public get stateManager(): TStateManager {
        return this._stateManager;
    }

    private _onBaseRender(current: any, original: any) {
        var isModified = (JSON.stringify(original) !== JSON.stringify(current));
        
        this.view.cancelButton.enabled = isModified;
        this.view.saveButton.enabled = isModified;
    }

    public onShow(): void {
        console.log("test");
        this.view.cancelButton.onClick = () => {

        };

        this.view.saveButton.onClick = () => {

        }
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
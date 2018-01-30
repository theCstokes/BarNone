import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { BaseView } from "UEye/Elements/Core/BaseView";

export abstract class View extends BaseView {
    
    /**
     * View configuration.
     */
    public get config(): ComponentConfig[] {
        return this.content;
    }

    /**
     * View content to be used in the configuration.
     */
    protected abstract get content(): ComponentConfig[];
}
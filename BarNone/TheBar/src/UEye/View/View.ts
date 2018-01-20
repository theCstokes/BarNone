import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

export abstract class View {
    
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
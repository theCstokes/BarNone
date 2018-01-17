import ComponentConfig from "UEye/Elements/Core/ComponentConfig";

export abstract class View {
    // [name: string]: any;

    public get config(): ComponentConfig[] {
        return this.content;
    }

    protected abstract get content(): ComponentConfig[];
}
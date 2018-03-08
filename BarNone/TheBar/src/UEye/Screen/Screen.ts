import { View } from "UEye/View/View";
import Inflater from "UEye/Elements/Inflater/Inflater";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import StringUtils from "UEye/Core/StringUtils";
import ControlTypes from "UEye/ControlTypes";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";
import { IHelp } from "App/Help/HelpCore";

/**
 * Screen config.
 */
export interface IScreenConfig {
    /**
     * Add screen to history flag.
     */
    addScreenToHistory: boolean;

    /**
     * Add screen to top element to make full screen.
     */
    fullScreen: boolean;
}

/**
 * Screen object.
 */
export default abstract class Screen<TView extends View> {
    /**
     * Screen view
     */
    private _view: TView;
    private _config: IScreenConfig;
    private _help?: IHelp;
    private _screenObj: ContentContainer;
    private _inflaterData: InflaterData;

    public constructor(ViewType: { new(): TView }, HelpType?: { new() :IHelp }
    /*, config?: Partial<IScreenConfig>*/) {
        this._view = new ViewType();
        this._config = this.configure();
        if (HelpType !== undefined) this._help = new HelpType();
        // Object.assign(this._config, config);
    }

    //#region Public Property(s).
    public get view(): TView {
        return this._view;
    }

    public get config(): IScreenConfig {
        return this._config;
    }

    public get help(): IHelp | undefined {
        return this._help;
    }

    public get screenObj(): ContentContainer {
        return this._screenObj;
    }

    public get inflaterData(): InflaterData {
        return this._inflaterData;
    }
    //#endregion

    //#region Public Member(s).
    public create(parent: HTMLElement): InflaterData {
        var config = {
            instance: ControlTypes.ContentContainer,
            id: "screenObj",
            content: this._view.config
        };

        var results = Inflater.execute(parent, config, this._view);
        this._screenObj = results.componentMap.screenObj as ContentContainer;

        this._view.setElements(results.componentMap);
        this._inflaterData = results;    
        return results;
    }

    public destroy(): void {
        this._screenObj.destroy();
    }
    //#endregion

    //#region Public Virtual Member(s).
    public configure(): IScreenConfig {
        return {
            addScreenToHistory: true,
            fullScreen: false
        }
    }
    //#endregion

    //#region Public Abstract Member(s).
    public abstract onShow(data?: any): void;
    //#endregion
}
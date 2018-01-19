import { View } from "UEye/View/View";
import Inflater from "UEye/Elements/Inflater/Inflater";
import InflaterData from "UEye/Elements/Inflater/InflaterData";
import StringUtils from "UEye/Core/StringUtils";
import ControlTypes from "UEye/ControlTypes";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";

/**
 * Screen config.
 */
export interface IScreenConfig {
    /**
     * Add screen to history flag.
     */
    addScreenToHistory: boolean;
}

/**
 * Screen object.
 */
export default abstract class Screen<TView extends View> {
    private _view: TView;
    private _config: IScreenConfig;
    private _screenObj: ContentContainer;

    public constructor(viewType: { new(): TView }, config?: Partial<IScreenConfig>) {
        this._view = new viewType();
        this._config = <IScreenConfig>{};
        Object.assign(this._config, config);
    }

    public create(parent: HTMLElement): InflaterData {
        var config = {
            instance: ControlTypes.ContentContainer,
            id: "screenObj",
            content: this._view.config
        };

        var results = Inflater.execute(parent, config);
        this._screenObj = results.componentMap.screenObj as ContentContainer;

        for (var key in results.componentMap) {
            if (!results.componentMap.hasOwnProperty(key)) continue;
            (this._view as any)[key] = results.componentMap[key];

            if (!(key in this._view)) {
                console.warn(StringUtils.format("{0} is not exposed in view.", key));
            }
        }

        return results;
    }

    public destroy(): void {
        this._screenObj.destroy();
    }

    public get view(): TView {
        return this._view;
    }

    public get config(): IScreenConfig {
        return this._config;
    }

    public abstract onShow(): void;
}
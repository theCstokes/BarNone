import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";

/**
 * Base edit view.
 */
export abstract class DialogView extends View {
    /**
     * Cancel changes button.
     */
    public cancelButton: Button;

    /**
     * Save changes button.
     */
    public saveButton: Button;

    /**
     * View base config
     */
    public get config(): ComponentConfig[] {
        return [
            {
                instance: ControlTypes.Dialog,
                content: this.content,
                // dockBottom: [
                //     {
                //         instance: ControlTypes.OrderLayout,
                //         rightToLeft: true,
                //         content: [
                //             {
                //                 id: "cancelButton",
                //                 instance: ControlTypes.Button,
                //                 icon: "fa-times",
                //                 text: "Cancel"
                //             },
                //             {
                //                 id: "saveButton",
                //                 instance: ControlTypes.Button,
                //                 icon: "fa-floppy-o",
                //                 text: "Save"
                //             }
                //         ]
                //     }
                // ]
            }
        ];
    }
}
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";
import Panel from "UEye/Elements/Containers/Panel/Panel";

/**
 * Base edit view.
 */
export abstract class EditView extends View {
    /**
     * Cancel changes button.
     */
    public cancelButton: Button;

    /**
     * Save changes button.
     */
    public saveButton: Button;

    /** Edit Panel. */
    public editPanel: Panel;

    protected abstract caption: string;

    /**
     * View base config
     */
    public get config(): ComponentConfig[] {
        return [
            {
                instance: ControlTypes.Panel,
                id: "editPanel",
                caption: this.caption,
                content: this.content,
                dockBottom: [
                    {
                        instance: ControlTypes.OrderLayout,
                        rightToLeft: true,
                        content: [
                            {
                                id: "cancelButton",
                                instance: ControlTypes.Button,
                                icon: "fa-times",
                                text: "Cancel",
                                enabled: false
                            },
                            {
                                id: "saveButton",
                                instance: ControlTypes.Button,
                                icon: "fa-save",
                                text: "Save",
                                enabled: false
                            }
                        ]
                    }
                ]
            }
        ];
    }
}
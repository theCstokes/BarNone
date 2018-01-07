import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";

export abstract class EditView extends View {
    public cancelButton: Button;
    public saveButton: Button;

    public get config(): ComponentConfig[] {
        return [
            {
                instance: ControlTypes.Panel,
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
                                text: "Cancel"
                            },
                            {
                                id: "saveButton",
                                instance: ControlTypes.Button,
                                icon: "fa-floppy-o",
                                text: "Save"
                            }
                        ]
                    }
                ]
            }
        ];
    }
}
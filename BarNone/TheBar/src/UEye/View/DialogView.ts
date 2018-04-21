import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import { View } from "UEye/View/View";
import ControlTypes from "UEye/ControlTypes";
import Button from "UEye/Elements/Components/Button/Button";
import { EDialogSize } from "UEye/Elements/Containers/Dialog/Dialog";
import Panel from "UEye/Elements/Containers/Panel/Panel";

/**
 * Base edit view.
 */
export abstract class DialogView extends View {
    public dialogPanel: Panel;
    
    public cancelButton: Button;

    public acceptButton: Button;

    public abstract get caption(): string;

    public abstract get cancelButtonText(): string;

    public abstract get cancelButtonIcon(): string;

    public abstract get acceptButtonText(): string;

    public abstract get acceptButtonIcon(): string;

    public size: EDialogSize = EDialogSize.Normal;

    public get config(): ComponentConfig[] {
        return [
            {
                instance: ControlTypes.Dialog,
                size: this.size,
                content: [
                    {
                        instance: ControlTypes.Panel,
                        id: "dialogPanel",
                        content: this.content,
                        caption: this.caption,
                        dockBottom: [
                            {
                                instance: ControlTypes.OrderLayout,
                                rightToLeft: true,
                                content: [
                                    {
                                        id: "cancelButton",
                                        instance: ControlTypes.Button,
                                        icon: this.cancelButtonIcon,
                                        text: this.cancelButtonText
                                    },
                                    {
                                        id: "acceptButton",
                                        instance: ControlTypes.Button,
                                        icon: this.acceptButtonIcon,
                                        text: this.acceptButtonText,
                                        enabled: false
                                    }
                                ]
                            }
                        ]
                    }
                ]

            }
        ];
    }
}
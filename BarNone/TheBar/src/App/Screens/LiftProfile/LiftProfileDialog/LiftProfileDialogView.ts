import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye";
import Button from "UEye/Elements/Components/Button/Button";
import Dropdown from "UEye/Elements/Components/DropdownInput/DropDownInput";

export default class LiftProfileDialogView extends DialogView {
    public caption: string = "New Lift Profile";
    public cancelButtonText: string = "Cancel"
    public cancelButtonIcon: string = "fa-times";
    public acceptButtonText: string = "Add";
    public acceptButtonIcon: string = "fa-plus";

    protected content: ComponentConfig[] = [
        {

            instance: ControlTypes.DropDownInput,
            id: "jointType",
            hint: "Joint Type"

        },
        {
            instance: ControlTypes.DropDownInput,
            id: "analysisType",
            hint: "Analysis Type"
        }
    ]

    // public get content(): any[] {
    //     return [
    //         {
    //             instance: ControlTypes.Panel,
    //             content: [
    //                 {

    //                     instance: ControlTypes.DropDownInput,
    //                     id: "jointType",
    //                     hint: "Joint Type"

    //                 },
    //                 {
    //                     instance: ControlTypes.DropDownInput,
    //                     id: "analysisType",
    //                     hint: "Analysis Type"
    //                 },
    //                 {
    //                     instance: ControlTypes.Button,
    //                     id: "cancelButton",
    //                     icon: "fa-times",
    //                     text: "Cancel"
    //                 }
    //             ]
    //         }
    //     ]
    // }
}

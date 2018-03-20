import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye";
import Button from "UEye/Elements/Components/Button/Button";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";


export default class LiftProfileDialogView extends DialogView {
    public caption: string = "New Lift Profile";
    public cancelButtonText: string = "Cancel"
    public cancelButtonIcon: string = "fa-times";
    public acceptButtonText: string = "Add";
    public acceptButtonIcon: string = "fa-plus";

    public analysisTypeDropDown: DropDownInput;
    public accelerationContainer: ContentContainer;
    public speedContainer: ContentContainer;
    public positionContainer: ContentContainer;
    public angleContainer: ContentContainer;

    public accelerationJointTypeDropDown: DropDownInput;
    public speedJointTypeDropDown: DropDownInput;
    public positionJointTypeDropDown: DropDownInput;
    public angleJointTypeDropDownA: DropDownInput;
    public angleJointTypeDropDownB: DropDownInput;
    public angleJointTypeDropDownC: DropDownInput;

    protected content: ComponentConfig[] = [
        // {

        //     instance: ControlTypes.DropDownInput,
        //     id: "jointTypeDropDown",
        //     hint: "Joint Type"
        // },
        {
            instance: ControlTypes.DropDownInput,
            id: "analysisTypeDropDown",
            hint: "Analysis Type"
        },
        {
            instance: ControlTypes.ContentContainer,
            id: "accelerationContainer",
            visible: false,
            content: [
                {

                    instance: ControlTypes.DropDownInput,
                    id: "accelerationJointTypeDropDown",
                    hint: "Joint Type"
                }
            ]
        },
        {
            instance: ControlTypes.ContentContainer,
            id: "speedContainer",
            visible: false,
            content: [
                {

                    instance: ControlTypes.DropDownInput,
                    id: "speedJointTypeDropDown",
                    hint: "Joint Type"
                }
            ]
        },
        {
            instance: ControlTypes.ContentContainer,
            id: "positionContainer",
            visible: false,
            content: [
                {

                    instance: ControlTypes.DropDownInput,
                    id: "positionJointTypeDropDown",
                    hint: "Joint Type"
                }
            ]
        },
        {
            instance: ControlTypes.ContentContainer,
            id: "angleContainer",
            visible: false,
            content: [
                {

                    instance: ControlTypes.DropDownInput,
                    id: "angleJointTypeDropDownA",
                    hint: "Joint Type A"
                },
                {

                    instance: ControlTypes.DropDownInput,
                    id: "angleJointTypeDropDownB",
                    hint: "Joint Type B"
                },
                {

                    instance: ControlTypes.DropDownInput,
                    id: "angleJointTypeDropDownC",
                    hint: "Joint Type C"
                }
            ]
        }
    ]
}

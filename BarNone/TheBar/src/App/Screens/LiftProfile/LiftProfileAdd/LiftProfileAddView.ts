import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye";
import Button from "UEye/Elements/Components/Button/Button";
import DropDownInput from "UEye/Elements/Components/DropdownInput/DropdownInput";
import ContentContainer from "UEye/Elements/Containers/ContentContainer/ContentContainer";


export default class LiftProfileAddView extends DialogView {
    public caption: string = "New Lift Profile";
    public cancelButtonText: string = "Cancel"
    public cancelButtonIcon: string = "fa-times";
    public acceptButtonText: string = "Add";
    public acceptButtonIcon: string = "fa-plus";

    public liftTypeDropDown: DropDownInput;

    protected content: ComponentConfig[] = [
        {
            instance: ControlTypes.DropDownInput,
            id: "liftTypeDropDown",
            hint: "Lift Type"
        }
    ]
}

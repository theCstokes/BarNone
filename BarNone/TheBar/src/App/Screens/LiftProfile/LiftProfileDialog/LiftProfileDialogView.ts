import { DialogView } from "UEye/View/DialogView";
import ComponentConfig from "UEye/Elements/Core/ComponentConfig";
import ControlTypes from "UEye/ControlTypes";
import UEye from "UEye/UEye";
import Button from "UEye/Elements/Components/Button/Button"; 
import Dropdown from "UEye/Elements/Components/DropdownInput/DropDownInput"; 

export default class LiftProfileDialogView extends DialogView {
    public get content(): any[] {
		return [
            {
               
                instance:ControlTypes.DropDownInput,
                id: "jointType",
                hint: "Joint Type"
            
            },
            {
                instance:ControlTypes.DropDownInput,
                id: "analysisType",
                hint: "Analysis Type"
            },
            {
                instance:ControlTypes.Button,
                id: "cancelButton",
                icon: "fa-times",
			    text: "Cancel"
            }
        ]
    }
}

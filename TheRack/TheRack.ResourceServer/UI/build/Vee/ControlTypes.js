define(["require", "exports"], function (require, exports) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class ControlTypeHelper {
        static getComponentPath(name) {
            return "Vee/Elements/Components/" + name + "/" + name;
        }
        static getContainerPath(name) {
            return "Vee/Elements/Containers/" + name + "/" + name;
        }
    }
    class ControlTypes {
    }
    // Components.
    ControlTypes.Button = ControlTypeHelper.getComponentPath("Button");
    ControlTypes.ContactListItem = ControlTypeHelper.getComponentPath("ContactListItem");
    ControlTypes.Input = ControlTypeHelper.getComponentPath("Input");
    ControlTypes.Label = ControlTypeHelper.getComponentPath("Label");
    ControlTypes.List = ControlTypeHelper.getComponentPath("List");
    // Containers.
    ControlTypes.Column = ControlTypeHelper.getContainerPath("Column");
    ControlTypes.ColumnLayout = ControlTypeHelper.getContainerPath("ColumnLayout");
    ControlTypes.Frame = ControlTypeHelper.getContainerPath("Frame");
    ControlTypes.LoginFrame = ControlTypeHelper.getContainerPath("LoginFrame");
    ControlTypes.OrderLayout = ControlTypeHelper.getContainerPath("OrderLayout");
    ControlTypes.Panel = ControlTypeHelper.getContainerPath("Panel");
    ControlTypes.PartitionLayout = ControlTypeHelper.getContainerPath("PartitionLayout");
    ControlTypes.Screen = ControlTypeHelper.getContainerPath("Screen");
    exports.default = ControlTypes;
});

//# sourceMappingURL=ControlTypes.js.map

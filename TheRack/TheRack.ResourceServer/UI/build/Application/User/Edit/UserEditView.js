define(["require", "exports", "Vee/View/View", "Vee/ControlTypes"], function (require, exports, View_1, ControlTypes_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserEditView extends View_1.View {
        get content() {
            return [
                {
                    id: "editPanel",
                    instance: ControlTypes_1.default.Panel,
                    caption: "User",
                    content: [
                        {
                            instance: ControlTypes_1.default.Button,
                            text: "Test"
                        },
                        {
                            id: "nameInput",
                            instance: ControlTypes_1.default.Input,
                            hint: "Name"
                        },
                        {
                            id: "ageInput",
                            instance: ControlTypes_1.default.Input,
                            readonly: true,
                            hint: "Age",
                            text: 21
                        }
                    ]
                }
            ];
        }
    }
    exports.default = UserEditView;
});

//# sourceMappingURL=UserEditView.js.map

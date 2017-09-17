define(["require", "exports", "Vee/View/View", "Vee/ControlTypes"], function (require, exports, View_1, ControlTypes_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class NavView extends View_1.View {
        get content() {
            return [
                {
                    instance: ControlTypes_1.default.Frame,
                    statusTitle: "Christopher Stokes",
                    statusImageSource: "res/jedi-symbol.jpg",
                    globalDock: {
                        instance: ControlTypes_1.default.OrderLayout,
                        rightToLeft: true,
                        content: [
                            {
                                instance: ControlTypes_1.default.Label,
                                text: "Hello World"
                            }
                        ]
                    },
                    contextDock: {
                        instance: ControlTypes_1.default.ColumnLayout,
                        columns: [
                            {
                                instance: ControlTypes_1.default.Column,
                                content: [
                                    {
                                        instance: ControlTypes_1.default.Label,
                                        text: "User/Contacts"
                                    }
                                ]
                            },
                            {
                                instance: ControlTypes_1.default.Column,
                                content: [
                                    {
                                        instance: ControlTypes_1.default.Button,
                                        text: "Add Contact"
                                    }
                                ]
                            },
                            {
                                instance: ControlTypes_1.default.Column,
                                content: {}
                            }
                        ]
                    }
                }
            ];
        }
    }
    exports.default = NavView;
});

//# sourceMappingURL=NavView.js.map

define(["require", "exports", "Vee/View/View", "Vee/ControlTypes"], function (require, exports, View_1, ControlTypes_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class LoginView extends View_1.View {
        get content() {
            return [
                {
                    instance: ControlTypes_1.default.LoginFrame,
                    background: 'res/bk1.jpg',
                    content: [
                        {
                            id: "usernameInput",
                            instance: ControlTypes_1.default.Input,
                            hint: "Username"
                        },
                        {
                            id: "passwordInput",
                            instance: ControlTypes_1.default.Input,
                            hint: "Password"
                        },
                        {
                            instance: ControlTypes_1.default.OrderLayout,
                            content: [
                                {
                                    instance: ControlTypes_1.default.Button,
                                    text: "Recover"
                                },
                                {
                                    id: "loginButton",
                                    instance: ControlTypes_1.default.Button,
                                    text: "Login"
                                }
                            ]
                        }
                    ]
                }
            ];
        }
    }
    exports.default = LoginView;
});

//# sourceMappingURL=LoginView.js.map

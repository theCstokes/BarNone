define(["require", "exports", "Vee/View/View", "Vee/ControlTypes"], function (require, exports, View_1, ControlTypes_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class UserView extends View_1.View {
        get content() {
            return [
                {
                    instance: ControlTypes_1.default.PartitionLayout,
                    leftSide: [
                        {
                            instance: ControlTypes_1.default.Panel,
                            id: "mainPanel",
                            caption: "Users",
                            content: [
                                {
                                    instance: ControlTypes_1.default.List,
                                    id: "userList",
                                    style: ControlTypes_1.default.ContactListItem
                                    // items: [
                                    // 	{
                                    // 		name: "Christopher Stokes",
                                    // 		selected: true
                                    // 	},
                                    // 	{
                                    // 		name: "Bob Bill"
                                    // 	}
                                    // ]
                                }
                            ]
                        }
                    ]
                }
            ];
        }
    }
    exports.default = UserView;
});

//# sourceMappingURL=UserView.js.map

define(["require", "exports", "Vee/Elements/Core/BaseElement/BaseElement", "Vee/Elements/Core/Core"], function (require, exports, BaseElement_1, Core_1) {
    "use strict";
    Object.defineProperty(exports, "__esModule", { value: true });
    class BaseContainer extends BaseElement_1.BaseElement {
        constructor(parent, ...style) {
            super(parent, ...style);
            Core_1.default.addClass(this.element, 'UEye-Container');
            this.componentContainers = {};
            this.screenContainers = {};
        }
        linkComponentContainer(name, element) {
            this.componentContainers[name] = { element: element, value: [null] };
        }
        setComponentContainer(name, value) {
            if (this.componentContainers.hasOwnProperty(name)) {
                this.componentContainers[name].value = value;
            }
        }
        getComponentContainer(name) {
            if (this.componentContainers.hasOwnProperty(name)) {
                return this.componentContainers[name].value;
            }
            return [];
        }
        linkScreenContainer(name, element) {
            this.screenContainers[name] = { element: element, value: [null] };
        }
        setScreenContainer(name, value) {
            if (this.screenContainers.hasOwnProperty(name)) {
                this.screenContainers[name].value = value;
            }
        }
        getScreenContainer(name) {
            if (this.screenContainers.hasOwnProperty(name)) {
                return this.screenContainers[name].value;
            }
            return [];
        }
        getComponentContainerElement(name) {
            if (this.componentContainers.hasOwnProperty(name)) {
                return this.componentContainers[name].element;
            }
            console.warn("Could not find component container with name: " + name);
            return null;
        }
        getScreenMountingPoints() {
            return Object.keys(this.screenContainers).map(key => this.screenContainers[key].element);
        }
    }
    exports.BaseContainer = BaseContainer;
});

//# sourceMappingURL=BaseContainer.js.map

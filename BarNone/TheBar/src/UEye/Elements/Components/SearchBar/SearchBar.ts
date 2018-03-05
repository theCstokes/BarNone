import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import SearchTag from "UEye/Elements/Components/SearchTag/SearchTag"
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

import ControlTypes from "UEye/ControlTypes";

class SearchConfig implements IListItem {
    public id: number | string;
    public title: string;
}

class SearchTagBind {
    public searchItem: HTMLElement;

    public searchTag: SearchTag;

    //public selected: boolean;

    public constructor(item:HTMLElement, tag:SearchTag){
        this.searchItem=item;
        this.searchTag=tag;
        //this.selected=sel;

    }
}

export default class SearchBar extends BaseComponent {
    /**Represents the list of searchable items that be selected. Appears as a list under the input bar*/
    private e_content: HTMLElement;

    private e_searchDropdown: HTMLElement;
      /**Represents the list of items that are selected */
    private e_tagList: HTMLElement;
     /**Represents the search input element*/
    private e_inputElement: HTMLInputElement;
    /** Represents the list of indivual list items in search list*/
    private e_searchItems: SearchTagBind[];


    private _items: SearchConfig[];
    private _tags: SearchTag[];
    private _hint:string;
    private _text:string;
    private _onChangeCallback: OnChangeCallback;
    private _onClickCallback: OnClickCallback;
    
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Search-Bar");
        this.e_content= Core.create("div", this.element, "Search-Bar-Content");
        this.e_searchDropdown= Core.create("ul", this.element, "Search-Bar-Dropdown");
        this.e_tagList= Core.create("ul", this.e_content, "Search-Bar-Tags");
        this.e_inputElement= Core.create("input", this.e_content, "Search-Bar-Input") as HTMLInputElement;
        this.e_inputElement.placeholder="Search...";
        
        this.e_inputElement.onblur = this.onBlurHandler.bind(this);
        this.e_inputElement.onfocus = this.onFocusHandler.bind(this);
        this.e_inputElement.oninput = this.onInputHandler.bind(this);
    }
    
    public set items(value: SearchConfig[]) {
        //this.destroyItems();
        this._items = value;
        this.refreshItems();
    }

    public get items():SearchConfig[] {
        return this._items;
    }

    private refreshItems() {
        this.e_searchItems = [];
        this._tags = [];
        this._items.forEach( element => {
                var listElement = Core.create("li", this.e_searchDropdown, "Search-Dropdown-Element");
                listElement.textContent=element.title;
                Core.addClass(listElement, "Hidden");
                listElement.onclick = this.onClickHandler.bind(this);
                var tagElement=  new SearchTag(this.e_tagList);
                tagElement.text=listElement.innerText;
                Core.addClass(tagElement.element, "Hidden");
                tagElement.selected=false;
                tagElement.onClick = () => {
                    //tagElement.element.style.display="none";
                    Core.addClass(tagElement.element, "Hidden");
                    tagElement.selected=false;
                };
                tagElement.selected=true;
                var searchManager= new SearchTagBind(listElement,tagElement);
                this.e_searchItems.push(searchManager);
           
        });
    }


    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    private onInputHandler(): void {
        var filter=  this.e_inputElement.value.toUpperCase();
        this.e_searchItems.forEach( element => {
            if (filter !="" && element.searchItem.innerHTML.toUpperCase().indexOf(filter) > -1) {
                Core.removeClass(element.searchItem, "Hidden");
            }
            else{
                Core.addClass(element.searchItem, "Hidden");
                // element.searchItem.style.display="none";
            }
        });

        if (this._onChangeCallback !== undefined) {
            this._onChangeCallback(this.e_inputElement.value);
        }
        
    }
    private onClickHandler(e: Event): void {
      
        this.e_searchItems.forEach( element => {
            if(element.searchItem === e.target){
                Core.addClass(element.searchItem, "Hidden");
                element.searchTag.selected=true;
                Core.removeClass(element.searchTag.element, "Hidden");
            }
            });
      
    }
    private onFocusHandler(): void {
        Core.addClass(this.e_content, "Focused");
    }
    private onBlurHandler(): void {
        Core.removeClass(this.e_content, "Focused");
    }

    }

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

export default class SearchBar extends BaseComponent {
    /**Represents the list of searchable items that be selected. Appears as a list under the input bar*/
    private e_content: HTMLElement;

    private e_searchDropdown: HTMLElement;
      /**Represents the list of items that are selected */
    private e_tagList: HTMLElement;
     /**Represents the search input element*/
    private e_inputElement: HTMLInputElement;
    /** Represents the list of indivual list items in search list*/
    private e_searchItems: HTMLElement[];


    private _items: SearchConfig[];
    private _tags: SearchTag[];
    private _hint:string;
    private _text:string;
    private _onCloseCallback: OnClickCallback;
    private _onChangeCallback: OnChangeCallback;
    private _onClickCallback: OnClickCallback;
    
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Search-Bar");
        this.e_content= Core.create("div", this.element, "Search-Bar-Content");
        this.e_searchDropdown= Core.create("ul", this.element, "Search-Bar-Dropdown");
        this.e_tagList= Core.create("ul", this.e_content, "Search-Bar-Tags");
        this.e_inputElement= Core.create("input", this.e_content, "Search-Bar-Input") as HTMLInputElement;
       
        this.e_inputElement.placeholder="Search...";
    
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
            listElement.style.display="none";
            listElement.onclick = this.onClickHandler.bind(this);
            this.e_searchItems.push(listElement);
        });
    }


    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    private onInputHandler(): void {
        var filter=  this.e_inputElement.value.toUpperCase();
        var i;
        for (i = 0; i < this.e_searchItems.length; i++) {
              if (filter !="" && this.e_searchItems[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                this.e_searchItems[i].style.display = "";
                
              } else {
                this.e_searchItems[i].style.display = "none";
              }
            } 

        if (this._onChangeCallback !== undefined) {
            this._onChangeCallback(this.e_inputElement.value);
        }
        
    }
    private onClickHandler(e: Event): void {
        console.log("ITEM SELECTED");
        var i;
        for (i=0;i<this.e_searchItems.length;i++){
            if(this.e_searchItems[i] === e.target){

                var selectedElement = Core.create("li", this.e_searchDropdown, "Search-Tag-Element");
                var tagElement=  new SearchTag(this.e_tagList);
                tagElement.text=this.e_searchItems[i].innerText;
                tagElement.selected=true;
                this._tags.push(tagElement);
                this.e_searchItems[i].style.display="none";
                var parentNode = this.e_searchItems[i].parentNode;
                console.log("Parent Node",parentNode);
                if (parentNode !== null) {
                    parentNode.removeChild(this.e_searchItems[i]);
                }
                this.e_searchItems.splice(i,1);
                console.log("Selected",this.e_searchItems);
        
      
    }
       
    }
    // private onClickClose(e: Event): void{
    //     var i;
    //     for (i=0;i<this._selectedItems.length;i++){
    //         if(e.target===this._selectedItems[i]){

    //         }
    //     }
    // }

    }
}
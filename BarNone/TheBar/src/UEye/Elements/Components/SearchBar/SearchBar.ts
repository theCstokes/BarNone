import { BaseComponent } from "UEye/Elements/Core/BaseComponent/BaseComponent";
import Core from "UEye/Elements/Core/Core";
import { OnChangeCallback } from "UEye/Elements/Core/EventCallbackTypes";
import { IListItem } from "UEye/Elements/Core/EventCallbackTypes";
import { OnClickCallback } from "UEye/Elements/Core/EventCallbackTypes";

class SearchConfig implements IListItem {
    public id: number | string;

    public title: string;
}

export default class SearchBar extends BaseComponent {

    private _searchList: HTMLElement;
    private _selectedList: HTMLElement;
    private _onOpenActionCallback: OnClickCallback;
    private _searchInput: HTMLInputElement;
    private _listItems: HTMLElement[];
    private _items: SearchConfig[];
    private _selectedItems: HTMLElement[];
    private _onChangeCallback: OnChangeCallback;
    private _onClickCallback: OnClickCallback;
    
    public constructor(parent: HTMLElement) {
        super(parent, "UEye-Search-Bar");
        this._selectedList = Core.create("ul", this.element, "Selected-List");
        this._searchInput= Core.create("input", this.element, "Search-Bar-Input") as HTMLInputElement;
        this._searchList = Core.create("ul", this.element, "Search-Bar-List");
       
        this._searchInput.placeholder="Search...";
    
        this._searchInput.oninput = this.onInputHandler.bind(this);
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
        this._listItems = [];
        this._selectedItems = [];
        this._items.forEach( element => {
            console.log("MNLKDKF");
            var listElement = Core.create("li", this._searchList, "Search-List-Element");
            listElement.textContent=element.title;
            listElement.style.display="none";
            listElement.onclick = this.onClickHandler.bind(this);
            this._listItems.push(listElement);
        });
    }


    public onEnabledChange(): void {
        throw new Error("Method not implemented.");
    }
    private onInputHandler(): void {
        var filter=  this._searchInput.value.toUpperCase();
      
        var i;
        for (i = 0; i < this._listItems.length; i++) {
              if (filter !="" && this._listItems[i].innerHTML.toUpperCase().indexOf(filter) > -1) {
                this._listItems[i].style.display = "";
                
              } else {
                this._listItems[i].style.display = "none";
              }
            } 

        if (this._onChangeCallback !== undefined) {
            this._onChangeCallback(this._searchInput.value);
        }
        
    }
    private onClickHandler(e: Event): void {
        var i;
        for (i=0;i<this._listItems.length;i++){
            if(this._listItems[i] === e.target){
                var selectedElement = Core.create("li", this._selectedList, "Selected-List-Element");
                var close = Core.create("div", selectedElement, "Close fa fa-times");
                close.onclick=this.onClickClose.bind(this);
                selectedElement.textContent=this._listItems[i].innerText;
                this._selectedItems.push(selectedElement);
                this._listItems[i].style.display="none";
                var parentNode = this._listItems[i].parentNode;
                if (parentNode !== null) {
                    parentNode.removeChild(this._listItems[i]);
                }
                this._listItems.splice(i,1);
                console.log("Selected",this._selectedItems);
        }
      
    }
       
    }
    private onClickClose(e: Event): void{
        var i;
        for (i=0;i<this._selectedItems.length;i++){
            if(e.target===this._selectedItems[i]){

            }
        }
    }

}
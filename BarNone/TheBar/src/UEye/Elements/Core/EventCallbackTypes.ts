export interface IListItem {
    id: any;    
}

export type OnChangeCallback = (data: any) => void;

export type OnClickCallback = () => void;

export type OnSelectCallback = (data: IListItem) => void;
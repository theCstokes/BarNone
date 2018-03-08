export interface IListItem {
    id: any | string;
}

export type OnChangeCallback = (data: any) => void;

export type OnActionCallback = (data: any) => void;

export type OnClickCallback = () => void;

export type OnSelectCallback = (data: IListItem) => void;
type DataEventCallback = () => void;

export interface IDataEvent {
    register(callback: DataEventCallback): void;

    remove(callback: DataEventCallback): void;
}

export default class DataEvent {
    private _callbackList: DataEventCallback[];

    public register(callback: DataEventCallback) {
        this._callbackList.push(callback);
    }

    public remove(callback: DataEventCallback) {
        this._callbackList = this._callbackList.filter(cb => cb !== callback);
    }

    public trigger() {
        this._callbackList.forEach(cb => cb());
    }

    public expose(): IDataEvent {
        return this as IDataEvent;
    }
}
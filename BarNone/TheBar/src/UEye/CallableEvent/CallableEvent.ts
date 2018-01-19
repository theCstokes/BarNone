type CallableEventCallback = () => void;

export interface ICallableEvent {
    register(callback: CallableEventCallback): void;

    remove(callback: CallableEventCallback): void;
}

export default class CallableEvent {
    private _callbackList: CallableEventCallback[];

    public constructor() {
        this._callbackList = [];
    }

    public register(callback: CallableEventCallback) {
        this._callbackList.push(callback);
    }

    public remove(callback: CallableEventCallback) {
        this._callbackList = this._callbackList.filter(cb => cb !== callback);
    }

    public trigger() {
        this._callbackList.forEach(cb => cb());
    }

    public expose(): ICallableEvent {
        return this as ICallableEvent;
    }
}
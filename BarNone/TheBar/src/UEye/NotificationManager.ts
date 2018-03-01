import StringUtils from "UEye/Core/StringUtils";
import { BaseDataManager } from "UEye/Data/BaseDataManager";
import NotificationRequestDTO from "App/Data/Models/NotificationRequestDTO";

type NotificationCallback = () => void;

export default class NotificationManager {

	private static socketList: WebSocket[] = [];

	public static addListener<T>(dto: NotificationRequestDTO<T>, callback: NotificationCallback) {
		var path = StringUtils.format("ws://localhost:58428?access_token={0}",
			BaseDataManager.auth.access_token);

		var ws = new WebSocket(path);
		ws.onopen = () => {
			console.log("onopen");

			ws.send(JSON.stringify(dto.create()));
		};
		ws.onmessage = function (e) {
			console.log("echo from server : " + e.data);
			// App.Toast.showMessage(e.data);   
			// callback(JSON.parse(e.data));
			callback();
		};

		ws.onclose = function () {
			console.log("onclose");
		};
		ws.onerror = function () {
			console.log("onerror");
		};		

		NotificationManager.socketList.push(ws);
	}

}
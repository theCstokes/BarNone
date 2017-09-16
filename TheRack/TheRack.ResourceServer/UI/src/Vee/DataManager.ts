export default class DataManager {
	private static readonly authURL = "http://localhost:61761/api/v1/authorize/login";
	// private static readonly authURL = "http://localhost:63202/api/v1/authorize/login";
	private static readonly defaultURL = "http://localhost:63202/api/v1/";

	private static readonly grant_type = "password";
	private static readonly client_id = "099153c2625149bc8ecb3e85e03f0022";

	public static async authorize(username: string, password: string): Promise<boolean> {

		var http = new XMLHttpRequest();

		var args = "username=" + username +
			"&password=" + password +
			"&grant_type=" + DataManager.grant_type +
			"&client_id=" + DataManager.client_id;

		http.open("POST", DataManager.authURL, true);
		http.setRequestHeader("Access-Control-Allow-Origin", "*");
		http.setRequestHeader("Content-type", "application/x-www-form-urlencoded");

		var result = new Promise<boolean>((resolve, reject) => {
			http.onreadystatechange = function () {
				if (http.readyState == 4 && http.status == 200) {
					console.log(http.responseText);
					resolve(true);
				} else if(http.readyState == 4) {
					resolve(false);
				}
			}
		});

		http.send(args);

		return result;
	}
}
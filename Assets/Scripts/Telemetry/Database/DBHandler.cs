
using UnityEngine;

public class DBHandler: ScriptableObject{

	static DBFirebase firebase = new DBFirebase();

	public static void saveSessionsData(string json) {
		firebase.saveSessionsData(json);
	}

	public static void loadSessionsData(string key) {
		firebase.loadSessionsData(key);
	}
}

using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System.Dynamic;
using Newtonsoft.Json;
using UnityEngine;

//[CreateAssetMenu(fileName = "DBFirebase", menuName = "Telemetry/DBFirebase")]
public class DBFirebase{

	public string dbUrl = "https://telemetry-data-test-default-rtdb.firebaseio.com/";

	DatabaseReference reference;

	public DBFirebase()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl(dbUrl);

        this.reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

	public void saveSessionsData(string json) {

		Debug.Log(dbUrl);

		string key = this.reference.Child("Users").Push().Key;
		this.reference.Child("Users").Child(key).SetRawJsonValueAsync(json);
	}

	public void loadSessionsData(string key) {
		FirebaseDatabase.DefaultInstance
			.GetReference(key)
			.GetValueAsync().ContinueWith(task => {
				if (task.IsFaulted) {
					Debug.Log("Could not load session data");
				}
				else if (task.IsCompleted) {
					DataSnapshot snapshot = task.Result;
					TelemetryCore.setSessionsData(snapshot.GetRawJsonValue());
				}
			});
	}
}

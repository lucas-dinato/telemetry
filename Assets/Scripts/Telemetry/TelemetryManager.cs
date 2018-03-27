using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TelemetryManager : MonoBehaviour {
	static public TelemetryManager instance = null;

	public bool isSavingActivated = false;
	public List<string> playableScenes = new List<string>();

	bool isRoundRunning = false;

	void Awake() { prepareSingleton(); }

	void OnEnable()	{
		SceneManager.sceneLoaded += OnSceneLoaded;
		SceneManager.sceneUnloaded += OnSceneUnloaded;
	}

	void OnDisable() {
		SceneManager.sceneLoaded -= OnSceneLoaded;
		SceneManager.sceneUnloaded -= OnSceneUnloaded;
	}

	void OnSceneLoaded(Scene scene, LoadSceneMode mode)	{
		if(playableScenes.Contains(scene.name)) {
			//Debug.Log("Telemetry: Starting new Round for scene: " + scene.name);
			this.isRoundRunning = true;
			TelemetryCore.newRound(scene.name);
		}
	}

	void OnSceneUnloaded(Scene scene)	{
		if(playableScenes.Contains(scene.name)) {
			//Debug.Log("Telemetry: Ending new Round for scene: " + scene.name);
			this.isRoundRunning = false;
			TelemetryCore.endRound();
		}
	}

	void OnApplicationQuit() {
		if(this.isRoundRunning) {
			// If round didn't finish yet, ends it
			TelemetryCore.endRound();
		}

		TelemetryCore.setPlayerInfo("Session Duration", Time.realtimeSinceStartup);

		if(isSavingActivated) {
			DBHandler.saveSessionsData(JsonConvert.SerializeObject(TelemetryCore.getPlayerInfo()));
		}
	}

	void prepareSingleton() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(this);
        }
	}
}
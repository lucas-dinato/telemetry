using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Telemetry_Synthesis : MonoBehaviour {

	public bool loadInfo = false;
	public bool read = false;

	public int totalSessions = 0;
	public string avgSessionDuration = "";
	public float avgRoundsPerSession = 0;
	
	float averageSessionDuration = 0f;

	void Update () {
		if(loadInfo) {
			TelemetryCore.loadSessionsData("Users");
			loadInfo = false;
		}
		
		if(read) {
			if(TelemetryCore.getSessionsData() != null) {
				clear();
				setup(TelemetryCore.getSessionsData());
			}
			read = false;
		}
	}

	void clear() {
		totalSessions = 0;
		avgSessionDuration = "0:00:00";
		avgRoundsPerSession = 0f;
		
		averageSessionDuration = 0f;
	}

	void setup(string data) {
		JObject info = JObject.Parse(data);

		totalSessions = info.Count;

		foreach (var session in info) {
			float sessionDuration = session.Value["Info"]["Session Duration"].Value<float>();
            averageSessionDuration += sessionDuration;

			foreach (var round in session.Value["Rounds"]) {
				avgRoundsPerSession += 1;
			}
		}
		
		averageSessionDuration /= totalSessions;
		avgSessionDuration = formatToTime(averageSessionDuration);

		avgRoundsPerSession /= totalSessions;
	}

	string formatToTime(float counter) {
        int hours = (int) counter / 3600;
        int minutes = (int) (counter % 3600) / 60;
        int seconds = (int) (counter % 3600) % 60;
		return string.Format("{0}:{1:00}:{2:00}", hours, minutes, seconds);
	}

	Vector3 positionVectorFromNode(JToken node) {
		return new Vector3(
				node["Position"]["x"].Value<float>(),
				node["Position"]["y"].Value<float>(),
				node["Position"]["z"].Value<float>()
			);
	}
}

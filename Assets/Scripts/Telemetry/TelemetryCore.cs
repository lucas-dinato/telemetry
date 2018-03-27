using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class TelemetryCore {
	
	static Telemetry_PlayerInfo playerInfo = new Telemetry_PlayerInfo();
	static string sessionsData = null;
	
	static Telemetry_RoundInfo currentRound = null;
	static float roundInitialTime = 0f;

	public static Telemetry_PlayerInfo getPlayerInfo() {
		return playerInfo;
	}

	public static void newRound(string sceneName){
		currentRound = new Telemetry_RoundInfo(sceneName);
		roundInitialTime = Time.realtimeSinceStartup;
	}

	public static int addNode(TelemetryNode node){
		return currentRound.addNode(node);
	}

	public static void endRound(){
		float roundDuration = Time.realtimeSinceStartup - roundInitialTime;
		currentRound.setDuration(roundDuration);
		
		playerInfo.addRound(currentRound);
	}

	public static void setPlayerInfo(string key, object value) {
		playerInfo.setInfo(key, value);
	}

	public static object getPlayerInfo(string key) {
		return playerInfo.getInfo(key);
	}

	public static bool containsPlayerInfo(string key) {
		return playerInfo.containsInfo(key);
	}

	public static void setSessionsData(string data) {
		TelemetryCore.sessionsData = data;
	}

	public static string getSessionsData() {
		return TelemetryCore.sessionsData;
	}

	public static void loadSessionsData(string key) {
		DBHandler.loadSessionsData(key);
	}
}
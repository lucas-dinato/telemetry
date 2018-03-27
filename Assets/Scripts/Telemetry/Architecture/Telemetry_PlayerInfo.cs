using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Telemetry_PlayerInfo : ISerializable {

	List<Telemetry_RoundInfo> rounds;
	Dictionary<string, object> additionalInfo;

	public Telemetry_PlayerInfo() {
		this.rounds = new List<Telemetry_RoundInfo>();
		this.additionalInfo = new Dictionary<string, object>();
	}

	public void addRound(Telemetry_RoundInfo roundInfo) {
		this.rounds.Add(roundInfo);
	}

	public void setInfo(string key, object value) {
		additionalInfo[key] = value;
	}

	public object getInfo(string key) {
		return additionalInfo[key];
	}

	public bool containsInfo(string key) {
		return additionalInfo.ContainsKey(key);
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)	{
		info.AddValue("Info", this.additionalInfo, typeof(Dictionary<string, object>));
		info.AddValue("Rounds", this.rounds, typeof(List<Telemetry_RoundInfo>));
	}
}

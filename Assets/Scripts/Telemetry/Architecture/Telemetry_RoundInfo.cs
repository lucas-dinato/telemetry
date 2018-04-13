using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Telemetry_RoundInfo : ISerializable {
	List<TelemetryNode> nodes;
	string sceneName;
	float duration;

	public Telemetry_RoundInfo (string sceneName) {
		this.nodes = new List<TelemetryNode> ();
		this.sceneName = sceneName;
		this.duration = 0f;
	}

	public int addNode (TelemetryNode node) {
		int nodeId = this.nodes.Count;
		node.setId (nodeId);
		this.nodes.Add (node);
		
		return nodeId;
	}

	public void setDuration (float duration) {
		this.duration = duration;
	}

	public void GetObjectData (SerializationInfo info, StreamingContext context) {
		info.AddValue ("Scene Name", this.sceneName, typeof (string));
		info.AddValue ("Duration", this.duration, typeof (float));
		info.AddValue ("Nodes", this.nodes, typeof (List<TelemetryNode>));
	}
}
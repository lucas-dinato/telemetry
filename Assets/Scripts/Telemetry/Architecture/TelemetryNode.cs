using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UnityEngine;

public class TelemetryNode : ISerializable {

	TelemetryNodeType nodeType;
	string name;
	float time;
	int id;
	int link;
	Vector3 position;
	ExpandoObject info;

	public TelemetryNode (TelemetryNodeType nodeType, string name, Vector3 position, ExpandoObject info = null) {
		this.nodeType = nodeType;
		this.id = -1;
		this.link = -1;
		this.name = name;
		this.position = position;

		this.info = info != null ? info : new ExpandoObject ();

		this.time = Time.realtimeSinceStartup;
	}

	public TelemetryNodeType getType () { return this.nodeType; }
	public string getName () { return this.name; }
	public float getTime () { return this.time; }
	public Vector3 getPosition () { return this.position; }
	public ExpandoObject getInfo () { return this.info; }

	public void setId (int nodeId) {
		this.id = nodeId;
	}

	public void setLink (int linkedNodeId) {
		this.link = linkedNodeId;
	}

	public void GetObjectData (SerializationInfo info, StreamingContext context) {
		info.AddValue ("ID", this.id, typeof (int));
		info.AddValue ("Type", this.nodeType.Value, typeof (string));
		info.AddValue ("Link", this.link, typeof (int));
		info.AddValue ("Name", this.name, typeof (string));
		info.AddValue ("Time", this.time, typeof (float));

		dynamic position = new ExpandoObject ();
		position.x = this.position.x;
		position.y = this.position.y;
		position.z = this.position.z;
		info.AddValue ("Position", position, typeof (ExpandoObject));

		info.AddValue ("Info", this.info, typeof (ExpandoObject));
	}
}
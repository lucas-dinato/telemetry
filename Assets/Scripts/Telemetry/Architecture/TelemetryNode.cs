using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Dynamic;
using Newtonsoft.Json;
using UnityEngine;

public class TelemetryNode : ISerializable {

	TelemetryNodeType nodeType;
	string name;
	float time;
	int link;
	Vector3 position;
	ExpandoObject info;

	public TelemetryNode(TelemetryNodeType nodeType, string name, Vector3 position, ExpandoObject info) {
		this.nodeType = nodeType;
		this.link = -1;
		this.name = name;
		this.position = position;
		this.info = info;
		this.time = Time.realtimeSinceStartup;
	}

	public TelemetryNode(TelemetryNodeType nodeType, string name, Vector3 position)
    {
			this.nodeType = nodeType;
			this.link = -1;
			this.name = name;
			this.position = position;
			this.time = Time.realtimeSinceStartup;
			this.info = new ExpandoObject();
    }

	public TelemetryNodeType getType() { return this.nodeType; }
	public string getName() { return this.name; }
	public float getTime() { return this.time; }
	public Vector3 getPosition() { return this.position; }
	public ExpandoObject getInfo() { return this.info; }

	public void setLink(int nodeId) { 
		this.link = nodeId;
	}

	public void GetObjectData(SerializationInfo info, StreamingContext context)
	{
		info.AddValue("Type", this.nodeType.Value, typeof(string));
		info.AddValue("Link", this.link, typeof(int));
		info.AddValue("Name", this.name, typeof(string));
		info.AddValue("Time", this.time, typeof(float));
		
		dynamic position = new ExpandoObject();
			position.x = this.position.x;
			position.y = this.position.y;
			position.z = this.position.z;
		info.AddValue("Position", position, typeof(ExpandoObject));

		info.AddValue("Info", this.info, typeof(ExpandoObject));		
	}
}

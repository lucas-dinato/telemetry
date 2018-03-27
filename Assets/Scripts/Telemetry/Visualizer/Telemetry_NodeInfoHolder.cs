using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class Telemetry_NodeInfoHolder : MonoBehaviour {
	JToken nodeInfo = null;

	public void setInfo(JToken info) {
		nodeInfo = info;
	}

	public JToken getInfo() {
		return nodeInfo;
	}
}

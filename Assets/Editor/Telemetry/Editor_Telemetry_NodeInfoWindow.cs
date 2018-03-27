using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Telemetry_NodeInfoWindow))]
public class Editor_Telemetry_NodeInfoWindow : Editor {

	private Telemetry_NodeInfoWindow it;

	void OnEnable()
	{
			it = target as Telemetry_NodeInfoWindow;
	}

	public override void OnInspectorGUI()
	{
			EditorUtility.SetDirty( target );
			JToken nodeInfo = it.getInfo();
			if(nodeInfo != null) {
				plotToken(nodeInfo);			
			}
	}

	void plotRow(string name, string value) {
		GUILayout.BeginHorizontal("box");
		GUILayout.Label(name);
		GUILayout.Label(value);
		GUILayout.EndHorizontal();
	}

	void plotToken(JToken token) {
		JObject info = token.Value<JObject>();


		foreach(var content in info) {
			var key = content.Key;
			var value = content.Value;
			
			if(value.Type.ToString().Equals("Object")){
				GUILayout.BeginVertical("box");
				GUILayout.Label(key);
				plotToken(value);
				GUILayout.EndVertical();
			}
			else{
				plotRow(key, value.ToString());
			}
		}	

	}
}
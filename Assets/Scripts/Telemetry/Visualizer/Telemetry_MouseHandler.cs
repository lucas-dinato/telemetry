using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

/*
  This class handles the mouse interaction
  with the telemetry nodes drawed in scene.
*/
public class Telemetry_MouseHandler : MonoBehaviour {

  [SerializeField] string telemetryNodeTag = "TelemetryNode";

  Telemetry_NodeInfoHolder currentTooltipHandler = null;
  Telemetry_NodeInfoWindow nodeInfoWindow = null;

  void Awake() {
    nodeInfoWindow = GetComponent<Telemetry_NodeInfoWindow>();
  }

	void OnEnable() {
		//Debug.Log("Telemetry: Mouse interaction enabled");
    SceneView.onSceneGUIDelegate += this.OnSceneMouseOver;
  }

  void OnSceneMouseOver(SceneView view) {
    RaycastHit hit;
    Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

    if (Physics.Raycast (ray, out hit, 100f)) {

      if(hit.transform.tag.Equals(telemetryNodeTag)) {
        currentTooltipHandler = hit.transform.GetComponent<Telemetry_NodeInfoHolder>();
        JToken nodeInfo = currentTooltipHandler.getInfo();
        nodeInfoWindow.setInfo(nodeInfo);
      }
    }
  }
	
}

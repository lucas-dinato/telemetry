using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

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
    #if UNITY_EDITOR
    SceneView.onSceneGUIDelegate += this.OnSceneMouseOver;
    #endif
  }

  #if UNITY_EDITOR
  void OnSceneMouseOver(SceneView view) {
    RaycastHit hit;
    Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);

    if (Physics.Raycast (ray, out hit)) {
      Debug.Log("hitu");
      if(hit.transform.tag.Equals(telemetryNodeTag)) {
        currentTooltipHandler = hit.transform.GetComponent<Telemetry_NodeInfoHolder>();
        JToken nodeInfo = currentTooltipHandler.getInfo();
        nodeInfoWindow.setInfo(nodeInfo);
      }
    }
  }
  #endif
}

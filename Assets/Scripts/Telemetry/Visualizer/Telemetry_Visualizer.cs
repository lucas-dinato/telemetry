using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Telemetry_Visualizer : MonoBehaviour {
	public GameObject nodesBag = null;
	public GameObject atomicModel = null;
	public GameObject singleEventModel = null;
	public GameObject chainEventModel = null;

	[Space(10)]

	public string sceneName = "";
	public bool detectSceneName = true;

    [Space(10)]

	public bool loadInfo = false;
	public bool draw = false;

    [Space(10)]

	public int roundsCount = 0;
	public int selectedRound = 0;

	List<JToken> roundNodes = new List<JToken>();
	bool isReading = false;

	void Start() {
		if(this.detectSceneName) {
			this.sceneName = SceneManager.GetActiveScene().name;
		}
	}

	void Update () {
		if(loadInfo) {
			TelemetryCore.loadSessionsData("Users");
			loadInfo = false;
			isReading = true;
		}
		
		if(isReading) {
			if(TelemetryCore.getSessionsData() != null) {
                clearData();
				setup(TelemetryCore.getSessionsData());
			}
			isReading = false;
		}

		if(draw) {
			clearScene();
			render();
			draw = false;
		}
	}

	void setup(string data) {
		JObject info = JObject.Parse(data);

		foreach (var session in info) {
			foreach (var round in session.Value["Rounds"]) {
				string roundSceneName = round["Scene Name"].Value<string>();
				if (roundSceneName == this.sceneName) {
					roundNodes.Add(round["Nodes"]);
				}
			}
		}

		roundsCount = roundNodes.Count;
	}

	void render() {
		if(roundNodes.Count == 0) return;
		if(selectedRound >= roundNodes.Count || selectedRound < 0) {
			Debug.LogWarning("Telemetry: Selected round is invalid");
			return;
		}

		foreach(var node in roundNodes[selectedRound])
		{
			Vector3 position = positionVectorFromNode(node);
			string nodeType = node["Type"].Value<string>();

			GameObject instantiatedNode = instantiateNode(nodeType, position);
			instantiatedNode.transform.SetParent(nodesBag.transform);

			Telemetry_NodeInfoHolder tooltip = instantiatedNode.GetComponent<Telemetry_NodeInfoHolder>();
			tooltip.setInfo(node);

			int linkId = node["Link"].Value<int>();
			if(linkId != -1) {
				//Renders link line between two nodes
				LineRenderer lineRenderer = instantiatedNode.GetComponent<LineRenderer>();
				Vector3 linkPosition = positionVectorFromNode(roundNodes[selectedRound][linkId]);
				Vector3[] positionsToLink = {position, linkPosition};				
				lineRenderer.SetPositions(positionsToLink);
			}
		}
	}

	GameObject instantiateNode(string nodeType, Vector3 position) {
        GameObject instantiatedNode = null;

        if (nodeType == "Atomic")
        {
            instantiatedNode = Instantiate(atomicModel, position, Quaternion.Euler(Vector3.up));
        }
        else if (nodeType == "Single Event")
        {
            instantiatedNode = Instantiate(singleEventModel, position, Quaternion.Euler(Vector3.up));
        }
        else if (nodeType == "Chain Event")
        {
            instantiatedNode = Instantiate(chainEventModel, position, Quaternion.Euler(Vector3.up));
        }

		return instantiatedNode;
	}

	void clearData() {
		roundNodes.Clear();
	}

	void clearScene() {
		foreach (Transform child in nodesBag.transform) {
			Destroy(child.gameObject);
		}
	}

	Vector3 positionVectorFromNode(JToken node) {
		return new Vector3(
				node["Position"]["x"].Value<float>(),
				node["Position"]["y"].Value<float>(),
				node["Position"]["z"].Value<float>()
			);
	}
}

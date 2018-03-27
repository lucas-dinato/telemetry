using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class PositionTracker : MonoBehaviour {

	[SerializeField] float trackingDelay = 0f;

	List<Vector3> positionsTracked = new List<Vector3>();

	int lastPositionId = -1;

	void Start () {
		InvokeRepeating("track", 0f, trackingDelay);		
	}

	void track() {
		TelemetryNode playerPosition = new TelemetryNode(
			TelemetryNodeType.Atomic,
			"Player Position",
			transform.position
		);

		if(lastPositionId != -1) {
			playerPosition.setLink(lastPositionId);
		}

		lastPositionId = TelemetryCore.addNode(playerPosition);
	}

	void OnApplicationQuit() {
//         StreamWriter writer = new StreamWriter(Application.persistentDataPath + "/playerPosition2.json", true);
		
// 		Dictionary<string, object> data = new Dictionary<string, object>();

// //================================================================================
// 		dynamic test = new ExpandoObject();
// 		test.damage = 25;
// 		test.target = "enemy";
		
// 		TelemetryNode teste = new TelemetryNode(
// 			TelemetryNodeType.Agent,
// 			"Player",
// 			transform.position,
// 			test
// 		);

// 		Debug.Log(teste.toJson());
// 		DBSaver.savePlayerData(teste.toJson());

// 		TelemetryNode teste2 = new TelemetryNode(
//             TelemetryNodeType.Agent,
//             "Player",
//             transform.position
//         );

//         Debug.Log(teste2.toJson());

		// dynamic test = new ExpandoObject();
		// test.nome = "Allan";
		// test.idade = 25;
		// test.eita = new ExpandoObject();
		// test.eita.cu = 125;
		// test.eita.cu2 = 126;
		// test.arraizu = new List<object>();
		// test.arraizu.Add(125);
		// test.arraizu.Add(125f);
		// test.arraizu.Add("tes");

		// foreach (KeyValuePair<string, object> kvp in test) // enumerating over it exposes the Properties and Values as a KeyValuePair
        //     Debug.Log(kvp.Key + " - " + kvp.Value);

		// string jison = JsonConvert.SerializeObject(test);

		// Debug.Log(jison);

		// dynamic novo = JsonConvert.DeserializeObject<ExpandoObject>(jison);
		// novo.extra = "mais um";

		// Debug.Log(JsonConvert.SerializeObject(novo));

		// JObject teste = new JObject();
		// teste["hehe"] = "oi";
		// JArray arroz = new JArray();
		// arroz.Add("teste1");
		// arroz.Add(123);
		// teste["Array"] = arroz;

		// Debug.Log(JsonConvert.SerializeObject(teste));

		// Dictionary<string, object> testo = new Dictionary<string, object>();
		// testo["test"] = "allan";
		// testo["numero"] = 125f;
		// Dictionary<string, object> testo2 = new Dictionary<string, object>();
		// testo2["test"] = "allan";
		// testo2["numero"] = 125f;
		// testo["array"] = testo2;

		// Debug.Log(JsonConvert.SerializeObject(testo2)); //não funciona bem, o array não serializa
//================================================================================
		// List<PlayerPosition> positions = new List<PlayerPosition>();

		// foreach (Vector3 position in positionsTracked)
		// {
		// 	PlayerPosition playerPos = new PlayerPosition();
		// 	playerPos.init(position);
		// 	positions.Add(playerPos);
		// }

		// data["positions"] = positions;

		// data["teste"] = "teste";

		// writer.Flush();
        // writer.WriteLine(JsonUtility.ToJson(data));
        // writer.Close();
	}
}

[System.Serializable]
public class PlayerPosition {
	public float posX;
	public float posY;
	public float posZ;

	public void init(Vector3 pos) {
		posX = pos.x;
		posY = pos.y;
		posZ = pos.z;
	}

	public Vector3 getVector() {
		return new Vector3(posX, posY, posZ);
	}
}

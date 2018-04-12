using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Telemetry {

	static public int createSingleEvent (string name, Vector3 position, ExpandoObject extraInfo = null) {
		TelemetryNode playerDeath = new TelemetryNode (
			TelemetryNodeType.SingleEvent,
			name,
			position,
			extraInfo
		);

		return TelemetryCore.addNode (playerDeath);
	}
}
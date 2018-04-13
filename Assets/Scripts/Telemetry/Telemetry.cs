using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Telemetry {

	static public int createSingleEvent (string name, Vector3 position, ExpandoObject extraInfo = null) {
		TelemetryNode newSingleEvent = new TelemetryNode (
			TelemetryNodeType.SingleEvent,
			name,
			position,
			extraInfo
		);

		return TelemetryCore.addNode (newSingleEvent);
	}

	static public int createChainEvent (string name, Vector3 position, int previousEventId = -1, ExpandoObject extraInfo = null) {
		TelemetryNode newChainEvent = new TelemetryNode (
			TelemetryNodeType.ChainEvent,
			name,
			position,
			extraInfo
		);

		if(previousEventId != -1)
			newChainEvent.setLink (previousEventId);

		// Telemetry: Save the event node ID
		return TelemetryCore.addNode (newChainEvent);
	}
}
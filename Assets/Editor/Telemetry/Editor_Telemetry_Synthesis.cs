using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Telemetry_Synthesis))]
public class Editor_Telemetry_Synthesis : Editor {

    SerializedProperty loadInfo;
    SerializedProperty read;
    SerializedProperty totalSessions;
    SerializedProperty avgSessionDuration;
    SerializedProperty avgRoundsPerSession;

    void OnEnable () {
        loadInfo = serializedObject.FindProperty ("loadInfo");
        read = serializedObject.FindProperty ("read");
        totalSessions = serializedObject.FindProperty ("totalSessions");
        avgSessionDuration = serializedObject.FindProperty ("avgSessionDuration");
        avgRoundsPerSession = serializedObject.FindProperty ("avgRoundsPerSession");
    }

    public override void OnInspectorGUI () {
        serializedObject.Update ();

        if (GUILayout.Button ("Load Info")) {
            loadInfo.boolValue = true;
        }

        if (GUILayout.Button ("Read")) {
            read.boolValue = true;
        }

        EditorGUILayout.PropertyField (totalSessions);
        EditorGUILayout.PropertyField (avgSessionDuration);
        EditorGUILayout.PropertyField (avgRoundsPerSession);

        serializedObject.ApplyModifiedProperties ();
    }
}
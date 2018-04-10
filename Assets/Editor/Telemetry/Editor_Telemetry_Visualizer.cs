using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (Telemetry_Visualizer))]
public class Editor_Telemetry_Visualizer : Editor {

    SerializedProperty nodesBag;
    SerializedProperty atomicModel;
    SerializedProperty singleEventModel;
    SerializedProperty chainEventModel;
    SerializedProperty detectSceneName;
    SerializedProperty sceneName;
    SerializedProperty loadInfo;
    SerializedProperty draw;
    SerializedProperty roundsCount;
    SerializedProperty selectedRound;

    void OnEnable () {
        nodesBag = serializedObject.FindProperty ("nodesBag");
        atomicModel = serializedObject.FindProperty ("atomicModel");
        singleEventModel = serializedObject.FindProperty ("singleEventModel");
        chainEventModel = serializedObject.FindProperty ("chainEventModel");
        detectSceneName = serializedObject.FindProperty ("detectSceneName");
        sceneName = serializedObject.FindProperty ("sceneName");
        loadInfo = serializedObject.FindProperty ("loadInfo");
        draw = serializedObject.FindProperty ("draw");
        roundsCount = serializedObject.FindProperty ("roundsCount");
        selectedRound = serializedObject.FindProperty ("selectedRound");
    }

    public override void OnInspectorGUI () {
        serializedObject.Update ();

        EditorGUILayout.PropertyField (nodesBag);
        EditorGUILayout.PropertyField (atomicModel);
        EditorGUILayout.PropertyField (singleEventModel);
        EditorGUILayout.PropertyField (chainEventModel);
        EditorGUILayout.PropertyField (detectSceneName);

        if(!detectSceneName.boolValue){
        	EditorGUILayout.PropertyField(sceneName);
        }

        if (GUILayout.Button ("Load Info")) {
            loadInfo.boolValue = true;
        }

        if (roundsCount.intValue > 0) {
            selectedRound.intValue = EditorGUILayout.IntSlider (selectedRound.intValue, 0, roundsCount.intValue);
        }

        if (GUILayout.Button ("Draw Round Nodes")) {
            draw.boolValue = true;
        }

        serializedObject.ApplyModifiedProperties ();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GroupSimulationSettings))]
public class GroupSimulationSettingsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GroupSimulationSettings groupSimulationSettings = (GroupSimulationSettings)target;

        if (GUILayout.Button("Parse Expert Item Rankings", GUILayout.Height(20)))
        {
            groupSimulationSettings.ParseExpertItemRankings();
        }
    }
}

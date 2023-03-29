using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;

#endif

#if UNITY_EDITOR
[CustomEditor(typeof(Objects_Playoff))]
public class PlayoffEditor : Editor {
	public override void OnInspectorGUI() {
		DrawDefaultInspector();
		Objects_Playoff myScript1 = (Objects_Playoff)target;
		if(GUILayout.Button("Set Flags Invisible")) {
			myScript1.SetFlagsInvisible();
		} else if(GUILayout.Button("Set Flags Visible")) {
			myScript1.SetFlagsVisible();
		} else if(GUILayout.Button("Set Names Invisible")) {
			myScript1.SetNamesInvisible();
		} else if(GUILayout.Button("Set Names Visible")) {
			myScript1.SetNamesVisible();
		} else if(GUILayout.Button("Set Scores Invisible")) {
			myScript1.SetScoresInvisible();
		} else if(GUILayout.Button("Set Scores Visible")) {
			myScript1.SetScoresVisible();
		}
	}
}
#endif
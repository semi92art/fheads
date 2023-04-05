using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR

[CustomEditor(typeof(StadiumChooseScript))]
public class StadiumEditor : Editor 
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();
		StadiumChooseScript myScript = (StadiumChooseScript)target;
		if(GUILayout.Button("Next Stadium")) 
			myScript.SetStadiumHandle();
		else if(GUILayout.Button("Previous Stadium"))
			myScript.SetStadiumBackHandle();
		else if(GUILayout.Button("Set Background"))
			myScript.SetBlackBackground();
		else if(GUILayout.Button("Disable Backround"))
			myScript.DisableBlackBackground();
	}
}

#endif


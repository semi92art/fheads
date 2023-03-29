using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(SkyScript))]
public class SkyEditor : Editor
{
	public override void OnInspectorGUI() 
	{
		DrawDefaultInspector();
		SkyScript myScript = (SkyScript)target;

		if(GUILayout.Button("Next Sky")) 
			myScript.SetNextSky();
		else if(GUILayout.Button("Enable/Disable Stadium")) 
			myScript.EnableDisableStadium();
	}
}
#endif


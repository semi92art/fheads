#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class GoogleMobileAdInit  {

	static GoogleMobileAdInit () {

		#if UNITY_IPHONE || UNITY_ANDROID || UNITY_WP8

		if(!GoogleMobileAdSettingsEditor.IsInstalled) {
			EditorApplication.update += OnEditorLoaded;
		} else {
			if(!GoogleMobileAdSettingsEditor.IsUpToDate) {
				EditorApplication.update += OnEditorLoaded;
			}
		}

		#endif
	}

	private static void OnEditorLoaded() {

		EditorApplication.update -= OnEditorLoaded;
		Debug.LogWarning("Google Mobile Ad Plugin Install Required. Opening Plugin settings...");
		Selection.activeObject = GoogleMobileAdSettings.Instance;
	}

}
#endif

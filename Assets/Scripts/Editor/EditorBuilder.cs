using System;
using UnityEditor;
using UnityEngine;

public class EditorBuilder : EditorWindow
{
    private readonly string[] m_PlatformPopupOptions = {"Android", "iOS"};
    private int m_PlatformPopupIndex;
    
    [MenuItem("Tools/Builder")]
    public static void ShowWindow()
    {
        GetWindow<EditorBuilder>("EditorBuilder");
    }

    private void OnGUI()
    {
        //Platform (Android/iOS) selection
        m_PlatformPopupIndex = EditorGUILayout.Popup(m_PlatformPopupIndex, m_PlatformPopupOptions);
        if (GUILayout.Button("Set"))
            PlatformSelector();
        if (GUILayout.Button("Developer Build"))
            LoadDeveloperBuild();
        if (GUILayout.Button("Release Build"))
            LoadReleaseBuild();
        
    }

    private void PlatformSelector()
    {
        switch (m_PlatformPopupIndex)
        {
            case 0:
                Debug.Log("Android");
                break;
            case 1:
                Debug.Log("iOS");
                break;
        }
    }

    private void LoadDeveloperBuild()
    {
        Debug.Log("Loading Developer Build");
    }

    private void LoadReleaseBuild()
    {
        Debug.Log("Loading Release Build");
    }
}

using System;
using UnityEditor;
using UnityEngine;

public class EditorBuilder : EditorWindow
{
    //popup options
    private string[] m_PlatformPopupOptions = new string[] {"Android", "iOS"};

    private int m_PlatformPopupIndex = 0;
    
    //Editor Window with building options
    [MenuItem("Tools/EditorBuilder")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorBuilder>("EditorBuilder");
    }

    void OnGUI()
    {
        //Platform (Android/iOS) selection
        m_PlatformPopupIndex = EditorGUILayout.Popup(m_PlatformPopupIndex, m_PlatformPopupOptions);
        if (GUILayout.Button("Set"))
        {
            PlatformSelector();
        }
        
        //Developer Build
        if (GUILayout.Button("Developer Build"))
        {
            LoadDeveloperBuild();
        }
        
        //Release Build
        if (GUILayout.Button("Release Build"))
        {
            LoadReleaseBuild();
        }
    }

    void PlatformSelector()
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

    void LoadDeveloperBuild()
    {
        //Loads project in debug mode with console and logging
        Debug.Log("Loading Developer Build");
    }

    void LoadReleaseBuild()
    {
        //Loads project in release mode without debug tools
        Debug.Log("Loading Release Build");
    }
}

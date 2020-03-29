using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MenuUI))]
public class MenuEditor : Editor
{
    private MenuUI m_MenuUi;

    private void OnEnable()
    {
        m_MenuUi = target as MenuUI;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Canvas"))
            m_MenuUi.CreateCanvas();

        if (GUILayout.Button("Create Play 1 on 1"))
            m_MenuUi.CreatePlayOneOnOne();
        
        
        if (GUILayout.Button("Clear all"))
        {
            GameObject canvasObj = GameObject.Find("canvas");
            while (canvasObj != null)
            {
                DestroyImmediate(canvasObj);
                canvasObj = GameObject.Find("canvas");
            }
        }
    }
}

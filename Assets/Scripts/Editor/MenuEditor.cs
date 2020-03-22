using System;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Menu))]
public class MenuEditor : Editor
{
    private Menu m_Menu;

    private void OnEnable()
    {
        m_Menu = target as Menu;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Create Canvas"))
            m_Menu.CreateCanvas();
        
        if (GUILayout.Button("Create Background"))
            m_Menu.CreateBackground();

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

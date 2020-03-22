using System;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(RectTranshormHelper))]
public class RectTransformEditor : Editor
{
    private RectTranshormHelper m_RectTranshormHelper;

    private void OnEnable()
    {
        m_RectTranshormHelper = target as RectTranshormHelper;
    }

    public override void OnInspectorGUI()
    {
        RectTransform rectTransform = m_RectTranshormHelper.GetComponent<RectTransform>();
        rectTransform.anchorMin = EditorGUILayout.Vector2Field("Anchor Min", rectTransform.anchorMin);
        rectTransform.anchorMax = EditorGUILayout.Vector2Field("Anchor Max", rectTransform.anchorMax);
        rectTransform.anchoredPosition = EditorGUILayout.Vector2Field("Anchored Position", rectTransform.anchoredPosition);
        rectTransform.pivot = EditorGUILayout.Vector2Field("Pivot", rectTransform.pivot);
        rectTransform.sizeDelta = EditorGUILayout.Vector2Field("Size Delta", rectTransform.sizeDelta);
    }
}


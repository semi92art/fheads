using UnityEditor;
using UnityEngine;

public class EditorHelper : EditorWindow
{
    [MenuItem("Tools/EditorHelper")]
    public static void ShowWindow()
    {
        GetWindow<EditorHelper>("EditorHelper");
    }
    
    void OnGUI()
    {
        if (GUILayout.Button("Clear PlayerPrefs"))
        {
            PlayerPrefs.DeleteAll();
            PrefsManager.Instance.MoneyCount = 0;
            Debug.Log("PlayerPrefs cleared");
        }

        if (GUILayout.Button("Add 10000$"))
        {
            PrefsManager.Instance.MoneyCount += 10000;
            Debug.Log("10000 added");
        }

        if (GUILayout.Button("Add 30% of current"))
        {
            PrefsManager.Instance.MoneyCount += Mathf.RoundToInt(0.3f * PrefsManager.Instance.MoneyCount);
            Debug.Log("30% added");
        }

        if (GUILayout.Button("Daily reward"))
        {
            GameObject scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.allAw.CallAwardPanel_1();
            Debug.Log("Daily reward");
        }
    }
}

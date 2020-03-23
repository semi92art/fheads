using UnityEditor;
using UnityEngine;

public class EditorHelper : EditorWindow
{
    public int moneyCount;
    
    [MenuItem("Tools/EditorHelper")]
    
    
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorHelper>("EditorHelper");
        
    }
    void OnGUI()
    {
        if (GUILayout.Button("Clear PlayerPrefs"))
        {
            GameObject scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            PlayerPrefs.DeleteAll();
            scr.alPrScr.moneyCount = 0;
            scr.alPrScr.setMoney = true;
            Debug.Log("PlayerPrefs cleared");
        }

        if (GUILayout.Button("Add 10000$"))
        {
            GameObject scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.alPrScr.moneyCount += 10000;
            scr.alPrScr.setMoney = true;
            Debug.Log("10000 added");
        }

        if (GUILayout.Button("Add 30% of current"))
        {
            GameObject scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.alPrScr.moneyCount += Mathf.RoundToInt(0.3f * scr.alPrScr.moneyCount);
            scr.alPrScr.setMoney = true;
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

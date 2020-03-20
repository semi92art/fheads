using UnityEditor;
using UnityEngine;

public class DebugUtility : EditorWindow
{
    public int moneyCount;
    
    [MenuItem("Window/DebugUtility")]
    
    
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<DebugUtility>("DebugUtility");
        
    }
    void OnGUI()
    {
        if (GUILayout.Button("Clear PlayerPrefs"))
        {
            var scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            PlayerPrefs.DeleteAll();
            scr.alPrScr.moneyCount = 0;
            scr.alPrScr.setMoney = true;
            Debug.Log("PlayerPrefs cleared");
        }

        if (GUILayout.Button("Add 10000$"))
        {
            var scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.alPrScr.moneyCount += 10000;
            scr.alPrScr.setMoney = true;
            Debug.Log("10000 added");
        }

        if (GUILayout.Button("Add 30% of current"))
        {
            var scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.alPrScr.moneyCount += Mathf.RoundToInt(0.3f * scr.alPrScr.moneyCount);
            scr.alPrScr.setMoney = true;
            Debug.Log("30% added");
        }

        if (GUILayout.Button("Daily reward"))
        {
            //AllPrefsScript.SetPrefsInt("EveryDayReward", 1000);
            var scrObj = GameObject.Find("Scripts");
            Scripts scr = scrObj.GetComponent<Scripts>();
            scr.allAw.CallAwardPanel_1();
            Debug.Log("Daily reward");
        }
    }
}

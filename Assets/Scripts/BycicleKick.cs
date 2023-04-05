using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BycicleKick : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;

    public int startGoals;
    public GameObject obj_BK;
    public Text text_BK, text_BK_0;

    private int totalGoals;
    private bool isBK_Opened;

    void Awake()
    {
        totalGoals = PlayerPrefs.GetInt("TotalGoals");
        isBK_Opened = totalGoals >= startGoals ? true : false;

        if (isBK_Opened)
        {
            PlayerPrefs.SetInt("IsBK_Opened", 1);
            obj_BK.SetActive(false);
        }
        else
        {
            int g0 = startGoals - totalGoals;
            text_BK_0.text = g0.ToString();
            text_BK.text = "goals to\nunlock\nbycicle kick!";
        }
    }
}

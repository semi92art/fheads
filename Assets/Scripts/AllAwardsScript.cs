using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AllAwardsScript : MonoBehaviour 
{
    public Scripts scr;
    [Space(5)]
    public Animator anim_AllAwPan;
    public GameObject obj_Ramka;
    public Image im_lang;
    public GameObject allAwPan;
    public GameObject[] awPans;
    public Text aw0CountText;

    [Header("Preview Objects:")]
    public GameObject yesButObj;
    public Text noButTxt;
    public Text prevText;

    private int[] pansActive;


    void Awake()
    {
        pansActive = new int[awPans.Length];

        for (int i = 0; i < pansActive.Length; i++)
            pansActive[i] = 0;

        obj_Ramka.SetActive(true);

        for (int i = 0; i < awPans.Length; i++)
            awPans[i].SetActive(false);

    }

    public void CloseAllAwardsPanel(int awPan)
    {
        //obj_Ramka.SetActive(true);
        int int_close = 0;

        for (int i = 0; i < pansActive.Length; i++)
        {
            if (pansActive[i] == 1)
                int_close++;
        }

        if (int_close > 1)
            awPans[awPan].SetActive(false);
        else
            anim_AllAwPan.SetTrigger(Animator.StringToHash("0"));

        pansActive[awPan] = 0;
    }

    public void CloseAllAwardsPanel_0()
    {
        for (int i = 0; i < awPans.Length; i++)
            awPans[i].SetActive(false);

        allAwPan.SetActive(false);
    }

    /// <summary>
    /// Everyday Reward
    /// </summary>
    public void CallAwardPanel_1()
    {
        allAwPan.SetActive(true);
        awPans[0].SetActive(true);
        pansActive[0] = 1;
        //aw1MainText.alignment = TextAnchor.LowerCenter;
        aw0CountText.enabled = true;
        aw0CountText.text = scr.univFunc.Money (scr.everyDayReward.reward);
    }

    /// <summary>
    /// Preview message.
    /// </summary>
    public void CallAwardPanel_2()
    {
        allAwPan.SetActive(true);
        awPans[1].SetActive(true);
        pansActive[1] = 1;

        if (GameManager.Instance._menues == Menues.MenuCareer)
        {
            if (PrefsManager.Instance.MoneyCount >= scr.carMng.lg_cost[scr.carMng._lgPrev])
            {
                yesButObj.SetActive(true);
                prevText.text = "Do you want to unlock this league for " +
                    scr.univFunc.Money(scr.carMng.lg_cost[scr.carMng._lgPrev]);
            }
            else
            {
                yesButObj.SetActive(false);
                prevText.text = "You have not enough money to unlock this league. " +
                    "You need " + scr.univFunc.Money(scr.carMng.lg_cost[scr.carMng._lgPrev]);
            }
        }
    }

    /// <summary>
    /// Call review panel.
    /// </summary>
    public void CallAwardPanel_3()
    {
        allAwPan.SetActive(true);
        awPans[2].SetActive(true);
        pansActive[2] = 1;
    }

    public void YesButton_Preview()
    {
        switch (GameManager.Instance._menues)
        {
            case Menues.MenuPlayers:
                scr.prMng.Unlock();
                scr.prMng.SetSkillsAndSprite();
                scr.prMng.SetShowcase();
                scr.allAw.CloseAllAwardsPanel(1);
                break;
            case Menues.MenuCareer:
                scr.carMng.UnlockLeague(scr.carMng._lgPrev);
                scr.allAw.CloseAllAwardsPanel(1);
                break;
        }
    }
}

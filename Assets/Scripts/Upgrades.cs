using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BallUpgrade
{
    public RectTransform _rectTr;
    //public Sprite icon;
    public int price;
    public string description;
}


public class Upgrades : MonoBehaviour
{
    public Scripts scr;

    [Header("Upgrade Name:")]
    public Text text_UpgradeName;
    public string[] upgrNames;
    [Header("Upgrade Description:")]
    public Text text_UpgrDescription;
    public string[] upgrDescriptions;
    [Space(5)]
    public RectTransform[] upgrButtons;
    [Space(5)]
    public Image im_UpgradeBig;
    [Header("Upgrade Sprites:")]
    public Sprite[] spr_Upgrades;
    [Header("Sticks:")]
    public Image[] im_Sticks;
    [Space(5)]
    public Text text_Percent;
    public Text text_BuyButton;
    public Text text_RefundButton;
    public Button but_Buy;
    public Button but_Refund;
    public Color col_Stick0, col_Stick1;
    [Header("Upgrade Prices:")]
    public int[] prices_Speed;
    [Header("Balls:")]
    public List<BallUpgrade> _ballUpgr;

    [Header("Balls Panel Objects:")]
    public GameObject[] obj_BallsPan;
    [Header("Not Balls Panel Objects:")]
    public GameObject[] obj_NotBallsPan;

    [Header("Kick Panel Objects:")]
    public RectTransform rect_K_But;
    public RectTransform rect_BK_But;
    public string str_BK;
    public int bkPrice;

    [HideInInspector]
    public int curr_ind;
    [HideInInspector]
    public int curr_indBall;


    public void Buy()
    {
        scr.objM.Button_Sound();

        switch (curr_ind)
        {
            case 0: //Speed
                PrefsManager.Instance.MoneyCount -= prices_Speed[PrefsManager.Instance.UpgradeSpeed++];
                break;
            case 1: //Kick
                if (!isBK_Chosen)
                    PrefsManager.Instance.MoneyCount -= prices_Speed[PrefsManager.Instance.UpgradeKick++];
                else
                {
                    PrefsManager.Instance.BycicleKickEnabled = true;
                    PrefsManager.Instance.MoneyCount -= bkPrice;
                }
                break;
            case 2: //Jump
                PrefsManager.Instance.MoneyCount -= prices_Speed[PrefsManager.Instance.UpgradeJump++];
                break;
            case 4: //Shields
                PrefsManager.Instance.MoneyCount -= prices_Speed[PrefsManager.Instance.UpgradeShield++];
                break;
            case 6: //Money
                PrefsManager.Instance.MoneyCount -= prices_Speed[PrefsManager.Instance.UpgradeMoneyIncome++];
                break;
        }

        if (curr_ind == 1 && isBK_Chosen)
            Set_KickPanel(1);
        else
            Upgrade_Choose(curr_ind);
    }

    public void Refund()
    {
        scr.objM.Button_Sound();

        switch (curr_ind)
        {
            case 0: //Speed
                PrefsManager.Instance.MoneyCount += prices_Speed[--PrefsManager.Instance.UpgradeSpeed];
                break;
            case 1: //Kick
                if (!isBK_Chosen)
                {
                    PrefsManager.Instance.MoneyCount += prices_Speed[--PrefsManager.Instance.UpgradeKick];
                }
                else
                {
                    PrefsManager.Instance.BycicleKickEnabled = false;
                    PrefsManager.Instance.MoneyCount += bkPrice;
                }
                break;
            case 2: //Jump
                PrefsManager.Instance.MoneyCount += prices_Speed[--PrefsManager.Instance.UpgradeJump];
                break;
            case 4: //Shields
                PrefsManager.Instance.MoneyCount += prices_Speed[--PrefsManager.Instance.UpgradeShield];
                break;
            case 6: //Money
                PrefsManager.Instance.MoneyCount += prices_Speed[--PrefsManager.Instance.UpgradeMoneyIncome];
                break;
        }
                
        if (curr_ind == 1 && isBK_Chosen)
            Set_KickPanel(1);
        else
            Upgrade_Choose(curr_ind);
    }

    public void Upgrade_Choose(int _ind)
    {
        scr.objM.Button_Sound();

        scr.topPanMng.moneyText.text = 
            scr.univFunc.Money(PrefsManager.Instance.MoneyCount);

        curr_ind = _ind;
        text_UpgradeName.text = upgrNames[_ind];
        text_UpgrDescription.text = upgrDescriptions[_ind];
        im_UpgradeBig.sprite = spr_Upgrades[_ind];

        if (_ind == 5)
        {
            text_Percent.alignment = TextAnchor.UpperLeft;
            Enable_BallPanel();
        }
        else
        {
            text_Percent.alignment = TextAnchor.LowerLeft;
            Disable_BallPanel();

            for (int i = 0; i < im_Sticks.Length; i++)
                im_Sticks[i].gameObject.SetActive(true);

            if (_ind == 1)
            {
                im_UpgradeBig.gameObject.SetActive(false);
                rect_K_But.gameObject.SetActive(true);
                rect_BK_But.gameObject.SetActive(true);
                rect_K_But.sizeDelta = new Vector2(200, 200);
                rect_BK_But.sizeDelta = new Vector2(170, 170);
            }
            else
            {
                im_UpgradeBig.gameObject.SetActive(true);
                rect_K_But.gameObject.SetActive(false);
                rect_BK_But.gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < upgrButtons.Length; i++)
            upgrButtons[i].sizeDelta = new Vector2(110, 110);

        upgrButtons[_ind].sizeDelta = new Vector2(140, 140);

        int sticks = 0;
        int perc = 0;

        switch (_ind)
        {
            case 0: //Speed
                sticks = PrefsManager.Instance.UpgradeSpeed;

                if (sticks < 20)
                {
                    if (sticks == 0)
                    {
                        text_RefundButton.color = col_Stick1;
                        but_Refund.interactable = false;
                    }
                    else
                    {
                        text_RefundButton.color = col_Stick0;
                        but_Refund.interactable = true;
                    }

                    text_BuyButton.text = "Upgrade: " +
                        scr.univFunc.Money(prices_Speed[sticks]);

                    if (PrefsManager.Instance.MoneyCount > prices_Speed[sticks])
                    {
                        text_BuyButton.color = col_Stick0;
                        but_Buy.interactable = true;
                    }
                    else
                    {
                        text_BuyButton.color = col_Stick1;
                        but_Buy.interactable = false;
                    }
                }
                else
                {
                    text_BuyButton.text = "Maxed";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].color = i < sticks ? col_Stick0 : col_Stick1;

                perc = 100 + sticks * 2;
                text_Percent.text = "Speed: " + perc.ToString() + "%";
                break;
            case 1: //Kick
                isBK_Chosen = false;
                sticks = PrefsManager.Instance.UpgradeKick;

                if (sticks < 20)
                {
                    if (sticks == 0)
                    {
                        text_RefundButton.color = col_Stick1;
                        but_Refund.interactable = false;
                    }
                    else
                    {
                        text_RefundButton.color = col_Stick0;
                        but_Refund.interactable = true;
                    }

                    text_BuyButton.text = "Upgrade: " +
                        scr.univFunc.Money(prices_Speed[sticks]);

                    if (PrefsManager.Instance.MoneyCount > prices_Speed[sticks])
                    {
                        text_BuyButton.color = col_Stick0;
                        but_Buy.interactable = true;
                    }
                    else
                    {
                        text_BuyButton.color = col_Stick1;
                        but_Buy.interactable = false;
                    }
                }
                else
                {
                    text_BuyButton.text = "Maxed";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].color = i < sticks ? col_Stick0 : col_Stick1;

                perc = 100 + sticks * 2;
                text_Percent.text = "Kick: " + perc.ToString() + "%";
                break;
            case 2: //Jump
                sticks = PrefsManager.Instance.UpgradeJump;

                if (sticks < 20)
                {
                    if (sticks == 0)
                    {
                        text_RefundButton.color = col_Stick1;
                        but_Refund.interactable = false;
                    }
                    else
                    {
                        text_RefundButton.color = col_Stick0;
                        but_Refund.interactable = true;
                    }

                    text_BuyButton.text = "Upgrade: " +
                        scr.univFunc.Money(prices_Speed[sticks]);

                    if (PrefsManager.Instance.MoneyCount > prices_Speed[sticks])
                    {
                        text_BuyButton.color = col_Stick0;
                        but_Buy.interactable = true;
                    }
                    else
                    {
                        text_BuyButton.color = col_Stick1;
                        but_Buy.interactable = false;
                    }
                }
                else
                {
                    text_BuyButton.text = "Maxed";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].color = i < sticks ? col_Stick0 : col_Stick1;

                perc = 100 + sticks * 2;
                text_Percent.text = "Jump: " + perc.ToString() + "%";
                break;
            
            case 4: //Shields
                sticks = PrefsManager.Instance.UpgradeShield;

                if (sticks < 20)
                {
                    if (sticks == 0)
                    {
                        text_RefundButton.color = col_Stick1;
                        but_Refund.interactable = false;
                    }
                    else
                    {
                        text_RefundButton.color = col_Stick0;
                        but_Refund.interactable = true;
                    }

                    text_BuyButton.text = "Upgrade: " +
                        scr.univFunc.Money(prices_Speed[sticks]);

                    if (PrefsManager.Instance.MoneyCount > prices_Speed[sticks])
                    {
                        text_BuyButton.color = col_Stick0;
                        but_Buy.interactable = true;
                    }
                    else
                    {
                        text_BuyButton.color = col_Stick1;
                        but_Buy.interactable = false;
                    }
                }
                else
                {
                    text_BuyButton.text = "Maxed";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].color = i < sticks ? col_Stick0 : col_Stick1;

                perc = 0 + sticks * 5;
                text_Percent.text = "Shields: " + perc.ToString() + "%";
                break;
            case 5: //Balls
                Ball_Choose(curr_indBall);
                break;
            case 6: //Money
                sticks = PrefsManager.Instance.UpgradeMoneyIncome;

                if (sticks < 20)
                {
                    if (sticks == 0)
                    {
                        text_RefundButton.color = col_Stick1;
                        but_Refund.interactable = false;
                    }
                    else
                    {
                        text_RefundButton.color = col_Stick0;
                        but_Refund.interactable = true;
                    }

                    text_BuyButton.text = "Upgrade: " +
                        scr.univFunc.Money(prices_Speed[sticks]);

                    if (PrefsManager.Instance.MoneyCount > prices_Speed[sticks])
                    {
                        text_BuyButton.color = col_Stick0;
                        but_Buy.interactable = true;
                    }
                    else
                    {
                        text_BuyButton.color = col_Stick1;
                        but_Buy.interactable = false;
                    }
                }
                else
                {
                    text_BuyButton.text = "Maxed";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].color = i < sticks ? col_Stick0 : col_Stick1;

                perc = 100 + sticks * 5;
                text_Percent.text = "Income: " + perc.ToString() + "%";
                break;
        }
    }

    public void Ball_Choose(int _ind)
    {
        curr_indBall = _ind;

        for (int i = 0; i < _ballUpgr.Count; i++)
            _ballUpgr[i]._rectTr.sizeDelta = new Vector2(140, 140);

        _ballUpgr[_ind]._rectTr.sizeDelta = new Vector2(170, 170);
        text_Percent.text = _ballUpgr[_ind].description;


        text_BuyButton.text = "Already got";
        text_BuyButton.color = col_Stick1;
        but_Buy.interactable = false;
        text_RefundButton.color = _ind == 0 ? col_Stick1 : col_Stick0;
        but_Refund.interactable = _ind != 0;

    }

    private bool isBK_Chosen;

    public void Set_KickPanel(int _ind)
    {
        switch (_ind)
        {
            case 0: // Kick Button
                Upgrade_Choose(1);
                break;
            case 1: // Becicke Kick Button
                scr.objM.Button_Sound();

                isBK_Chosen = true;

                rect_K_But.sizeDelta = new Vector2(170, 170);
                rect_BK_But.sizeDelta = new Vector2(200, 200);
                text_UpgrDescription.text = "Bycicle kick.";
                text_Percent.alignment = TextAnchor.UpperLeft;
                text_Percent.text = str_BK;
                
                if (PrefsManager.Instance.BycicleKickEnabled)
                {
                    text_BuyButton.text = "Already got";
                    text_BuyButton.color = col_Stick1;
                    but_Buy.interactable = false;
                    text_RefundButton.color = col_Stick0;
                    but_Refund.interactable = true;
                }
                else
                {
                    text_BuyButton.text = "Buy: " + scr.univFunc.Money(bkPrice);
                    text_BuyButton.color = col_Stick0;
                    but_Buy.interactable = true;
                    text_RefundButton.color = col_Stick1;
                    but_Refund.interactable = false;
                }

                for (int i = 0; i < im_Sticks.Length; i++)
                    im_Sticks[i].gameObject.SetActive(false);
                break;
        }
    }

    public void Enable_BallPanel()
    {
        for (int i = 0; i < obj_BallsPan.Length; i++)
            obj_BallsPan[i].SetActive(true);

        for (int i = 0; i < obj_NotBallsPan.Length; i++)
            obj_NotBallsPan[i].SetActive(false);
    }

    public void Disable_BallPanel()
    {
        for (int i = 0; i < obj_BallsPan.Length; i++)
            obj_BallsPan[i].SetActive(false);

        for (int i = 0; i < obj_NotBallsPan.Length; i++)
            obj_NotBallsPan[i].SetActive(true);
    }


}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class MatchBonus
{
    public string[] names;
    public int award;
}

public class MoneyWinScript : MonoBehaviour 
{
    [SerializeField]
    private Scripts scr;

    public GameObject congrPan;
    public int defaultPrice;
    public int repeatTournPrice;
    public int freePlayDefaultPrice;
    public Text moneyBankText;
    public GameObject sampleBonus;
    public RectTransform contentPanel;

    public List<MatchBonus> bonusList;

    public bool isMoneyToBank;
    private bool updatesChecked;
    public int totalPrice;

    private float totalPrice0;
    private float timer;
    private int bankMoney;
    private bool advertiseShown;


    void Awake()
    {
        if (!scr.objLev.isMoneyWinPopulate)
        {
            congrPan.SetActive (true);
            moneyBankText.text = scr.univFunc.moneyString(bankMoney = PrefsManager.Instance.MoneyCount);
            DestroyEditorButtons ();
            congrPan.SetActive (false);
        }
    }

    private void DestroyEditorButtons()
    {
        GameObject[] bonusObj = GameObject.FindGameObjectsWithTag ("SampleBonus");

        for (int i = 0; i < bonusObj.Length; i++)
            DestroyImmediate (bonusObj [i]);
    }

    void Update()
    {
        if (isMoneyToBank && updatesChecked)
        {
            timer += Time.deltaTime;

            if (timer <= Time.deltaTime && timer > float.Epsilon)
            {
                PrefsManager.Instance.MoneyCount += totalPrice;
            } 
            else if (timer >= 0.8f)
            {
                if (totalPrice > 0)
                {
                    if (timer >= 1 && timer < 1 + Time.deltaTime)
                    {
                        if (scr.levAudScr.moneyWinSource.enabled)
                            scr.levAudScr.moneyWinSource.Play();

                        scr.levAudScr.moneyWinSource1.PlayDelayed(0.2f);
                    }

                    if (totalPrice > 10000)
                    {
                        totalPrice -= 1000;
                        bankMoney += 1000;
                    } 
                    else if (totalPrice > 1000)
                    {
                        totalPrice -= 100;
                        bankMoney += 100;
                    } 
                    else if (totalPrice <= 1000 && totalPrice > 10)
                    {
                        totalPrice -= 10;
                        bankMoney += 10;
                    }
                    else
                    {
                        totalPrice -= 1;
                        bankMoney += 1;
                    }

                    moneyBankText.text = scr.univFunc.moneyString(bankMoney);
                } 
                else
                {
                    if (scr.levAudScr.moneyWinSource.isPlaying)
                    {
                        scr.levAudScr.moneyWinSource.Stop();
                        scr.levAudScr.moneyWinSource1.Stop();
                        isMoneyToBank = false;
                    }

                    /*if (!advertiseShown)
                    {
                        scr.univFunc.ShowInterstitialAd();
                        advertiseShown = true;
                    }*/
                }
            }
        }
    }

    public void SetMoneyWin()
    {
        moneyBankText.gameObject.SetActive(true);
        isMoneyToBank = true;
        Time.timeScale = 1;
    }
}

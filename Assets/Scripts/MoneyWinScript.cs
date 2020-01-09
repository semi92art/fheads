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
            moneyBankText.text = scr.univFunc.moneyString(scr.alPrScr.moneyCount);
            bankMoney = scr.alPrScr.moneyCount;
            DestroyEditorButtons ();
            //PopulateList ();

            congrPan.SetActive (false);
        }
    }

    private void DestroyEditorButtons()
    {
        GameObject[] bonusObj = GameObject.FindGameObjectsWithTag ("SampleBonus");

        for (int i = 0; i < bonusObj.Length; i++)
            DestroyImmediate (bonusObj [i]);
    }

    /*private void PopulateList()
    {
        foreach (var item in bonusList)
        {
            GameObject bonusObj = Instantiate(sampleBonus) as GameObject;
            SampleBonus bonus = bonusObj.GetComponent<SampleBonus> ();
            bonus.award = item.award;
            bonus.awardText.text = scr.univFunc.moneyString (item.award);
            bonus.name0 = item.names[0];

            bonus._name.text = item.names[scr.univFunc.sysLang()];

            if (scr.univFunc.sysLang() == 1 || scr.univFunc.sysLang() == 2 || scr.univFunc.sysLang() == 3)
                bonus._name.font = scr.langScr.font_Second;

            bonusObj.transform.SetParent (contentPanel);
        }
            
        scr.objLev.isMoneyWinPopulate = true;
    }*/

    /*public void TournamentGameResult()
    {
        if (scr.alPrScr.isRandGame == 0)
        {
            int lg = scr.alPrScr.lg;
            int game = scr.alPrScr.game;
            int lastInd;

            if (TimeManager.resOfGame == 1)
            {
                scr.alPrScr.wonGames[game, lg] = 1;
                scr.alPrScr.doCh = true;
            }
        }
    }

    public void CheckForBonuses()
    {
        GameObject[] bonusObj = GameObject.FindGameObjectsWithTag ("SampleBonus");
        SampleBonus[] sampBonus = new SampleBonus[bonusObj.Length];

        for (int i = 0; i < bonusObj.Length; i++) 
            sampBonus [i] = bonusObj [i].GetComponent<SampleBonus> ();

        // Disable bonuses, whitch were not got in this game.
        for (int i = 0; i < sampBonus.Length; i++) 
        {
            if (sampBonus [i].name0 == "award")
            {
                if (TimeManager.resOfGame == 1 && scr.alPrScr.isRandGame == 0) 
                {
                    sampBonus [i].award = scr.alPrScr.award;
                    sampBonus [i].awardText.text = scr.univFunc.moneyString(sampBonus [i].award);
                }
                else
                {
                    sampBonus [i].award = 0;
                    bonusObj [i].SetActive (false);         
                }
            }
            else if (sampBonus [i].name0 == "win in match")
            {
                if (TimeManager.resOfGame != 1) 
                {
                    sampBonus [i].award = 0;
                    bonusObj [i].SetActive (false);         
                }
            }
            else if (sampBonus [i].name0 == "clean match")
            {
                if (Score.score1 != 0)
                {
                    sampBonus [i].award = 0;
                    bonusObj [i].SetActive (false); 
                }
            }
        }

        for (int i = 0; i < sampBonus.Length; i++) 
        {
            if (sampBonus [i].name0 == "total")
            {
                for (int j = 0; j < sampBonus.Length; j++)
                {
                    if (i != j && bonusObj[j].activeSelf)
                        sampBonus [i].award += sampBonus [j].award;
                }

                scr.objLev.totalPrice = sampBonus [i].award;
                totalPrice = scr.objLev.totalPrice;
                //Debug.Log ("totalPrice = " + scr.objLev.totalPrice);
                sampBonus [i].awardText.text = scr.univFunc.moneyString (sampBonus [i].award);
            }
        }

        updatesChecked = true;
    }*/

    void Update()
    {
        if (isMoneyToBank && updatesChecked)
        {
            timer += Time.deltaTime;

            if (timer <= Time.deltaTime && timer != 0)
            {
                scr.alPrScr.moneyCount += totalPrice;

                if (scr.alPrScr.moneyCount > 100000000)
                    scr.alPrScr.moneyCount = 100000000;

                scr.alPrScr.setMoney = true;
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

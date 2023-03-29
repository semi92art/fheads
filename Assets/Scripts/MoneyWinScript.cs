using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


[System.Serializable]
public class MatchBonus
{
	public string meaning;
	public string name;
	public string nameRus;
	public int award;
}

public class MoneyWinScript : MonoBehaviour 
{
	public Scripts scr;

	public GameObject menuButton;
	public GameObject congrPan;
	public int defaultPrice;
	public int repeatTournPrice;
	public int freePlayDefaultPrice;
	public Text moneyBankText;
	public float skill;
	public GameObject bank;
	public GameObject sampleBonus;
	public RectTransform contentPanel;

	public List<MatchBonus> bonusList;

	public int totalPrice;
	private float totalPrice0;
	public bool isMoneyToBank;
	private float timer;
	private int bankMoney;

	void Awake()
	{
		if (!scr.objLev.isMoneyWinPopulate)
		{
			congrPan.SetActive (true);
			menuButton.SetActive(false);
			moneyBankText.text = scr.gM.moneyString(scr.alPrScr.moneyCount);
			GetEnemySkill();
			bankMoney = scr.alPrScr.moneyCount;
			DestroyEditorButtons ();
			PopulateList ();

			congrPan.SetActive (false);
		}
	}

	private void DestroyEditorButtons()
	{
		GameObject[] bonusObj = GameObject.FindGameObjectsWithTag ("SampleBonus");

		for (int i = 0; i < bonusObj.Length; i++)
			DestroyImmediate (bonusObj [i]);
	}

	private void PopulateList()
	{
		foreach (var item in bonusList)
		{
			Debug.Log ("Instantiated");
			GameObject bonusObj = Instantiate(sampleBonus) as GameObject;
			SampleBonus bonus = bonusObj.GetComponent<SampleBonus> ();
			bonus.meaning = item.meaning;
			bonus.award = item.award;
			bonus.awardText.text = scr.gM.moneyString (item.award);

			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				bonus._name.text = item.nameRus;
			else
				bonus._name.text = item.name;

			bonusObj.transform.SetParent (contentPanel);
		}

		GameObject[] bonusObj1 = GameObject.FindGameObjectsWithTag ("SampleBonus");
		Debug.Log("bonusObj.length = " + bonusObj1.Length);
		scr.objLev.isMoneyWinPopulate = true;
	}

	public void CheckForBonuses()
	{
		GameObject[] bonusObj = GameObject.FindGameObjectsWithTag ("SampleBonus");
		SampleBonus[] sampBonus = new SampleBonus[bonusObj.Length];

		for (int i = 0; i < bonusObj.Length; i++) 
			sampBonus [i] = bonusObj [i].GetComponent<SampleBonus> ();

		for (int i = 0; i < sampBonus.Length; i++) 
		{
			if (Score.score > Score.score1) 
			{
				if (sampBonus [i].meaning == "tie")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}

			}
			else if (Score.score == Score.score1)
			{
				if (sampBonus [i].meaning == "tie")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}

				if (sampBonus [i].meaning == "win")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}
			}
			else
			{
				if (sampBonus [i].meaning == "tie")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}

				if (sampBonus [i].meaning == "win")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}
			}

			if (!scr.tM.isGoldenGoal) 
			{
				if (sampBonus [i].meaning == "golden goal")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);			
				}
			}

			if (Score.score1 != 0)
			{
				if (sampBonus [i].meaning == "clean")
				{
					sampBonus [i].award = 0;
					bonusObj [i].SetActive (false);	
				}
			}

			if (sampBonus [i].meaning == "goals") 
			{
				int oneGoalAward = sampBonus [i].award;
				sampBonus [i].award = Score.score * oneGoalAward;
				sampBonus [i].awardText.text = scr.gM.moneyString (sampBonus [i].award);

				string nameStr = sampBonus [i]._name.text;
				string goals1 = Score.score.ToString ();

				if (nameStr.Contains("#"))
				{
					Debug.Log ("goals1 = " + goals1);
					Debug.Log ("nameStr = " + nameStr);
					string nameStr1 = nameStr.Replace ("#", goals1);
					Debug.Log ("nameStr = " + nameStr1);
					sampBonus [i]._name.text = nameStr1;
				}

			}

			if (sampBonus [i].meaning == "skill") 
			{
				float award1 = 0.0f;

				if (Score.score > Score.score1) 
				{
					if (scr.alPrScr.freePlay == 1)
						award1 = (float)sampBonus [i].award * (1 + (float)scr.alPrScr.enemyIndexFP / 30);
					else
						award1 = (float)sampBonus [i].award * (1 + (float)scr.alPrScr.enemyIndex / 30);

					sampBonus [i].award = Mathf.CeilToInt(award1);
					sampBonus [i].awardText.text = scr.gM.moneyString (sampBonus [i].award);
				}
				else
					bonusObj [i].SetActive (false);
			}

			if (sampBonus [i].meaning == "stage") 
			{
				bonusObj [i].SetActive (false);	
			}

			if (sampBonus [i].meaning == "shots") 
			{
				bonusObj [i].SetActive (false);	
			}
		}

		for (int i = 0; i < sampBonus.Length; i++) 
		{
			if (sampBonus [i].meaning == "total")
			{
				for (int j = 0; j < sampBonus.Length; j++)
				{
					if (i != j && bonusObj[j].activeSelf)
						sampBonus [i].award += sampBonus [j].award;
				}

				scr.objLev.totalPrice = sampBonus [i].award;
				totalPrice = scr.objLev.totalPrice;
				Debug.Log ("totalPrice = " + scr.objLev.totalPrice);
				sampBonus [i].awardText.text = scr.gM.moneyString (sampBonus [i].award);
			}
		}
	}
		
	void Update()
	{
		if (isMoneyToBank)
		{
			timer += Time.deltaTime;

			if (timer <= Time.deltaTime && timer != 0)
			{
				scr.alPrScr.moneyCount += totalPrice;

				if (scr.alPrScr.moneyCount > 10000000)
					scr.alPrScr.moneyCount = 10000000;

				scr.alPrScr.setMoney = true;
			} 
			else if (timer >= 0.8f)
			{
				if (totalPrice > 0)
				{
					if (timer >= 1 && timer < 1 + Time.deltaTime)
					{
						scr.levAudScr.moneyWinSource.Play();
						scr.levAudScr.moneyWinSource1.PlayDelayed(0.2f);
					}

					if (totalPrice > 1000)
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

					if (bankMoney > 10000000)
						bankMoney = 10000000;

					moneyBankText.text = scr.gM.moneyString(bankMoney);
				} 
				else
				{
					if (scr.levAudScr.moneyWinSource.isPlaying)
					{
						scr.levAudScr.moneyWinSource.Stop();
						scr.levAudScr.moneyWinSource1.Stop();
						isMoneyToBank = false;
						menuButton.SetActive(true);
					}

					if (!menuButton.activeSelf)
						menuButton.SetActive(true);

					if (!advertiseShown)
					{
						scr.objLev.ShowInterstitialAd();
						advertiseShown = true;
					}
				}
			}
		}
	}
		
	private bool advertiseShown;

	private void GetEnemySkill()
	{
		for (int i = 0; i < scr.prScrL.itemList.Count; i++)
		{
			if (scr.prScrL.itemList[i].icon == scr.alScr.enemySprite)
				skill = scr.prScrL.itemList[i].summSkill;
		} 
	}

	public void SetMoneyWin()
	{
		moneyBankText.gameObject.SetActive(true);
		isMoneyToBank = true;
		Time.timeScale = 1;
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class CareerItem
{
	[Multiline(2)]
	public string nameRus;
	[Multiline(2)]
	public string name;
	public Sprite icon, lockedIcon;
	public bool isLocked, isGalochka;
	public int award;
	public Button.ButtonClickedEvent thingToDo;
	public string bestResult;
}

public class CareerScrollList : MonoBehaviour
{
	public Scripts scr;

	public Sprite[] medals;
	public Color[] medalColors;
	public ScrollRect careerScrollRect;
	public Animator carPrevAnim;
	public Text lockText;
	public GameObject sampleButton;
	public static bool isChampionship1;
	public RectTransform contentPanel;
	public List<CareerItem>itemList;
	private string awardStr, bestResStr;
	[HideInInspector]
	public string finalStr;

	void Awake () 
	{
		if ((Application.systemLanguage == SystemLanguage.Russian ||
		    Application.systemLanguage == SystemLanguage.Ukrainian ||
		    Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange) 
		{
			awardStr = "приз:";
			bestResStr = "лучший\nрезультат:";
			finalStr = "финал";
		} 
		else
		{
			awardStr = "award:";
			bestResStr = "best\nresult:";
			finalStr = "final";
		}

		DeleteEditorButtons();
		SetLocks ();
		SetBestPlaces ();
		PopulateList ();
		SetCupPrices ();
	}
	
	void PopulateList () 
	{
		int k = 0;

		foreach (var item in itemList)
		{
			k ++;

			GameObject newButton = Instantiate(sampleButton) as GameObject;
			CareerSampleButton button = newButton.GetComponent<CareerSampleButton>();
			button.icon.sprite = item.icon;
			button.lockedIcon.sprite = item.lockedIcon;
			button.lockedIcon.gameObject.SetActive(item.isLocked);
			button.icon.gameObject.SetActive(!item.isLocked);
			button.buttonAnim.enabled = !item.isLocked;
			button.awardText.text = "" + scr.gM.moneyString(item.award);
			button.awardText1.text = awardStr;
			button.medalText1.text = bestResStr;

			if (k % 2 != 0)
				button.medalText.fontSize = 25;
			else
				button.medalText.fontSize = 40;

			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				button.nameLabel.text = item.nameRus;
			else
				button.nameLabel.text = item.name;
			
			if (item.isLocked)
			{
				button.awardText.gameObject.SetActive (true);
				button.medal.SetActive(false);
			}
			else 
			{
				if (item.bestResult != "0" && item.bestResult != "") 
				{
					button.medal.SetActive (true);
					button.medalText.text = item.bestResult;

					if (item.bestResult == "1") 
					{
						button.medal.GetComponent<Image> ().sprite = medals [2];
						button.medalText.color = Color.clear;
						button.medalText1.gameObject.SetActive (false);
					}
					else if (item.bestResult == "2" || item.bestResult == finalStr)
					{
						button.medal.GetComponent<Image> ().sprite = medals [1];
						button.medalText.color = Color.clear;
						button.medalText1.gameObject.SetActive (false);
					}
					else if (item.bestResult == "3")
					{
						button.medal.GetComponent<Image> ().sprite = medals [0];
						button.medalText.color = Color.clear;
						button.medalText1.gameObject.SetActive (false);
					}
					else
					{
						button.medal.GetComponent<Image> ().color = Color.clear;
						button.medalText1.gameObject.SetActive (true);
					}
				}
				else
					button.medal.SetActive (false);


				if (item.isGalochka) 
					button.awardText.gameObject.SetActive(false);
				else 
					button.awardText.gameObject.SetActive(true);
			}
				
			button.button.onClick = item.thingToDo;
			newButton.transform.SetParent (contentPanel);
		}
	}

	private void SetBestPlaces()
	{
		for (int i = 0; i < itemList.Count; i++) 
			itemList [i].bestResult = scr.alPrScr.bestResultInTourns [i];
	}

	private void SetCupPrices()
	{
		for (int i = 0; i < itemList.Count; i++)
			scr.alScr.cupPrices [i] = itemList [i].award;
	}

	private void SetLocks()
	{
		if (scr.alPrScr.trn1 > scr.alPrScr.opndTrns)
		{
			if (scr.alPrScr.unlock == 1)
			{
				scr.alPrScr.opndTrns = scr.alPrScr.trn1;
				// scr.achievm.UnlockAchievementById(GooglePlayManager.Instance.Achievements[scr.alPrScr.opndTrns - 1].Id);
			}
			else 
				scr.alPrScr.opndTrns = scr.alPrScr.trn1 - 1;
		}

		for (int i = 0; i < itemList.Count; i++)
		{
			if (scr.alPrScr.opndTrns != 0)
			{
				if (i < scr.alPrScr.opndTrns + 1)
				{
					itemList[i].isLocked = false;

					if (i < scr.alPrScr.opndTrns)
						itemList[i].isGalochka = true;
					else 
						itemList[i].isGalochka = false;
				} 
				else 
					itemList[i].isLocked = true;
			}
		}
			
		scr.alPrScr.isChange0 = true;
	}
		
	public void CupStart(int j)
	{
		scr.objM.buttonsSource.Play();

		if (!itemList [j].isLocked)
		{
			for (int i = 0; i < 2; i++)
			{
				if (i == 0) 
				{
					scr.objM.loadingPanelAnim.gameObject.SetActive (true);
					scr.objM.loadingPanelAnim.SetTrigger("call");
				} 
				else if (i == 1) 
				{
					scr.alPrScr.trn = (int)(j / 2 + 1);
					scr.alPrScr.trn1 = j + 1;
					scr.alPrScr.isLoadedFromMenu = 1;
					scr.alPrScr.unlock = 0;

					if(scr.alPrScr.finishTourn != "PlayoffGoes")
						SceneManager.LoadScene("Cup");
					else 
						SceneManager.LoadScene("Playoff");
				}
			}
		} 
		else
			CareerPreview();
			
		scr.alPrScr.isChange0 = true;
	}

	public void ChampStart(int j)
	{
		scr.objM.buttonsSource.Play();

		if (!itemList [j].isLocked)
		{
			for (int i = 0; i < 2; i++)
			{
				if (i == 0) 
				{
					scr.objM.loadingPanelAnim.gameObject.SetActive(true);
					scr.objM.loadingPanelAnim.SetTrigger("call");
				} 
				else if (i == 1)
				{
					scr.alPrScr.trn = (int)((j+1)/2 + 5);
					scr.alPrScr.trn1 = j + 1;
					scr.alPrScr.isLoadedFromMenu = 1;
					scr.alPrScr.unlock = 0;
					SceneManager.LoadScene("Championship");
				} 
			}
		} 
		else 
			CareerPreview();
		
		scr.alPrScr.isChange0 = true;
	}

	void DeleteEditorButtons()
	{
		GameObject[] carButtons = GameObject.FindGameObjectsWithTag("CareerButton");

		foreach (var item in carButtons)
			DestroyImmediate(item);
	}

	public void CareerPreview()
	{				
		scr.contTrnScr.backButton.SetActive(true);
		scr.contTrnScr.noButton.SetActive(false);
		scr.contTrnScr.yesButton.SetActive(false);
		scr.objM.topPanelShadow.SetActive(true);
		carPrevAnim.SetBool ("call", true);
		careerScrollRect.enabled = false;

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			lockText.text = "Выиграйте предыдущий турнир, чтобы открыть этот!";
		else
			lockText.text = "Win previous tournament to open this!";
	}

	public void CareerPreviewBack()
	{
		carPrevAnim.SetBool ("call", false);
		careerScrollRect.enabled = true;
	}

	public void IconNormal(RectTransform icon1)
	{
		icon1.localScale = new Vector3(1, 1, 1);
	}

	public void IconBigger(RectTransform icon1)
	{
		icon1.localScale = new Vector3(1.4f, 1.4f, 1);
	}

	public void ClearPlayer()
	{
		scr.alScr.playerSprite = null;
		scr.alScr.playerName = null;
		scr.alScr.playerName0 = null;
		scr.alScr.playerFlag = null;
	}

	public void ClearEnemy()
	{
		scr.alScr.enemySprite = null;
		scr.alScr.enemyName = null;
		scr.alScr.enemyName0 = null;
		scr.alScr.enemyFlag = null;
	}
}

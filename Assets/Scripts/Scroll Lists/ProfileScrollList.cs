using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ProfileItem 
{
	public string name0;
	public string name;
	public Sprite icon;
	public Sprite flag;
	[HideInInspector]
	public float summSkill;
	public int isOpened;
	public int moneyCoast;
	public Button.ButtonClickedEvent thingToDo;
}

public class ProfileScrollList : MonoBehaviour
{
	private Scripts scr;

	public List<ProfileItem>itemList;

	private int index;
	private string buyPlayerString;
	private float timer;

	private GameObject[] profileShowcase;
	private GameObject[] profileIcon;

	private GameObject[] profBut0;

	void Awake () 
	{
		scr = FindObjectOfType<Scripts> ();

		SetCountryNamesLanguage ();

		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			DestroyEditorButtons();

			for (int i = 0; i < itemList.Count; i++)
			{
				itemList[i].summSkill = i/(float)itemList.Count;
				itemList[i].isOpened = scr.alPrScr.openedPlayers[i];
			}

			PopulateList ();
			profileShowcase = GameObject.FindGameObjectsWithTag ("Showcase");
			profileIcon = GameObject.FindGameObjectsWithTag ("ProfileIcon");
			profBut0 = GameObject.FindGameObjectsWithTag("ProfileButton");

			scr.alScr.playerSprite = itemList[scr.alPrScr.playerIndex].icon;
			scr.alScr.playerName = itemList[scr.alPrScr.playerIndex].name;
			scr.alScr.playerName0 = itemList[scr.alPrScr.playerIndex].name0;
			scr.alScr.playerFlag = itemList[scr.alPrScr.playerIndex].flag;

			scr.gM.MainMenuBools.mainMenu = true;
			Preview(scr.alPrScr.playerIndex);
			gameObject.transform.SetParent(gameObject.transform);

			scr.objM.prevPlayerAnim.gameObject.SetActive(false);
			scr.objM.freePlayPlayerAnim.gameObject.SetActive(false);
		}
	}

	private void SetCountryNamesLanguage()
	{
		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
		{
			for (int i = 0; i < itemList.Count; i++)
			{
				for (int j = 0; j < scr.langScr.countriesTextL.Count; j++)
				{
					if (itemList [i].name0.Contains(scr.langScr.countriesTextL [j].english))
					{
						itemList [i].name0 = scr.langScr.countriesTextL [j].russian;
						break;
					}
				}
			}
		}
		else if (Application.systemLanguage == SystemLanguage.Spanish)
		{
			for (int i = 0; i < itemList.Count; i++)
			{
				for (int j = 0; j < scr.langScr.countriesTextL.Count; j++)
				{
					if (itemList [i].name0.Contains(scr.langScr.countriesTextL [j].english))
					{
						itemList [i].name0 = scr.langScr.countriesTextL [j].spanish;
						break;
					}
				}
			}
		}
	}

	void Update()
	{
		if (SceneManager.GetActiveScene ().name != "MainMenu") 
		{
			timer += Time.deltaTime;

			if (timer > 2)
				DestroyImmediate (gameObject);	
		}
	}

	void DestroyEditorButtons()
	{
		GameObject[] editorButtons = GameObject.FindGameObjectsWithTag("ProfileButton");

		foreach (var item in editorButtons)
			DestroyImmediate(item);
	}

	void PopulateList ()
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			GameObject newButton = Instantiate(scr.objM.sampleButton) as GameObject;
			ProfileSampleButton button = newButton.GetComponent<ProfileSampleButton>();

			int i1 = itemList.Count - i;
			button.nameLabel.text = "#" + i1;

			button.icon.sprite = itemList[i].icon;
			button.flag.sprite = itemList[i].flag;

			if (itemList[i].isOpened == 0)
			{
				button.lockIcon.SetActive(true);
				button.flag.color = Color.white;
				button.flagShowcase.color = new Color(1, 0.94f, 0.75f, 1);
			} 
			else
			{
				button.showcase.sprite = scr.objM.greenBox;
				button.icon.color = Color.white;
				button.lockIcon.SetActive(false);
				button.flag.color = Color.white;
				button.flagShowcase.color = new Color(1, 0.94f, 0.75f, 1);
			}

			button.moneyCoast.text = scr.gM.moneyString(itemList[i].moneyCoast);
			button.button.onClick = itemList[i].thingToDo;
			newButton.transform.SetParent (scr.objM.contentPanel1);
		}
	}

	public void SetSkillsAndSprite()
	{
		if (scr.gM.MainMenuBools.menuFreePlay)
			SetEnemy ();
		else
			SetSkillsAndSpriteFunction ();
	}

	private void SetSkillsAndSpriteFunction()
	{
		if (scr.gM.MainMenuBools.menuProfile || scr.gM.MainMenuBools.mainMenu)
		{
			if (itemList [index].isOpened == 1)
			{
				scr.alScr.playerSprite = itemList [index].icon;
				scr.alScr.playerName = itemList [index].name;
				scr.alScr.playerName0 = itemList [index].name0;
				scr.alScr.playerFlag = itemList [index].flag;
				scr.alPrScr.playerIndex = index;
				scr.alPrScr.isChange0 = true;
				scr.currPrPan.isChange = true;
			}
		}
	}

	public void Unlock()
	{
		int moneyCoast1 = itemList[index].moneyCoast;

		if (scr.alPrScr.moneyCount >= moneyCoast1 && itemList[index].isOpened == 0) 
		{
			ProfileSampleButton prSampBut = profBut0[index].GetComponent<ProfileSampleButton>();

			prSampBut.lockIcon.SetActive(false);
			prSampBut.icon.color = Color.white;
			prSampBut.flag.color = Color.white;
			prSampBut.flagShowcase.color = new Color(1, 0.94f, 0.75f, 1);

			scr.alPrScr.moneyCount-=moneyCoast1;
			itemList [index].isOpened = 1;
			scr.alPrScr.openedPlayers[index] = 1;
			scr.alPrScr.isChange0 = true;
		}
	}

	public void SetShowcase()
	{
		SetMainShowcase ();
	}

	private void SetMainShowcase()
	{
		if (scr.gM.MainMenuBools.menuProfile)
			SetMainShowcaseFcn (); 
		else if (scr.gM.MainMenuBools.mainMenu)
			SetMainOrRetroShowcaseOnStart ();
	}
		
	private void SetMainShowcaseFcn()
	{
		if (!scr.gM.MainMenuBools.menuFreePlay)
		{
			if (profileShowcase [index].GetComponent<Image> ().sprite != scr.objM.orangeBox)
			{
				for (int i = 0; i < itemList.Count; i++)
				{
					if (i == index)
						profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.orangeBox; 
					else
					{
						if (itemList [i].isOpened == 1)
							profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.greenBox;
					}
				}
			}
		} 
		else
		{
			if (index != scr.alPrScr.playerIndex) 
			{
				if (profileShowcase [index].GetComponent<Image> ().sprite != scr.objM.orangeBoxEn)
				{
					for (int i = 0; i < itemList.Count; i++)
					{
						if (i == index)
							profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.orangeBoxEn; 
						else
						{
							if (itemList [i].isOpened == 1 && 
								profileShowcase [i].GetComponent<Image> ().sprite != scr.objM.orangeBox)
								profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.greenBox;
						}
					}
				}
			}
		}
	}



	private void SetMainOrRetroShowcaseOnStart()
	{
		for (int i = 0; i < profileIcon.Length; i++)
		{
			if (i == index)
				profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.orangeBox;
			else 
			{
				if (itemList[i].isOpened == 1)
					profileShowcase [i].GetComponent<Image> ().sprite = scr.objM.greenBox;
			}
		}
	}


	public void SetEnemy()
	{
		if (index != scr.alPrScr.playerIndex && scr.gM.MainMenuBools.menuFreePlay)
		{
			scr.alScr.enemySprite = itemList [index].icon;
			scr.alScr.enemyName = itemList [index].name;
			scr.alScr.enemyName0 = itemList [index].name0;
			scr.alScr.enemyFlag = itemList [index].flag;
			scr.alPrScr.enemyIndexFP = index;
			scr.alPrScr.isChange0 = true;
			scr.currPrPan.isChangeEnemy = true;
		}
	}

	public void Preview(int index0)
	{
		index = index0;
		PreviewMain ();
	}

	public void PreviewMain()
	{
		int coast = itemList[index].moneyCoast;

		if (itemList[index].isOpened == 0)
		{
			GameObject.Find("ButtonsSource").GetComponent<AudioSource>().Play();

			if (!scr.gM.MainMenuBools.mainMenu) 
				scr.objM.topPanelShadow.SetActive(true);

			if (scr.alPrScr.moneyCount >= coast)
			{
				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
					buyPlayerString = "Вы хотите купить этого игрока за " + scr.gM.moneyString (coast) + "?";
				else
					buyPlayerString = "Do you want to buy this player for " + scr.gM.moneyString (coast) + "?";
				
				scr.objM.fpBuyPlayer.SetActive(true);
				scr.objM.profBuyPlayer.SetActive(true);
			} 
			else
			{
				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				{
					scr.objM.noButtonFP.GetComponent<Text>().text = "назад";
					scr.objM.noButtonProfPl.GetComponent<Text>().text = "назад";
					buyPlayerString = "У вас недостаточно денег на этого игрока";
				} 
				else 
				{
					scr.objM.noButtonFP.GetComponent<Text>().text = "back";
					scr.objM.noButtonProfPl.GetComponent<Text>().text = "back";
					buyPlayerString = "You have not enough money on this player";
				}
				
				scr.objM.profBuyPlayer.SetActive(false);
				scr.objM.fpBuyPlayer.SetActive(false);
			}
				
			scr.objM.prevPlayerAnim.gameObject.SetActive(true);
			scr.objM.prevPlayerAnim.SetBool("call", true);
			scr.objM.previewText.text = buyPlayerString;
		} 
		else
		{
			if (scr.gM.MainMenuBools.menuFreePlay)
			{
				SetShowcase ();
				SetEnemy ();
			} 
			else 
			{
				if (scr.gM.MainMenuBools.menuProfile || scr.gM.MainMenuBools.mainMenu)
				{
					SetShowcase();
					SetSkillsAndSprite();
				}
			} 
		}
	}
		
	public void PreviewBack(Text noText)
	{
		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
		{
			noText.text = "нет";
		}
		else
		{
			noText.text = "no";
		}

		scr.objM.prevPlayerAnim.SetBool ("call", false);
		scr.objM.prevPlayerAnim.gameObject.SetActive(false);
	}

	public void PreviewRetroBack(Text noText)
	{
		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
		{
			noText.text = "нет";
		}
		else
		{
			noText.text = "no";
		}
	}

	public void FreePlayPreviewPlayerBack()
	{
		scr.objM.freePlayPlayerAnim.SetBool ("call", false);
		scr.objM.freePlayPlayerAnim.gameObject.SetActive(false);
	}

	public void SetEnemyShowcaseInFreePlay()
	{
		profileShowcase [scr.alPrScr.playerIndex].
		GetComponent<Image> ().sprite = scr.objM.orangeBox;

		profileShowcase [scr.alPrScr.enemyIndexFP].
		GetComponent<Image> ().sprite = scr.objM.orangeBoxEn;
	}

	public void SetPlayerShowcaseInProfile()
	{
		profileShowcase [scr.alPrScr.enemyIndexFP].
		GetComponent<Image> ().sprite = scr.objM.greenBox;
		profileShowcase [scr.alPrScr.playerIndex].
		GetComponent<Image> ().sprite = scr.objM.orangeBox;
	}
}

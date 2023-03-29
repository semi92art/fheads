using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LostCup : MonoBehaviour
{
	public Scripts scr;

	public Animator lostPanelAnim;
	public Animator buttonAnim;
	public Text buttonText;
	public Image buttonImage;
	public Text panelText;
	public bool lostPlayoff;
	public GameObject toMenuButton;
	
	private int index;
	private string ending;
	private int timer;
	private string finalStr, semiFinStr, quaterFinStr, groupStr;

	void Awake()
	{
		if (SceneManager.GetActiveScene ().name == "Cup") 
		{
			scr.objCup.closeButton.SetActive (false);
		}
		else if (SceneManager.GetActiveScene ().name == "Playoff") 
		{
			scr.objPlf.closeButton.SetActive (false);
		}
		else if (SceneManager.GetActiveScene ().name == "Championship") 
		{
			scr.objCh.closeButton.SetActive (false);
		}


		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) &&
			scr.alPrScr.langChange)
		{
			finalStr = "финал";
			semiFinStr = "полуфинал";
			quaterFinStr = "четвертьфинал";
			groupStr = "групповой этап";
		} 
		else
		{
			finalStr = "final";
			semiFinStr = "semifinal";
			quaterFinStr = "quarterfinal";
			groupStr = "group stage";
		}
			
		if (scr.alPrScr.plfGames == 0) 
			scr.alPrScr.isW = 1;	
	}

	void Update()
	{
		UpdateMainLeague();
	}

	private void UpdateMainLeague()
	{
		timer++;

		if(timer == 1)
		{
			if (SceneManager.GetActiveScene().name == "Cup")
			{
				if (!scr.gM.isPlayoff && scr.alPrScr.cupGames == 3)
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) && 
						scr.alPrScr.langChange)
					{
						panelText.text = "Вы проиграли кубок!";
						buttonText.text = "В меню";
					} 
					else
					{
						panelText.text = "You lost cup!";
						buttonText.text = "To menu";
					}
					
					lostPanelAnim.SetTrigger("call");
					scr.objCup.closeButton.SetActive(true);
					scr.objCup.noButton.SetActive(false);
					scr.objCup.yesButton.SetActive(false);
					buttonAnim.SetTrigger("lost");
					toMenuButton.SetActive(false);
					scr.alPrScr.finishTourn = "FinishedCup";

					string str1 = scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1];

					if (str1 != "1" &&
						str1 != finalStr &&
						str1 != semiFinStr &&
						str1 != quaterFinStr)
					scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = groupStr;

					scr.alPrScr.isChange0 = true;
				} 
				else if (scr.gM.isPlayoff && scr.alPrScr.cupGames == 3)
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) && 
						scr.alPrScr.langChange)
						buttonText.text = "Плейофф";
					else
						buttonText.text = "Playoff";
					
					scr.objCup.infoPanelAnim.SetTrigger("call");
					scr.alPrScr.isW = 1;
					scr.alPrScr.isChange0 = true;
				}
				else if (scr.alPrScr.cupGames == 2)
				{
					CupPlayerList cupPlList = FindObjectOfType<CupPlayerList>();
					for (int i = 0; i < cupPlList.itemList.Count; i++)
					{
						if (cupPlList.itemList[i].imageSprite == scr.alScr.playerSprite)
						{
							if (cupPlList.itemList[i].Loses == 2)
							{
								if ((Application.systemLanguage == SystemLanguage.Russian ||
									Application.systemLanguage == SystemLanguage.Ukrainian ||
									Application.systemLanguage == SystemLanguage.Belarusian) && 
									scr.alPrScr.langChange)
									panelText.text = "Вы проиграли кубок!\nНазад в главное меню?";
								else
									panelText.text = "You lost cup!\nGo back to main menu?";
								
								lostPanelAnim.SetTrigger("call");

								string str1 = scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1];

								if (str1 != "1" &&
									str1 != finalStr &&
									str1 != semiFinStr &&
									str1 != quaterFinStr)
									scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = groupStr;
								
								scr.alPrScr.isChange0 = true;
							}
							else
							{
								if ((Application.systemLanguage == SystemLanguage.Russian ||
									Application.systemLanguage == SystemLanguage.Ukrainian ||
									Application.systemLanguage == SystemLanguage.Belarusian) &&
									scr.alPrScr.langChange)
								{
									panelText.text = "Вы хотите выйти в главное меню? " +
									"Можно будет продолжить этот розыгрыш позже.";
								} 
								else
								{
									panelText.text = "Do you want to exit to main menu? " +
									"You can continue this cup later.";
								}
							}
						}
					}

					scr.objCup.closeButton.SetActive(false);
				}
				else 
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) && 
						scr.alPrScr.langChange)
					{
						panelText.text = "Вы хотите выйти в главное меню? " +
							"Можно будет продолжить этот розыгрыш позже.";
					} 
					else
					{
						panelText.text = "Do you want to exit to main menu? " +
							"You can continue this cup later.";
					}

					scr.objCup.closeButton.SetActive(false);
				}
			} 
			else if (SceneManager.GetActiveScene().name == "Playoff") 
			{
				if (scr.alPrScr.plfGames == 3 && scr.alPrScr.isW == 1)
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) &&
						scr.alPrScr.langChange)
					{
						buttonText.text = "В меню";
						panelText.text = "Поздравляем! Вы выиграли кубок!";
					} 
					else
					{
						buttonText.text = "To menu";
						panelText.text = "Congradulations! You won cup!";
					}
						
					scr.objPlf.closeButton.SetActive(true);
					scr.objPlf.noButton.SetActive(false);
					scr.objPlf.yesButton.SetActive(false);
					toMenuButton.SetActive(false);
					scr.alPrScr.unlock = 1;
					scr.alPrScr.finishTourn = "FinishedCup";
					lostPanelAnim.SetTrigger("call");
					scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = "1";

					scr.alPrScr.isChange0 = true;
				}
				else 
				{
					if (scr.alPrScr.isW == 0)
					{
						if ((Application.systemLanguage == SystemLanguage.Russian ||
							Application.systemLanguage == SystemLanguage.Ukrainian ||
							Application.systemLanguage == SystemLanguage.Belarusian) && 
							scr.alPrScr.langChange)
						{
							buttonText.text = "В меню";
							panelText.text = "Вы проиграли кубок!";
						} 
						else
						{
							buttonText.text = "To menu";
							panelText.text = "You lost cup!";
						}

						scr.objPlf.closeButton.SetActive(true);
						scr.objPlf.noButton.SetActive(false);
						scr.objPlf.yesButton.SetActive(false);
						toMenuButton.SetActive(false);
						lostPlayoff = true;
						buttonAnim.SetTrigger("lost");
						lostPanelAnim.SetTrigger("call");
						scr.alPrScr.finishTourn = "FinishedCup";

						string str1 = scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1];

						if (scr.alPrScr.plfGames == 3) 
						{
							if (str1 != "1")
								scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = finalStr;
						}
						else if (scr.alPrScr.plfGames == 2)
						{
							if (str1 != "1" &&
								str1 != finalStr)
								scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = semiFinStr;
						}
						else if (scr.alPrScr.plfGames == 1)
						{
							if (str1 != "1" &&
								str1 != finalStr &&
								str1 != semiFinStr)
								scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = quaterFinStr;
						}
							
						scr.alPrScr.isChange0 = true;
					} 
					else 
					{
						if ((Application.systemLanguage == SystemLanguage.Russian ||
							Application.systemLanguage == SystemLanguage.Ukrainian ||
							Application.systemLanguage == SystemLanguage.Belarusian) && 
							scr.alPrScr.langChange)
						{
							panelText.text = "Вы хотите выйти в главное меню? " +
								"Можно будет продолжить этот розыгрыш позже.";
						} 
						else
						{
							panelText.text = "Do you want to exit to main menu? " +
								"You can continue this cup later.";
						}

						scr.objPlf.closeButton.SetActive(false);
					}
				}
			} 
			else if (SceneManager.GetActiveScene().name == "Championship")
			{
				if (scr.chLIm.endOfChamp)
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) && 
						scr.alPrScr.langChange)
						buttonText.text = "В меню";
					else
						buttonText.text = "To menu";

					scr.objCh.closeButton.SetActive(true);
					scr.objCh.noButton.SetActive(false);
					scr.objCh.yesButton.SetActive(false);
					toMenuButton.SetActive(false);
					scr.alPrScr.finishTourn = "FinishedCup";

					if(scr.chL.itemList[0].imageSprite == scr.alScr.playerSprite)
					{
						scr.objCh.cupImage.sprite = scr.objCh.goldMedal;
						scr.objCh.secondPanel.SetTrigger("call");
						//scr.objCh.cupImage.sprite = scr.objCh.currCupSprite;
						lostPanelAnim.SetTrigger("call");
						scr.alPrScr.unlock = 1;
						scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = "1";
						scr.alPrScr.isChange0 = true;

						if ((Application.systemLanguage == SystemLanguage.Russian ||
							Application.systemLanguage == SystemLanguage.Ukrainian ||
							Application.systemLanguage == SystemLanguage.Belarusian) && 
							scr.alPrScr.langChange)
							panelText.text = "Поздравляем! Вы выиграли чемпионат!";
						else
							panelText.text = "Congradulations! You won championship!";
					} 
					else 
					{
						for (int i = 1; i < scr.chL.itemList.Count; i++)
						{
							if (scr.chL.itemList[i].imageSprite == scr.alScr.playerSprite)
							{
								scr.objCh.secondPanel.SetTrigger("call");
								lostPanelAnim.SetTrigger("call");
								scr.alPrScr.unlock = 0;

								index = i;
								int indPlusOne = index + 1;

								if (i == 1)
								{
									ending = "-nd";
									scr.objCh.cupImage.sprite = scr.objCh.silverMedal;
									buttonAnim.SetTrigger("lost");
								} 
								else if (i == 2)
								{
									ending = "-rd";
									scr.objCh.cupImage.sprite = scr.objCh.bronzeMedal;
									buttonAnim.SetTrigger("lost");
								} 
								else 
								{
									ending = "-th";
									scr.objCh.cupImage.color = Color.clear;
									buttonAnim.SetTrigger("lost1");
								}

								if ((Application.systemLanguage == SystemLanguage.Russian ||
									Application.systemLanguage == SystemLanguage.Ukrainian ||
									Application.systemLanguage == SystemLanguage.Belarusian) && 
									scr.alPrScr.langChange)
									panelText.text = "Чемпионат окончен. Вы заняли " + indPlusOne + " место";
								else
									panelText.text = "The championship is over. You became " + indPlusOne + ending;

								string str1 = scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1];
								int checkBestRes = 0;

								int.TryParse (str1, System.Globalization.NumberStyles.Any,
									new System.Globalization.CultureInfo ("en-US"), out checkBestRes);

								if (indPlusOne < checkBestRes)
									scr.alPrScr.bestResultInTourns [scr.alPrScr.trn1 - 1] = indPlusOne.ToString ();

								scr.infM.finalPlaceText.gameObject.SetActive (true);
								scr.infM.finalPlaceText.text = indPlusOne.ToString ();
					
								scr.alPrScr.isChange0 = true;
							}
						}
					}
				} 
				else 
				{
					if ((Application.systemLanguage == SystemLanguage.Russian ||
						Application.systemLanguage == SystemLanguage.Ukrainian ||
						Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
					{
						panelText.text = "Вы хотите выйти в главное меню? " +
							"Можно будет продолжить этот розыгрыш позже.";
					} 
					else
					{
						panelText.text = "Do you want to exit to main menu? " +
							"You can continue this championship later.";
					}

					scr.objCh.closeButton.SetActive(false);
				}
			}
		}
	}
}

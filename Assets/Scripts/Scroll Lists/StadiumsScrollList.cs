using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StadiumItem {
	public Sprite icon;
	public int isOpened;
	public int moneyCoast;
	public Button.ButtonClickedEvent thingToDo;
}

public class StadiumsScrollList : MonoBehaviour 
{
	public Scripts scr;

	public Sprite greenBox, blueBox;
	public Animator prevAnim;
	public Text previewText;
	public GameObject sampleButton;
	public List<StadiumItem>itemList;
	public RectTransform contentPanel1;
	public ScrollRect scrList;

	private int index;
	private string buyString;

	void Awake () 
	{
		DestroyEditorButtons();	

		for (int i = 0; i < itemList.Count; i++)
			itemList[i].isOpened = scr.alPrScr.openedStadiums[i];
		
		PopulateList1 ();
	}
	
	void DestroyEditorButtons()
	{
		GameObject[] editorButtons = GameObject.FindGameObjectsWithTag("StadiumButton");

		foreach (var item in editorButtons)
			DestroyImmediate(item);
	}

	void PopulateList1 () 
	{
		foreach (var item in itemList) 
		{
			GameObject newButton = Instantiate(sampleButton) as GameObject;
			StadiumButton button = newButton.GetComponent<StadiumButton>();
			button.icon.sprite = item.icon;

			if(item.isOpened == 0)
			{
				button.lockIcon.SetActive(true);
				button.galochka.gameObject.SetActive(false);
			} 
			else
			{
				button.lockIcon.SetActive(false);
				button.showcase.sprite = greenBox;
				button.galochka.gameObject.SetActive(true);
			}

			button.moneyCoast.text = scr.gM.moneyString (item.moneyCoast);
			button.button.onClick = item.thingToDo;
			newButton.transform.SetParent (contentPanel1);
		}
	}

	public void Unlock()
	{
		int moneyCoast1 = itemList[index].moneyCoast;

		if (itemList [index].isOpened==0)
		{
			GameObject[] stadiumButton = GameObject.FindGameObjectsWithTag("StadiumButton");
			StadiumButton stBut1 = stadiumButton[index].GetComponent<StadiumButton>();
			stBut1.lockIcon.SetActive(false);
			stBut1.galochka.gameObject.SetActive(true);
			stBut1.showcase.sprite = greenBox;

			scr.alPrScr.moneyCount-=moneyCoast1;
			itemList [index].isOpened = 1;
			scr.alPrScr.openedStadiums[index] = 1;
			scr.alPrScr.isChange0 = true;
		} 
	}

	public void Preview(int index0)
	{
		GameObject.Find("ButtonsSource").GetComponent<AudioSource>().Play();
		scrList.enabled = false;
		scr.objM.fpBuyPlayer.SetActive(false);
		index = index0;
		int coast = itemList[index].moneyCoast;

		if (itemList [index].isOpened == 0) 
		{
			scr.objM.topPanelShadow.SetActive(true);

			if (scr.alPrScr.moneyCount>=coast) 
			{
				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
					buyString = "Вы хотите купить этот стадион за " + scr.gM.moneyString (coast) + "?";
				else
					buyString = "Do you want to buy this stadium for " + scr.gM.moneyString (coast) + "?";
			
				scr.objM.profBuyStadium.SetActive (true);
			} 
			else
			{
				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
				{
					buyString = "У вас недостаточно денег на этот стадион.";
					scr.objM.noButtonFP.GetComponent<Text>().text = "назад";
					scr.objM.noButtonProfSt.GetComponent<Text>().text = "назад";
				} 
				else
				{
					buyString = "You have not enough money for this stadium.";
					scr.objM.noButtonFP.GetComponent<Text>().text = "back";
					scr.objM.noButtonProfSt.GetComponent<Text>().text = "back";
				}

				scr.objM.profBuyStadium.SetActive (false);
			}

			prevAnim.SetBool ("call", true);
			previewText.text = buyString;
		} 
		else 
		{
			if ((Application.systemLanguage == SystemLanguage.Russian ||
				Application.systemLanguage == SystemLanguage.Ukrainian ||
				Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange) 
			{
				buyString = "Вы уже купили этот стадион.";
				scr.objM.noButtonProfSt.GetComponent<Text>().text = "назад";
			} 
			else
			{
				buyString = "You have already bought this stadium.";
				scr.objM.noButtonProfSt.GetComponent<Text>().text = "back";
			}
				
			scr.objM.profBuyStadium.SetActive (false);
			prevAnim.SetBool ("call", true);
			previewText.text = buyString;
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

		scrList.enabled = true;
		prevAnim.SetBool ("call", false);
	}
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Events;

[System.Serializable]
public class CountriesScrollItem
{
    public int cntr_int;
    public Countries.CountryName cntr;
}


public class ProfileManager : MonoBehaviour
{
    public Scripts scr;

    public int[] moneyCoast;
    public int[] moneyCoast_2;
	public List<ProfileItem> itemList;
    public List<ProfileItem_2> itemList_2;

    [HideInInspector]
    public int index, butInd;
	private string buyPlayerString;

    public GameObject[] profBut0;
    public GameObject[] profBut0_2;

    public GameObject[] profShowcase;
    public GameObject[] profShowcase_0;
    public GameObject[] profShowcase_2;
    public GameObject[] profShowcase_2_0;

    public List<CountriesScrollItem> cntrScrollList;
    private bool is2ndLgOpnd;

	void Awake () 
	{
        SetPlayerIndexes();
        SetCountryIndexes();

		if (SceneManager.GetActiveScene().buildIndex == 1)
		{
			PopulateList_1 ();
            PopulateList_2 ();
            GameManager.Instance._menues = Menues.MainMenu;

            switch (PlayerPrefs.GetInt("PlayerLeague"))
            {
                case 1:
                    Preview(scr.alPrScr.playerIndex);
                    cntr_0 = scr.cntrL.Countries[itemList[scr.alPrScr.playerIndex].cntrInd].country;

                    for (int i = 0; i < cntrScrollList.Count; i++)
                    {
                        if (cntrScrollList[i].cntr == cntr_0)
                        {
                            ViewPlayersFromCountry(cntrScrollList[i].cntr_int);
                            break;
                        }
                    }
                    break;
                case 2:
                    Preview_2(scr.alPrScr.playerIndex);
                    cntr_0 = scr.cntrL.Countries[itemList_2[scr.alPrScr.playerIndex].cntrInd].country;

                    for (int i = 0; i < cntrScrollList.Count; i++)
                    {
                        if (cntrScrollList[i].cntr == cntr_0)
                        {
                            ViewPlayersFromCountry(cntrScrollList[i].cntr_int);
                            break;
                        }
                    }
                    break;
            }

            SetOpenedPlayersCountryText(false);
		}
	}

    private void SetPlayerIndexes()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < scr.plL.Players.Count; j++)
            {
                if (itemList[i].player.ToString() == scr.plL.Players[j].player.ToString())
                {
                    itemList[i].plInd = j;
                    break;
                } 
            }
        }

        for (int i = 0; i < itemList_2.Count; i++)
        {
            for (int j = 0; j < scr.plL.Players_2.Count; j++)
            {
                if (itemList_2[i].player.ToString() == scr.plL.Players_2[j].player.ToString())
                {
                    itemList_2[i].plInd = j;
                    break;
                }
            }
        }
    }

    private void SetCountryIndexes()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            for (int j = 0; j < scr.cntrL.Countries.Count; j++)
            {
                if (itemList[i].country.ToString() == scr.cntrL.Countries[j].country.ToString())
                {
                    itemList[i].cntrInd = j;
                    break;
                }
            }
        }

        for (int i = 0; i < itemList_2.Count; i++)
        {
            for (int j = 0; j < scr.cntrL.Countries.Count; j++)
            {
                if (itemList_2[i].country.ToString() == scr.cntrL.Countries[j].country.ToString())
                {
                    itemList_2[i].cntrInd = j;
                    break;
                }
            }
        }
    }

    // Populate Heads in League 1:
	private void PopulateList_1 ()
	{
        float new_h_val;

        for (int i = 0; i < itemList.Count; i++)
		{
            GameObject newButton = profBut0[i];
			ProfileSampleButton button = newButton.GetComponent<ProfileSampleButton>();

			int i1 = itemList.Count - i;
			button.nameLabel.text = "#" + i1;

            for (int j = 0; j < itemList.Count; j++)
            {
                if (itemList[j].plInd == i)
                {
                    button.lockedObj.SetActive(false);
                    button.icon.sprite = itemList[j].icon;
                    button.leg.sprite = scr.cntrL.Countries[itemList[j].cntrInd].boot;

                    bool opnd = scr.univFunc.Int2Bool(scr.alPrScr.openedPlayers[i]);
                    button.isOpened = opnd;
                    button.showcase.color = opnd ? scr.objM.col_Blue : scr.objM.col_Gray;
                    button.showcase_2.color = opnd ? scr.objM.col_Blue : scr.objM.col_Gray;
                    button.im_MoneyCoastShowcase.color = opnd ?
                        Color.clear : scr.objM.col_Gray;
                    button.moneyCoast.text = scr.univFunc.moneyString(moneyCoast[i]);
                    button.moneyCoast.gameObject.SetActive(!scr.univFunc.Int2Bool(scr.alPrScr.openedPlayers[i]));
                    button.plInd = j;
                    button.buttonInd = i;
                    button.cntrInd = itemList[j].cntrInd;
                    button.button.onClick = itemList[j].toDo;

                    button.im_Speed.rectTransform.sizeDelta = new Vector2(
                            button.im_Speed.rectTransform.rect.width,
                            button.im_Speed.rectTransform.rect.height * itemList[j].skill_Speed / 100f);
                    button.im_Kick.rectTransform.sizeDelta = new Vector2(
                            button.im_Kick.rectTransform.rect.width,
                            button.im_Kick.rectTransform.rect.height * itemList[j].skill_Kick / 100f);
                    button.im_Jump.rectTransform.sizeDelta = new Vector2(
                            button.im_Jump.rectTransform.rect.width,
                            button.im_Jump.rectTransform.rect.height * itemList[j].skill_Jump / 100f);
                    break;
                }
            }

            newButton.transform.SetParent (scr.objM.cP);
		}
	}

    // Populate Heads in League 2:
    private void PopulateList_2()
    {
        float new_h_val;

        for (int i = 0; i < itemList_2.Count; i++)
        {
            GameObject newButton = profBut0_2[i];
            ProfileSampleButton button = newButton.GetComponent<ProfileSampleButton>();
            button.lockedObj.SetActive(false);

            int i1 = itemList_2.Count - i;
            button.nameLabel.text = "#" + i1;

            for (int j = 0; j < itemList_2.Count; j++)
            {
                if (itemList_2[j].plInd == i)
                {
                    button.icon.sprite = itemList_2[j].icon;
                    button.leg.sprite = scr.cntrL.Countries[itemList_2[j].cntrInd].boot;

                    bool opnd = scr.univFunc.Int2Bool(scr.alPrScr.openedPlayers_2[i]);
                    button.isOpened = opnd;
                    button.showcase.color = opnd ? scr.objM.col_Blue : scr.objM.col_Gray;
                    button.showcase_2.color = opnd ? scr.objM.col_Blue : scr.objM.col_Gray;
                    button.im_MoneyCoastShowcase.color = opnd ?
                        Color.clear : scr.objM.col_Gray;
                    button.moneyCoast.text = scr.univFunc.moneyString(moneyCoast_2[i]);
                    button.moneyCoast.gameObject.SetActive(!scr.univFunc.Int2Bool(scr.alPrScr.openedPlayers_2[i]));
                    button.plInd = j;
                    button.buttonInd = i;
                    button.cntrInd = itemList_2[j].cntrInd;
                    button.button.onClick = itemList_2[j].toDo;

                    button.im_Speed.rectTransform.sizeDelta = new Vector2(
                            button.im_Speed.rectTransform.rect.width,
                            button.im_Speed.rectTransform.rect.height * itemList_2[j].skill_Speed / 100f);
                    button.im_Kick.rectTransform.sizeDelta = new Vector2(
                            button.im_Kick.rectTransform.rect.width,
                            button.im_Kick.rectTransform.rect.height * itemList_2[j].skill_Kick / 100f);
                    button.im_Jump.rectTransform.sizeDelta = new Vector2(
                            button.im_Jump.rectTransform.rect.width,
                            button.im_Jump.rectTransform.rect.height * itemList_2[j].skill_Jump / 100f);
                    break;
                }
            }

            newButton.transform.SetParent(scr.objM.cP);
        }
    }

	void DestroyEditorButtons()
	{
		GameObject[] editorButtons = GameObject.FindGameObjectsWithTag("ProfileButton");

        for (int i = 0; i < editorButtons.Length; i++)
            DestroyImmediate(editorButtons[i]);
	}

    public void Unlock()
    {
        int moneyCoast1 = 0;

        if (previewPlayerLg == 1)
        {
            if (GameManager.Instance._menues == Menues.MenuPlayers)
                moneyCoast1 = moneyCoast[butInd];

            if (scr.alPrScr.moneyCount >= moneyCoast1 && scr.alPrScr.openedPlayers[butInd] == 0)
            {
                ProfileSampleButton but = profBut0[butInd].GetComponent<ProfileSampleButton>();

                if (GameManager.Instance._menues == Menues.MenuPlayers)
                {
                    scr.alPrScr.moneyCount -= moneyCoast1;
                    scr.alPrScr.setMoney = true;
                }

                but.isOpened = true;
                but.moneyCoast.gameObject.SetActive(false);
                but.im_MoneyCoastShowcase.gameObject.SetActive(false);
                scr.alPrScr.openedPlayers[butInd] = 1;
            }
        }
        else if (previewPlayerLg == 2)
        {
            if (GameManager.Instance._menues == Menues.MenuPlayers)
                moneyCoast1 = moneyCoast_2[butInd];

            if (scr.alPrScr.moneyCount >= moneyCoast1 && scr.alPrScr.openedPlayers_2[butInd] == 0)
            {
                ProfileSampleButton but = profBut0_2[butInd].GetComponent<ProfileSampleButton>();

                if (GameManager.Instance._menues == Menues.MenuPlayers)
                {
                    scr.alPrScr.moneyCount -= moneyCoast1;
                    scr.alPrScr.setMoney = true;
                }

                but.isOpened = true;
                but.moneyCoast.gameObject.SetActive(false);
                but.im_MoneyCoastShowcase.gameObject.SetActive(false);
                scr.alPrScr.openedPlayers_2[butInd] = 1;
            }
        }

        scr.alPrScr.doCh = true;
        SetOpenedPlayersCountryText(false);
    }

	public void SetSkillsAndSprite()
	{
        if (previewPlayerLg == 1)
        {
            if (scr.alPrScr.openedPlayers[butInd] == 1)
            {
                scr.currPrPan.isChange = true;
                scr.alPrScr.playerIndex = index;
                scr.alPrScr.buttonIndex = butInd;

                //Set Skills:
                scr.alPrScr.skill_Speed = itemList[index].skill_Speed;
                scr.alPrScr.skill_Kick = itemList[index].skill_Kick;
                scr.alPrScr.skill_Jump = itemList[index].skill_Jump;
            }
        }
        else if (previewPlayerLg == 2)
        {
            if (scr.alPrScr.openedPlayers_2[butInd] == 1)
            {
                scr.currPrPan.isChange = true;
                scr.alPrScr.playerIndex = index;
                scr.alPrScr.buttonIndex = butInd;

                //Set Skills:
                scr.alPrScr.skill_Speed = itemList_2[index].skill_Speed;
                scr.alPrScr.skill_Kick = itemList_2[index].skill_Kick;
                scr.alPrScr.skill_Jump = itemList_2[index].skill_Jump;
            }
        }

        scr.alPrScr.doCh = true;
	}


	public void SetShowcase()
	{
        if (previewPlayerLg == 1)
        {
            if (profShowcase[butInd].GetComponent<Image>().color != scr.objM.col_Orange)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    if (i == butInd)
                    {
                        profShowcase[i].GetComponent<Image>().color = scr.objM.col_Orange;
                        profShowcase_0[i].GetComponent<Image>().color = scr.objM.col_Orange;
                    }
                    else
                    {
                        if (scr.alPrScr.openedPlayers[i] == 1)
                        {
                            profShowcase[i].GetComponent<Image>().color = scr.objM.col_Blue;
                            profShowcase_0[i].GetComponent<Image>().color = scr.objM.col_Blue;
                        }    
                    }
                }

                for (int i = 0; i < itemList_2.Count; i++)
                {
                    if (scr.alPrScr.openedPlayers_2[i] == 1)
                    {
                        profShowcase_2[i].GetComponent<Image>().color = scr.objM.col_Blue;
                        profShowcase_2_0[i].GetComponent<Image>().color = scr.objM.col_Blue;
                    }
                }
            }
        }
        else if (previewPlayerLg == 2)
        {
            if (profShowcase_2[butInd].GetComponent<Image>().color != scr.objM.col_Orange)
            {
                for (int i = 0; i < itemList_2.Count; i++)
                {
                    if (i == butInd)
                    {
                        profShowcase_2[i].GetComponent<Image>().color = scr.objM.col_Orange;
                        profShowcase_2_0[i].GetComponent<Image>().color = scr.objM.col_Orange;
                    }
                    else
                    {
                        if (scr.alPrScr.openedPlayers_2[i] == 1)
                        {
                            profShowcase_2[i].GetComponent<Image>().color = scr.objM.col_Blue;
                            profShowcase_2_0[i].GetComponent<Image>().color = scr.objM.col_Blue;
                        }
                    }
                }

                for (int i = 0; i < itemList.Count; i++)
                {
                    if (scr.alPrScr.openedPlayers[i] == 1)
                    {
                        profShowcase[i].GetComponent<Image>().color = scr.objM.col_Blue;
                        profShowcase_0[i].GetComponent<Image>().color = scr.objM.col_Blue;
                    }
                }
            }
        }

        scr.alPrScr.plLg = previewPlayerLg;
        scr.alPrScr.doCh = true;
	}

    [HideInInspector]
    public int previewPlayerLg;

    public void Preview(int ind)
	{
        previewPlayerLg = 1;
        index = ind;
        butInd = itemList[ind].plInd;
        int coast = moneyCoast[butInd];

        if (scr.alPrScr.openedPlayers[butInd] == 0)
		{
			if (scr.alPrScr.moneyCount >= coast)
			{
                buyPlayerString = "Do you want to buy this head for " + scr.univFunc.moneyString(coast) + "?";
				scr.allAw.yesButObj.SetActive(true);
			} 
			else
			{
                buyPlayerString = "You have not enough money on this player";
                scr.allAw.yesButObj.SetActive(false);
			}

            scr.allAw.CallAwardPanel_2();
            scr.allAw.prevText.text = buyPlayerString;
		} 
		else
		{
			SetShowcase();
            SetSkillsAndSprite();
		}

        if (GameManager.Instance._menues != Menues.MainMenu)
            scr.objM.buttonsSource.Play();
	}

	public void SetPlayerShowcaseInProfile()
	{
        int ind = scr.alPrScr.playerIndex;
        profShowcase[ind].GetComponent<Image>().color = scr.objM.col_Orange;
	}

    private string buyPlayerStr_1(bool isEnoughMoney)
    {
        string str0 = isEnoughMoney ? "Do you want to buy this head for " : 
            "you have not enough money on this head";

        return str0;
    }

    public void Preview_2(int ind)
    {
        previewPlayerLg = 2;
        index = ind;
        butInd = itemList_2[ind].plInd;

        AudioSource audS = GameObject.Find("ButtonsSource").GetComponent<AudioSource>();

        int coast = moneyCoast_2[butInd];

        if (scr.alPrScr.openedPlayers_2[butInd] == 0)
        {
            if (audS.enabled)
                audS.Play();

            if (scr.alPrScr.moneyCount >= coast)
            {
                buyPlayerString = "Do you want to buy this head for " + scr.univFunc.moneyString(coast) + "?";
                scr.allAw.yesButObj.SetActive(true);
            }
            else
            {
                scr.allAw.noButTxt.text = "Back";
                buyPlayerString = "You have not enough money on this player";
                scr.allAw.yesButObj.SetActive(false);
            }

            scr.allAw.CallAwardPanel_2();
            scr.allAw.prevText.text = buyPlayerString;
        }
        else
        {
            SetShowcase();
            SetSkillsAndSprite();
        }

        if (GameManager.Instance._menues != Menues.MainMenu)
            scr.objM.buttonsSource.Play();
    }

    private Countries.CountryName cntr_0;
    private int cntrInd_0;

    public void SetOpenedPlayersCountryText(bool oneCountry)
    {
        int num0 = 0;
        int num1 = 0;

        if (!oneCountry)
        {
            for (int i = 0; i < scr.objM.text_opndPlayers.Length; i++)
            {
                num0 = 0;
                num1 = 0;
                SetCountry(i);

                for (int j = 0; j < profBut0.Length; j++)
                {
                    if (profBut0[j].GetComponent<ProfileSampleButton>().cntrInd == cntrInd_0)
                    {
                        num0++;
                        if (profBut0[j].GetComponent<ProfileSampleButton>().isOpened)
                            num1++;
                    }
                }

                for (int j = 0; j < profBut0_2.Length; j++)
                {
                    if (profBut0_2[j].GetComponent<ProfileSampleButton>().cntrInd == cntrInd_0)
                    {
                        num0++;
                        if (profBut0_2[j].GetComponent<ProfileSampleButton>().isOpened)
                            num1++;
                    }
                }

                scr.objM.text_opndPlayers[i].text = num1.ToString() + "/" + num0.ToString();
            }
        }
        /*else
        {
            num0 = 0;
            num1 = 0;
            SetCountry(cntr_int0);

            for (int j = 0; j < profBut0.Length; j++)
            {
                if (profBut0[j].GetComponent<ProfileSampleButton>().cntrInd == cntrInd_0)
                {
                    num0++;
                    if (profBut0[j].GetComponent<ProfileSampleButton>().isOpened)
                        num1++;
                }
            }

            for (int j = 0; j < profBut0_2.Length; j++)
            {
                if (profBut0_2[j].GetComponent<ProfileSampleButton>().cntrInd == cntrInd_0)
                {
                    num0++;
                    if (profBut0_2[j].GetComponent<ProfileSampleButton>().isOpened)
                        num1++;
                }
            }

            scr.objM.text_opndPlayers[cntr_int0].text = num1.ToString() + "/" + num0.ToString();
        }*/
    }

    public int cntr_int0;

    public void ViewPlayersFromCountry(int cntr_int)
    {
        scr.objM.Button_Sound();

        cntr_int0 = cntr_int;
        SetCountry(cntr_int);

        for (int i = 0; i < profBut0.Length; i++)
        {
            int cntrInd = profBut0[i].GetComponent<ProfileSampleButton>().cntrInd;
            bool _bool = cntrInd == cntrInd_0 ? true : false;
            profBut0[i].SetActive(_bool);
        }

        for (int i = 0; i < profBut0_2.Length; i++)
        {
            int cntrInd = profBut0_2[i].GetComponent<ProfileSampleButton>().cntrInd;
            bool _bool = cntrInd == cntrInd_0 ? true : false;
            profBut0_2[i].SetActive(_bool);
        }
    }

    private void SetCountry(int cntr_int)
    {
        for (int i = 0; i < cntrScrollList.Count; i++)
        {
            if (cntr_int == cntrScrollList[i].cntr_int)
            {
                cntr_0 = cntrScrollList[i].cntr;
                break;
            }
        }

        for (int j = 0; j < scr.cntrL.Countries.Count; j++)
        {
            if (cntr_0 == scr.cntrL.Countries[j].country)
                cntrInd_0 = j;
        }
    }
}

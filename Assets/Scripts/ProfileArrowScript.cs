using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileArrowScript : MonoBehaviour {

	public Scripts scr;

	public Button[] leftButs, rightButs;
	public RectTransform plContPanel;
	public RectTransform stadContPanel;
	public ScrollRect plScrRect;
	public ScrollRect stadScrRect;
	
	private int plN = 5;
	private int stadN = 2;
	private int extrN = 1;
	public Sprite deactArrow, actArrow;

	void Awake()
	{
		foreach (var item in leftButs)
		{
			item.interactable = false;
			item.gameObject.GetComponent<Image>().sprite = deactArrow;
		}
	}

	public void SetLeagueNumber()
	{
		float posX = plContPanel.anchoredPosition.x;
		//float posY = plContPanel.anchoredPosition.y;
		float width = plScrRect.gameObject.GetComponent<RectTransform>().rect.width;

		int lN = 7 - (Mathf.RoundToInt (-posX / width) + 1);

		if ((Application.systemLanguage == SystemLanguage.Russian ||
			Application.systemLanguage == SystemLanguage.Ukrainian ||
			Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
			scr.objM.profileLeagueText.text = "основа: лига " + scr.gM.leagueNumberString(lN);
		else
			scr.objM.profileLeagueText.text = "base league " + scr.gM.leagueNumberString(lN);
	}

	void Start()
	{
		int ind = scr.alPrScr.playerIndex;
		int lN = 0;

		if (ind >= 0 && ind < 10)
			lN = 0;
		else if (ind >= 10 && ind < 20)
			lN = 1;
		else if (ind >= 20 && ind < 30)
			lN = 2;
		else if (ind >= 30 && ind < 40)
			lN = 3;
		else if (ind >= 40 && ind < 50)
			lN = 4;
		else if (ind >= 50 && ind < 60)
			lN = 5;

		Debug.Log ("lN = " + lN);

		if (lN >= 1) 
		{
			for (int i = 1; i <= lN; i++)
				PlayersArrow ("right");
		}
	}

	public void PlayersArrow (string side)
	{
		float posX = plContPanel.anchoredPosition.x;
		float posY = plContPanel.anchoredPosition.y;
		float width = plScrRect.gameObject.GetComponent<RectTransform>().rect.width;

		if (side == "right")
		{
			leftButs[0].interactable = true;
			leftButs[0].gameObject.GetComponent<Image>().sprite = actArrow;

			if (-posX/width <= plN - 1)
			{
				int lN = 7 - (Mathf.RoundToInt (-posX / width) + 2);
				plContPanel.anchoredPosition = new Vector3(posX - width, posY, 0);

				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
					scr.objM.profileLeagueText.text = "основа: лига " + scr.gM.leagueNumberString(lN);
				else
					scr.objM.profileLeagueText.text = "base league " + scr.gM.leagueNumberString(lN);
			}

			if (-posX/width >= plN - 1)
			{
				rightButs[0].interactable = false;
				rightButs[0].gameObject.GetComponent<Image>().sprite = deactArrow;
			}
		} 
		else if (side == "left")
		{
			rightButs[0].interactable = true;
			rightButs[0].gameObject.GetComponent<Image>().sprite = actArrow;

			if (-posX/width >= 1)
			{
				int lN = 7 - Mathf.RoundToInt (-posX / width);

				plContPanel.anchoredPosition = new Vector3(posX + width, posY, 0);

				if ((Application.systemLanguage == SystemLanguage.Russian ||
					Application.systemLanguage == SystemLanguage.Ukrainian ||
					Application.systemLanguage == SystemLanguage.Belarusian) && scr.alPrScr.langChange)
					scr.objM.profileLeagueText.text = "основа: лига " + scr.gM.leagueNumberString(lN);
				else
					scr.objM.profileLeagueText.text = "base league " + scr.gM.leagueNumberString(lN);
			}

			if (-posX/width <= 1)
			{
				leftButs[0].interactable = false;
				leftButs[0].gameObject.GetComponent<Image>().sprite = deactArrow;
			}
		}
	}

	public void StadiumsArrow (string side)
	{
		float posX = stadContPanel.anchoredPosition.x;
		float posY = stadContPanel.anchoredPosition.y;
		float width = stadScrRect.gameObject.GetComponent<RectTransform>().rect.width;

		if (side == "right")
		{
			leftButs[1].interactable = true;
			leftButs[1].gameObject.GetComponent<Image>().sprite = actArrow;

			if (-posX/width<=stadN - 1)
			{
				stadContPanel.anchoredPosition = new Vector3(posX - width, posY, 0);
			}

			if (-posX/width>=stadN - 1)
			{
				rightButs[1].interactable = false;
				rightButs[1].gameObject.GetComponent<Image>().sprite = deactArrow;
			}
		} 
		else if (side == "left")
		{
			rightButs[1].interactable = true;
			rightButs[1].gameObject.GetComponent<Image>().sprite = actArrow;

			if (-posX/width >= 1)
			{
				stadContPanel.anchoredPosition = new Vector3(posX + width, posY, 0);
			}

			if (-posX/width <= 1)
			{
				leftButs[1].interactable = false;
				leftButs[1].gameObject.GetComponent<Image>().sprite = deactArrow;
			}
		}
	}
}

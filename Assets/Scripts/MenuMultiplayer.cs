using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class MenuMultiplayer : MonoBehaviour 
{
	public Scripts scr;

	public GameObject playButton;

	public RectTransform plFlagContPan;
	public RectTransform playerContPan;
	public ScrollRect plFlagContScr;
	public ScrollRect playerContScr;
	public Image plFlArrPrev, plFlArrNext;
	public Image playerArrPrev, playerArrNext;
	public GameObject countrySampBut;
	public GameObject playerSampBut;

	public Sprite orangeArrow;
	public Sprite magentaArrow;
	public Sprite grayArrow;


	public List<posItemList>plItemList;

	private int plFlagIndex;
	private int playerIndex;
	public Sprite currPlFlag;
	private Sprite checkFlagSpr;

	private GameObject[] playersFP;

	public int currPlayerIndexFP;

	private bool isChange;
	private bool isFreePlay;

	private int index1 = -1;


	void Awake()
	{
		isFreePlay = true;

		plFlArrPrev.sprite = grayArrow;
		plFlArrPrev.GetComponent<Button> ().interactable = false;
		playerArrPrev.sprite = grayArrow;
		playerArrPrev.GetComponent<Button> ().interactable = false;
		playerArrNext.sprite = grayArrow;
		playerArrNext.GetComponent<Button> ().interactable = false;

		PopulateListPlayerFlag ();
		PopulateListPlayer ();
		playersFP = GameObject.FindGameObjectsWithTag("MultiplayerPlayer");
	}

	void Update()
	{
		if (isChange)
		{
			float xPos = playerContPan.anchoredPosition.x;
			float scrRectWidth = playerContScr.GetComponent<RectTransform>().rect.width;
			int playerIndex1 = (int)(-xPos/scrRectWidth);

			for (int i = 0; i < scr.prScrL.itemList.Count; i++)
			{
				Sprite plSpr = playersFP[playerIndex1].GetComponent<PlayerSampleButton>().playerIm.sprite;

				if (plSpr == scr.prScrL.itemList[i].icon)
				{
					if (scr.prScrL.itemList[i].isOpened == 0)
					{
						playButton.SetActive(false);
						playersFP[playerIndex1].GetComponent<PlayerSampleButton>().playerIm.color = Color.black;
					}
					else
					{
						scr.alScr.playerFlag = scr.prScrL.itemList[i].flag;
						scr.alScr.playerName = scr.prScrL.itemList[i].name;
						scr.alScr.playerName0 = scr.prScrL.itemList[i].name0;
						scr.alScr.playerSprite = scr.prScrL.itemList[i].icon;

						scr.alScr.enemyFlag = scr.prScrL.itemList[i].flag;
						scr.alScr.enemyName = scr.prScrL.itemList[i].name;
						scr.alScr.enemyName0 = scr.prScrL.itemList[i].name0;
						scr.alScr.enemySprite = scr.prScrL.itemList[i].icon;

						playButton.SetActive(true);
					}
				}
			}
			isChange = false;
		}


		if (isFreePlay)
		{
			if (index1 < 1)
			{
				index1++;
			}
			else if (index1 == 1)
			{
				index1++;
				SetCurrentFreePlay();
				isFreePlay = false;
			} 
			else
			{
				isFreePlay = false;
			}
		}
	}

	public void EnableFreePlay()
	{
		isFreePlay = true;
	}

	void SetCurrentFreePlay()
	{
		int plFlagIndex1;
		int playerIndex1;

		if (scr.alPrScr.launches == 1)
		{
			plFlagIndex1 = 12;
			playerIndex1 = 0;
		}
		else
		{
			plFlagIndex1 = PlayerPrefs.GetInt ("PlayerFlagIndex");
			playerIndex1 = PlayerPrefs.GetInt ("PlayerIndexFP");
		}

		//Debug.Log ("PlayerFlagIndex = " + plFlagIndex1);
		//Debug.Log ("PlayerIndexFP = " + playerIndex1);

		for (int i = 0; i < plFlagIndex1; i++) 
		{
			NextElement(plFlagContPan);
		}
		for (int i = 0; i < playerIndex1; i++) 
		{
			NextElement(playerContPan);
		}
		isChange = true;
	}

	void PopulateListPlayerFlag()
	{
		for (int i = 0; i < scr.dblScr.countriesList.Count; i++) 
		{
			for (int j = 0; j < scr.prScrL.itemList.Count; j++)
			{
				if (scr.dblScr.countriesList[i].flag == scr.prScrL.itemList[j].flag)
				{
					GameObject newButton = Instantiate(countrySampBut) as GameObject;
					FlagSampleButton button = newButton.GetComponent<FlagSampleButton>();
					button.flagIm.sprite = scr.prScrL.itemList[j].flag;
					button.countryName.text = scr.prScrL.itemList[j].name0;

					newButton.transform.SetParent (plFlagContPan);
					break;
				}
			}
		}
	}
		
	void PopulateListPlayer()
	{
		for (int i = 0; i < scr.dblScr.countriesList.Count; i++) 
		{
			for (int j = 0; j < scr.prScrL.itemList.Count; j++)
			{
				if (scr.dblScr.countriesList[i].flag == scr.prScrL.itemList[j].flag)
				{
					GameObject newButton = Instantiate(playerSampBut) as GameObject;
					PlayerSampleButton button = newButton.GetComponent<PlayerSampleButton>();
					button.playerIm.sprite = scr.prScrL.itemList[j].icon;
					button.tag = "MultiplayerPlayer";

					newButton.transform.SetParent (playerContPan);

					GameObject[] playersFP = GameObject.FindGameObjectsWithTag("MultiplayerPlayer");
					if (scr.dblScr.countriesList[i].flag != checkFlagSpr)
					{
						float width = (float)playerContScr.GetComponent<RectTransform>().rect.width;
						plItemList[i].anchPos = width * playersFP.Length;
					}
					plItemList[i].numOfPlayers ++;

					checkFlagSpr = scr.dblScr.countriesList[i].flag;
				}
			}
		}

		for (int i = 0; i < scr.dblScr.countriesList.Count; i++) 
		{
			if (i < plItemList.Count)
			{
				if ((int)plItemList[i].anchPos == 0)
				{
					plItemList.RemoveAt(i);
				}
			}
		}
	}

	public void NextElement(RectTransform cPan)
	{
		//Debug.Log("Next Elem");
		float elWidth = 0;

		if (cPan == plFlagContPan) 
		{
			elWidth = plFlagContScr.GetComponent<RectTransform> ().rect.width;
			float xPos = plFlagContPan.anchoredPosition.x;
			float yPos = plFlagContPan.anchoredPosition.y;

			if ((int)xPos <= (int)(-cPan.rect.width + 2 * elWidth))
			{
				plFlArrNext.sprite = grayArrow;
				plFlArrNext.GetComponent<Button>().interactable = false;
			}

			if (xPos > -cPan.rect.width + elWidth)
			{
				playerIndex = 0;
				plFlagContPan.anchoredPosition = new Vector3 (xPos - elWidth, yPos, 0);
				plFlagIndex ++;

				float yPos1 = playerContPan.anchoredPosition.y;
				float xWidth = playerContScr.GetComponent<RectTransform>().rect.width;

				playerContPan.anchoredPosition = new Vector3 
					(-plItemList[plFlagIndex].anchPos + xWidth, yPos1, 0);

				playerArrPrev.sprite = grayArrow;
				playerArrPrev.GetComponent<Button>().interactable = false;
				if (plItemList[plFlagIndex].numOfPlayers == 1)
				{
					playerArrNext.sprite = grayArrow;
					playerArrNext.GetComponent<Button>().interactable = false;
				}
				else
				{
					playerArrNext.sprite = orangeArrow;
					playerArrNext.GetComponent<Button>().interactable = true;
				}

				isChange = true;

				PlayerPrefs.SetInt("PlayerFlagIndex", plFlagIndex);
				PlayerPrefs.SetInt("PlayerIndexFP", 0);
			}

			if (plFlArrPrev.sprite == grayArrow)
			{
				plFlArrPrev.sprite = orangeArrow;
				plFlArrPrev.GetComponent<Button>().interactable = true;
			}

			//Debug.Log("Player Flag Index = " + plFlagIndex);
			//Debug.Log("Player Index = " + playerIndex);
		} 
		else if (cPan == playerContPan) 
		{
			elWidth = playerContScr.GetComponent<RectTransform> ().rect.width;
			float xPos = playerContPan.anchoredPosition.x;
			float yPos = playerContPan.anchoredPosition.y;

			if (playerIndex < plItemList[plFlagIndex].numOfPlayers - 1)
			{
				playerIndex ++;
				playerContPan.anchoredPosition = new Vector3 (xPos - elWidth, yPos, 0);

				isChange = true;

				PlayerPrefs.SetInt("PlayerIndexFP", playerIndex);
			}

			if (playerIndex > plItemList[plFlagIndex].numOfPlayers - 2)
			{
				playerArrNext.sprite = grayArrow;
				playerArrNext.GetComponent<Button>().interactable = false;
			}

			if (playerArrPrev.sprite == grayArrow)
			{
				playerArrPrev.sprite = orangeArrow;
				playerArrPrev.GetComponent<Button>().interactable = true;
			}

			Debug.Log("Player Index = " + playerIndex);
		} 
	}

	public void PrevElement(RectTransform cPan)
	{
		float elWidth = 0;

		if (cPan == plFlagContPan) 
		{
			elWidth = plFlagContScr.GetComponent<RectTransform> ().rect.width;
			float xPos = plFlagContPan.anchoredPosition.x;
			float yPos = plFlagContPan.anchoredPosition.y;

			if ((int)xPos >= (int)-elWidth)
			{
				plFlArrPrev.sprite = grayArrow;
				plFlArrPrev.GetComponent<Button>().interactable = false;
			}

			if (xPos < 0)
			{
				playerIndex = 0;
				plFlagContPan.anchoredPosition = new Vector3 (xPos + elWidth, yPos, 0);
				plFlagIndex --;

				float xWidth = playerContScr.GetComponent<RectTransform>().rect.width;
				float yPos1 = playerContPan.anchoredPosition.y;

				playerContPan.anchoredPosition = new Vector3 
					(-plItemList[plFlagIndex].anchPos + xWidth, yPos1, 0);
				playerArrPrev.sprite = grayArrow;
				playerArrPrev.GetComponent<Button>().interactable = false;

				if (plItemList[plFlagIndex].numOfPlayers == 1)
				{
					playerArrNext.sprite = grayArrow;
					playerArrNext.GetComponent<Button>().interactable = false;
				}
				else
				{
					playerArrNext.sprite = orangeArrow;
					playerArrNext.GetComponent<Button>().interactable = true;
				}

				isChange = true;

				PlayerPrefs.SetInt("PlayerFlagIndex", plFlagIndex);
				PlayerPrefs.SetInt("PlayerIndexFP", 0);
			}

			if (plFlArrNext.sprite == grayArrow)
			{
				plFlArrNext.sprite = orangeArrow;
				plFlArrNext.GetComponent<Button>().interactable = true;
			}

			Debug.Log("Player Flag Index = " + plFlagIndex);
			Debug.Log("Player Index = " + playerIndex);
		} 
		else if (cPan == playerContPan) 
		{
			elWidth = playerContScr.GetComponent<RectTransform> ().rect.width;
			float xPos = playerContPan.anchoredPosition.x;
			float yPos = playerContPan.anchoredPosition.y;

			if (playerIndex > 0)
			{
				playerIndex --;
				playerContPan.anchoredPosition = new Vector3 (xPos + elWidth, yPos, 0);

				isChange = true;

				PlayerPrefs.SetInt("PlayerIndexFP", playerIndex);
			}

			if (playerIndex < 1)
			{
				playerArrPrev.sprite = grayArrow;
				playerArrPrev.GetComponent<Button>().interactable = false;
			}

			if (playerArrNext.sprite == grayArrow)
			{
				playerArrNext.sprite = orangeArrow;
				playerArrNext.GetComponent<Button>().interactable = true;
			}
			Debug.Log("Player Index = " + playerIndex);
		} 
	}	

	public void SetPlayerNotFP()
	{
		int i = scr.alPrScr.playerIndex;

		scr.alScr.playerFlag = scr.prScrL.itemList[i].flag;
		scr.alScr.playerName = scr.prScrL.itemList[i].name;
		scr.alScr.playerName0 = scr.prScrL.itemList[i].name0;
		scr.alScr.playerSprite = scr.prScrL.itemList[i].icon;
	}
}


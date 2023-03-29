using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class LevelStadiumItem {
	public Sprite icon;
	[HideInInspector]
	public int isOpened;
	public Button.ButtonClickedEvent thingToDo;
}

public class LevelStadiumScrollList : MonoBehaviour
{
	public Scripts scr;

	public GameObject loadingText;
	public GameObject chooseButLeft, chooseButRight;
	public Text numberOfStad;
	public Button nextButton, prevButton;
	public ScrollRect scrRect;
	public Sprite orangeArrow, magentaArrow, grayArrow;
	public RectTransform contentPanel;
	public GameObject sampleButton;
	public List<LevelStadiumItem>itemList;
	private float width, minWidth;
	private int number;
	private GameObject[] stadButtons;


	void Awake()
	{
		scr.objLev.loadingPanel.SetActive(true);

		if (scr.alPrScr.isLeaderboardGame == 1)
		{
			chooseButLeft.SetActive (false);
			chooseButRight.SetActive (false);
			numberOfStad.enabled = false;
			loadingText.SetActive (false);
		}
			
		minWidth = sampleButton.GetComponent<LayoutElement>().minWidth;
		width = scrRect.GetComponent<RectTransform>().rect.width;
		DestroyEditorButtons();
		SetLocks();
		PopulateList();
		stadButtons = GameObject.FindGameObjectsWithTag("StadiumButton");
		SetRandomStadium();

		if (scr.alPrScr.isLeaderboardGame == 1)
			scr.objLev.playButton.onClick.Invoke();
	}

	void DestroyEditorButtons()
	{
		GameObject[] editorButtons = GameObject.FindGameObjectsWithTag("StadiumButton");

		foreach (var item in editorButtons)
			DestroyImmediate(item);
	}

	void SetLocks()
	{
		for (int i = 0; i < itemList.Count; i++)
			itemList[i].isOpened = scr.alPrScr.openedStadiums[i];
	}

	void PopulateList()
	{
		for (int i = 0; i < itemList.Count; i++)
		{
			if (itemList[i].isOpened==1)
			{
				GameObject newButton = Instantiate(sampleButton) as GameObject;
				LevelStadiumSampleButton button = newButton.GetComponent<LevelStadiumSampleButton>();
				button.stadiumIcon.sprite = itemList[i].icon;
				button.stadNumber = i;
				newButton.transform.SetParent (contentPanel);
			}
		}
	}

	public void NextStadium()
	{
		number = Mathf.RoundToInt(-contentPanel.anchoredPosition.x/minWidth);
		//float var = -contentPanel.anchoredPosition.x/minWidth;
		int stadNum = stadButtons[number + 1].GetComponent<LevelStadiumSampleButton>().stadNumber;

		if(!prevButton.interactable)
		{
			prevButton.interactable = true;
			prevButton.gameObject.GetComponent<Image>().sprite = orangeArrow;
		}

		if (number < stadButtons.Length - 1)
		{
			scr.stChScr.SetStadiumHandleByNumber(stadNum);
			float posX = contentPanel.anchoredPosition.x;
			float posY = contentPanel.anchoredPosition.y;
			contentPanel.anchoredPosition = new Vector3(posX - width, posY, 0);
		}

		if (number > stadButtons.Length - 3)
		{
			nextButton.interactable = false;
			nextButton.gameObject.GetComponent<Image>().sprite = grayArrow;
		}

		int stadPlusOne = stadNum + 1;
		numberOfStad.text = stadPlusOne + "/" + stadButtons.Length;
	}

	public void PreviousStadium()
	{
		number = Mathf.RoundToInt(-contentPanel.anchoredPosition.x/minWidth);
		//float var = -contentPanel.anchoredPosition.x/minWidth;
		int stadNum = stadButtons[number - 1].GetComponent<LevelStadiumSampleButton>().stadNumber;

		if(!nextButton.interactable)
		{
			nextButton.interactable = true;
			nextButton.gameObject.GetComponent<Image>().sprite = orangeArrow;
		}

		if (number > 0)
		{
			scr.stChScr.SetStadiumHandleByNumber(stadNum);
			float posX = contentPanel.anchoredPosition.x;
			float posY = contentPanel.anchoredPosition.y;
			contentPanel.anchoredPosition = new Vector3(posX + width, posY, 0);
		}

		if (number < 2)
		{
			prevButton.interactable = false;
			prevButton.gameObject.GetComponent<Image>().sprite = grayArrow;
		}

		int stadPlusOne = stadNum + 1;
		numberOfStad.text = stadPlusOne + "/" + stadButtons.Length;
	}

	private void SetRandomStadium()
	{
		Debug.Log("Min Width = " + minWidth);
		Debug.Log("Width = " + width);

		float randStad0 = Random.value * (stadButtons.Length - 0.01f);
		int randStad = (int)randStad0;

		Debug.Log("Rand stadium = " + randStad);

		int i = 0;
		while (i < randStad)
		{
			i++;
			NextStadium();
		} 

		int stadPlusOne = randStad + 1;
		numberOfStad.text = stadPlusOne + "/" + stadButtons.Length;
	}
}

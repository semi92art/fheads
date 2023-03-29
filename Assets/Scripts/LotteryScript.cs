using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class LotteryItem
{
	public Image _image;
	public Image _image3;
	public float zAngle;
	public int index;
	public int type;
	[HideInInspector]
	public Transform _imageTr;
}

public class LotteryScript : MonoBehaviour
{
	private Scripts scr;
	public Animator previewAnim;
	public Image previewIm, previewIm3;
	public Text previewText;
	public Animator lotteryArrowAnim;
	public Sprite lottButt2Spr, lottButt2Pushed;
	public GameObject lotteryPanel;
	public Button lotteryButton, lotteryButton2;
	public Text numberText;
	public RectTransform fortuneWheelRectTr;
	public Sprite emptySpr;
	private int currInd;
	private int type_1;
	private bool isRotate, stopRotate;
	private int lotteryTimer;
	private Transform lotteryPanTr;

	public List<LotteryItem> lotteryList;

	private bool[] lockedPlayersMain = new bool[60];
	private bool[] lockedStadiums = new bool[18];

	public bool menuLottery;
	public bool checkLottery;

	private bool correctRotation;
	[HideInInspector]
	public int unlocked_ind, unlocked_ind_Prof;
	private bool unlocked;
	[HideInInspector]
	public int typeForUnlock;


	void Awake()
	{
		scr = FindObjectOfType<Scripts> ();

		numberText.text = scr.alPrScr.lotteryCount.ToString ();
		lotteryPanTr = fortuneWheelRectTr.transform;

		if (scr.alPrScr.lotteryCount == 0) 
			lotteryButton.interactable = false;

		for (int i = 0; i < lotteryList.Count; i++)
			lotteryList [i]._imageTr = lotteryList [i]._image.transform;

		lotteryPanel.SetActive (false);
	}

	void Update()
	{
		RotateWheel ();

		if (checkLottery && !menuLottery) 
		{
			previewIm.sprite = lotteryList [unlocked_ind]._image.sprite;
			previewIm3.sprite = lotteryList [unlocked_ind]._image3.sprite;
			previewAnim.gameObject.SetActive(true);
			previewAnim.SetBool ("call", true);
		}

		if (Time.frameCount % 2 == 0 && lotteryPanel.activeSelf)
		{
			for (int i = 0; i < lotteryList.Count; i++)
			{
				if (lotteryList[i]._imageTr.position.y > lotteryPanTr.position.y - 70.0f)
				{
					lotteryList [i]._image.enabled = true;
					lotteryList [i]._image3.enabled = true;
				}
				else
				{
					lotteryList [i]._image.enabled = false;
					lotteryList [i]._image3.enabled = false;
				}
			}
		}

		checkLottery = menuLottery;
	}
		
	public void LotteryPlusOne()
	{
		scr.alPrScr.lotteryCount ++;
		scr.alPrScr.isChange0 = true;
		numberText.text = scr.alPrScr.lotteryCount.ToString ();
		lotteryButton.interactable = true;
	}

	private void SetRandomItems()
	{
		currInd = 0;

		while(currInd < lotteryList.Count)
		{
			ChoosePlayer ();
			currInd ++;
		}
	}

	private void ChoosePlayer()
	{
		for (int j = 0; j < 10000; j++) 
		{
			int randInd = Mathf.FloorToInt (Random.value * (float)lockedPlayersMain.Length);
			Debug.Log(currInd + ";" + randInd + ";" + j);

			if (lockedPlayersMain[randInd])
			{
				lockedPlayersMain [randInd] = false;
				lotteryList [currInd]._image.sprite = scr.prScrL.itemList [randInd].icon;
				lotteryList [currInd].index = randInd;
				lotteryList [currInd].type = 0;
				lotteryList [currInd]._image3.sprite = scr.prScrL.itemList[randInd].flag;
				break;
			}
		}
	}
		
	private void ChooseStadium()
	{
		for (int j = 0; j < 100000; j++) 
		{
			int randInd = Mathf.FloorToInt (Random.value * lockedStadiums.Length);
			Debug.Log ("Stadiums Index = " + randInd);

			if (lockedStadiums[randInd])
			{
				lockedStadiums [randInd] = false;
				lotteryList [currInd]._image.sprite = scr.stScrL.itemList [randInd].icon;
				lotteryList [currInd].index = randInd;
				lotteryList [currInd].type = 2;
				currInd ++;
				break;
			}
		}
	}
		
	private void GetLockedItems()
	{
		for (int i = 0; i < scr.prScrL.itemList.Count; i++) 
		{
			if (scr.prScrL.itemList [i].isOpened == 0)
				lockedPlayersMain [i] = true;
		}

		for (int i = 0; i < scr.stScrL.itemList.Count; i++) 
		{
			if (scr.stScrL.itemList [i].isOpened == 0)
				lockedStadiums [i] = true;
		}
	}

	public void StartRotateWheel()
	{
		isRotate = !isRotate;

		if (isRotate)
			lotteryButton2.GetComponent<Image> ().sprite = lottButt2Pushed;
		else
		{
			lotteryButton2.GetComponent<Image> ().sprite = lottButt2Spr;
			stopRotate = true;
		}
			
	}

	public void LotteryPreviewBack()
	{
		previewAnim.SetBool ("call", false);
		previewAnim.gameObject.SetActive (false);
		fortuneWheelRectTr.rotation = Quaternion.Euler(0 ,0, 0);
		lotteryArrowAnim.gameObject.SetActive(false);

		if (scr.alPrScr.lotteryCount == 0) 
			lotteryButton.interactable = false;
	}

	public void LotteryPanelCall()
	{
		scr.gM.MainMenuBools.menuLottery = true;
		menuLottery = true;
		lotteryPanel.SetActive(true);
		lotteryPanel.GetComponent<Animator>().SetTrigger("call");
		scr.alPrScr.lotteryCount --;
		scr.alPrScr.isChange0 = true;
		numberText.text = scr.alPrScr.lotteryCount.ToString();

		currInd = 0;
		GetLockedItems ();
		SetRandomItems ();
	}

	public void LotteryPanelBack()
	{
		scr.gM.MainMenuBools.menuLottery = false;
		lotteryPanel.GetComponent<Animator>().SetTrigger("back");
		lotteryArrowAnim.SetTrigger ("back");
		lotteryPanel.SetActive(false);
	}

	private void RotateWheel()
	{
		if (isRotate) 
		{
			if (lotteryTimer < 200)
				lotteryTimer ++;

			if (lotteryTimer < 200)
				fortuneWheelRectTr.localRotation = Quaternion.Euler (0,0,
					fortuneWheelRectTr.localRotation.eulerAngles.z + (float)lotteryTimer * Time.deltaTime);
			else
				fortuneWheelRectTr.localRotation = Quaternion.Euler (0,0,
					fortuneWheelRectTr.localRotation.eulerAngles.z + 200.0f * Time.deltaTime);
		}
		else
		{
			if (lotteryTimer > 0) 
			{	
				if (Time.frameCount % 3 == 0)
					lotteryTimer--;

				fortuneWheelRectTr.localRotation = Quaternion.Euler (0,0,
					fortuneWheelRectTr.localRotation.eulerAngles.z + (float)lotteryTimer * Time.deltaTime);
			}
			else
			{
				if (stopRotate)
				{
					LotteryResult();
					stopRotate = false;
				}

				if (correctRotation) 
				{
					float diff = -fortuneWheelRectTr.localRotation.eulerAngles.z + lotteryList [unlocked_ind].zAngle;

					if (Mathf.Abs(diff) > 0.1f) 
					{
						fortuneWheelRectTr.localRotation = Quaternion.Euler (0, 0,
							fortuneWheelRectTr.localRotation.eulerAngles.z + diff * 0.05f);
					} 
					else
					{
						correctRotation = false;
						menuLottery = false;
					}
				}
			}
		}
	}
		
	private void LotteryResult()
	{
		float lotPosY = -50000.0f;
		int currInd_0 = 0;

		for (int i = 0; i < lotteryList.Count; i++)
		{
			if (lotteryList[i]._image.transform.position.y > lotPosY)
			{
				lotPosY = lotteryList[i]._image.transform.position.y;
				currInd_0 = i;
			}
		}

		unlocked_ind = currInd_0;
		unlocked_ind_Prof = lotteryList [currInd_0].index;
		correctRotation = true;
		UnlockLot(currInd_0);
	}
		
	private void UnlockLot(int ind)
	{
		if (lotteryList[ind].type == 0)
			typeForUnlock = 0;
		else if (lotteryList[ind].type == 1)
			typeForUnlock = 1;
		else if (lotteryList[ind].type == 2)
			typeForUnlock = 2;
	}


}

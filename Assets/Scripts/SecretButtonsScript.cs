using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SecretButtonsScript : MonoBehaviour 
{
	public Scripts scr;

	public GameObject[] objectsToEnable;

	public Text summPriceText;
	public Text summAwardText;
	public string requiredCombination;
	public string combinationToEnable;
	public string onlineConbination = "HOTBA";
	public string combination;

	void Awake()
	{
		for (int i = 0; i < objectsToEnable.Length; i++)
			objectsToEnable [i].SetActive (false);

		for (int i = 0; i < objectsToEnable.Length; i++)
			objectsToEnable [i].SetActive (false);
	}

	void Update()
	{
		if (combination == requiredCombination)
		{
			Debug.Log ("All content is unlocked!");
			scr.alPrScr.trn1 = 11;
			scr.alPrScr.unlock = 1;

			for (int i = 0; i < scr.alPrScr.openedPlayers.Length; i++)
				scr.alPrScr.openedPlayers [i] = 1;

			for (int i = 0; i < scr.alPrScr.openedStadiums.Length; i++)
				scr.alPrScr.openedStadiums [i] = 1;
			
			for (int i = 0; i < scr.alPrScr.openedBalls.Length; i++)
				scr.alPrScr.openedBalls [i] = 1;

			scr.alPrScr.isChange0 = true;
			combination = "";
		}
		else if (combination == combinationToEnable) 
		{
			Debug.Log ("Combination To Enable");

			for (int i = 0; i < objectsToEnable.Length; i++)
				objectsToEnable [i].SetActive (true);

			combination = "";
		}
	}

	public void F_Button()
	{
		combination += "F";
		Debug.Log(combination);
	}

	public void O_Button()
	{
		combination += "O";
		Debug.Log(combination);
	}

	public void T_Button()
	{
		combination += "T";
		Debug.Log(combination);
	}

	public void B_Button()
	{
		combination += "B";
		Debug.Log(combination);
	}

	public void A_Button()
	{
		combination += "A";
		Debug.Log(combination);
	}

	public void L_Button()
	{
		combination += "L";
		Debug.Log(combination);
	}

	public void H_Button()
	{
		combination += "H";
		Debug.Log(combination);
	}

	public void E_Button()
	{
		combination += "E";
		Debug.Log(combination);
	}
		
	public void D_Button()
	{
		combination += "D";
		Debug.Log(combination);
	}

	public void S_Button()
	{
		combination += "S";
		Debug.Log(combination);
	}

	public void Clear_Button()
	{
		combination = "";
		Debug.Log("Combination cleared!");
	}


	public void CalculateSummPrice()
	{
		int summ1 = 0;
		int summ2 = 0;
		int summ3 = 0;
		int finalSumm = 0;

		for (int i = 0; i < scr.prScrL.itemList.Count; i++) 
			summ1 += scr.prScrL.itemList[i].moneyCoast;

		for (int i = 0; i < scr.stScrL.itemList.Count; i++) 
			summ2 += scr.stScrL.itemList[i].moneyCoast;

		finalSumm = summ1 + summ2 + summ3;
		summPriceText.text = finalSumm + "";
	}

	public void CalculateSummAward()
	{
		int summ1 = 0;

		foreach (var item in scr.carScrL.itemList) 
			summ1 += item.award;

		summAwardText.text = summ1 + "";
	}

}

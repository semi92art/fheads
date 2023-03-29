using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayoffSaveItem
{
	public Sprite imageSprite;
	public int goals;
	public string name;
	public Sprite flag;
}

public class PlayoffPlayerListSave : MonoBehaviour
{
	private Scripts scr;

	public int[] randWin, randGoals, randDiff;

	void Awake()
	{
		scr = FindObjectOfType<Scripts> ();

		switch (scr.alPrScr.plfGames)
		{
		case 0:
			for (int i = 0; i < randWin.Length; i++)
			{
				randWin[i] = Mathf.RoundToInt(Random.value);
				randGoals[i] = Mathf.RoundToInt(Random.value);
				randDiff[i] = Mathf.RoundToInt(1 + 2 * Random.value);
			}
			break;
		case 1:
			int ind  = randWin.Length - 1;
			randWin[ind] = Mathf.RoundToInt(Random.value);
			randGoals[ind] = Mathf.RoundToInt(Random.value);
			randDiff[ind] = Mathf.RoundToInt(1 + 2 * Random.value);
			break;
		}
	}
}

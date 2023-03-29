using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PlayoffPlayerList : MonoBehaviour 
{
	private Scripts scr;

	public bool stopSelect;

	private float random1, random2, random3;
	private int timer;
	private int[] random = new int[8];

	void Awake()
	{
		scr = FindObjectOfType<Scripts> ();

		DontDestroyOnLoad (transform.gameObject);

		if (SceneManager.GetActiveScene().name != "MainMenu") 
			RandomSelect ();
		else
			DestroyImmediate (gameObject);
	}
	
	public void RandomSelect()
	{
		if (scr.alPrScr.plfGames == 0)
		{
			if(scr.alPrScr.finishTourn == "CupGoes")
			{
				if (!stopSelect) 
				{
					for (int i = 0; i < scr.alScr.varList.Count; i++)
					{
						Debug.Log("i = " + i);
						Debug.Log("index = " + scr.alScr.varList[i].index);
					}
						
					for (int i = 0; i < 8; i++)
					{
						if(i != 0 && i != 4)
						{
							int m = (int)(Random.value * (scr.alScr.varList.Count - 1));
							random[i] = scr.alScr.varList[m].index;
							scr.alScr.varList.RemoveAt(m);
							scr.alPrScr.playoffPlayers[i] = random[i];
						} 
						else if (i == 0)
						{
							for (int j = 0; j < scr.prScrL.itemList.Count; j++)
							{
								if (scr.prScrL.itemList[j].icon == scr.alScr.playerSprite)
									scr.alPrScr.playoffPlayers[i] = j;
							}
						}
						else if (i == 4)
						{
							for (int j = 0; j < scr.prScrL.itemList.Count; j++)
							{
								if (scr.prScrL.itemList[j].icon == scr.alScr.secondWinnerSprite)
									scr.alPrScr.playoffPlayers[i] = j;
							}
						}
					}
					stopSelect = true;
				}
				scr.alPrScr.finishTourn = "PlayoffGoes";
			}
		}

		scr.alPrScr.isChange0 = true;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChampInfoPanelManager : MonoBehaviour 
{
	public Scripts scr;

	public Text finalPlaceText;

	private const int size = 10;
	private const int size1 = 5;
	//[HideInInspector]
	public int[] player2;

	public Image playerFlag, enemyFlag;
	public Text resText, resTable0, resTable1, resTable2;
	public Text nextTable0, nextTable1;
	public Image playerImage, enemyImage;
	public Text playerName, enemyName;
	public Text vsText, nextTourText, tireText;
	public string[] player1Names, player2Names;
	[HideInInspector]
	public string[] player1Score;
	[HideInInspector]
	public string[] player2Score;
	[HideInInspector]
	public int var1, var2;


	void Awake()
	{
		finalPlaceText.gameObject.SetActive (false);

		player1Names = new string[size1];
		player2Names = new string[size1];
		player1Score = new string[size1];
		player2Score = new string[size1];
		var1 = -1;
		var2 = -1;

		resTable1.text = "";
		nextTable0.text = "";
		nextTable1.text = "";
	}

	private void SetPlayerMain()
	{
		playerName.text = scr.prScrL.itemList[scr.alPrScr.playerIndex].name0;
		playerImage.sprite = scr.prScrL.itemList[scr.alPrScr.playerIndex].icon;
		playerFlag.sprite = scr.prScrL.itemList[scr.alPrScr.playerIndex].flag;
	}
		
	public void SetInfoPanel()
	{
		if (scr.chMan.isSetImages)
		{
			for (int i = 0; i < scr.chLIm.itemList.Count; i++)
			{
				if (player2[i] != 0)
				{
					if (var1 < player1Names.Length - 1)
					{
						var1++;
						if (!scr.chLIm.endOfChamp)
						{
							player1Names[var1] = scr.chLIm.itemList[i].Name0;
							player2Names[var1] = scr.chLIm.itemList[player2[i]].Name0;

							int i2 = scr.alPrScr.enemyIndex;
							SetPlayerMain();

							enemyName.text = scr.prScrL.itemList[i2].name0;
							enemyImage.sprite = scr.prScrL.itemList[i2].icon;
							enemyFlag.sprite = scr.prScrL.itemList[i2].flag;

							scr.alScr.enemyName = scr.prScrL.itemList[i2].name;
							scr.alScr.enemyName0 = scr.prScrL.itemList[i2].name0;
							scr.alScr.enemySprite = scr.prScrL.itemList[i2].icon;
							scr.alScr.enemyFlag = scr.prScrL.itemList[i2].flag;
						} 
						else 
						{
							GameObject[] infoElements = GameObject.FindGameObjectsWithTag("ChampInfoUI");

							foreach (var item in infoElements)
							{
								if (item.name == "PlayerImage" || item.name == "EnemyImage" ||
								    item.name == "PlayerFlag" || item.name == "EnemyFlag" ||
								    item.name == "PlayerFlagShowcase" || item.name == "EnemyFlagShowcase")
								{
									item.GetComponent<Image>().color = Color.clear;
								} 
								else 
									item.GetComponent<Text>().color = Color.clear;
							}
						}
					}
				}

				if (player2[i] != 0)
				{
					if (var2 < player1Names.Length - 1)
					{
						var2++;
						player1Score[var2] = "" + scr.alPrScr.chCurrG[i];
						player2Score[var2] = "" + scr.alPrScr.chCurrG[player2[i]];
					}
				}
			}

			for (int i = 0; i < size1; i++)
			{
				if (scr.alPrScr.champGames == 0 && scr.alPrScr.isSecTour == 0)
					resTable1.text += " : \n";
				else 
					resTable1.text += player1Score[i] + ":" + player2Score[i] +  "\n";
					
				nextTable0.text += player1Names[i] + "\n";
				nextTable1.text += player2Names[i] + "\n";

				if (scr.chLIm.endOfChamp)
				{
					nextTable0.color = Color.clear;
					nextTable1.color = Color.clear;
					gameObject.GetComponent<Image>().color = Color.clear;
				}
			}
				
			if (!(scr.alPrScr.champGames == 0 && scr.alPrScr.isSecTour == 0))
			{
				resTable0.text = nextTable0.text;
				resTable2.text = nextTable1.text;
			}
				
			scr.alPrScr.rndT1 = nextTable0.text;
			scr.alPrScr.rndT2 = nextTable1.text;
			scr.alPrScr.isChange0 = true;
			scr.chMan.isSetImages = false;
		}
	}
}

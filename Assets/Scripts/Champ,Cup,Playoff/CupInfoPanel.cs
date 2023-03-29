using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupInfoPanel : MonoBehaviour {

	public Scripts scr;

	public Image playerImage, enemyImage;
	public Text playerText, enemyText;
	private int timer;

	void Awake()
	{

	}

	void Update()
	{
		timer++;
		if (timer == 1) 
		{
			playerImage.sprite = scr.alScr.playerSprite;
			enemyImage.sprite = scr.alScr.enemySprite;
			playerText.text = scr.alScr.playerName0;
			enemyText.text = scr.alScr.enemyName0;
		} 
		else if (timer > 1)
		{
			//gameObject.GetComponent<CupInfoPanel>().enabled = false;
		}
	}
}

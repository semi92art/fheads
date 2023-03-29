using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour{

	public Text enemyScore;
	public Text playerScore;

	public Scripts scr;

	public static int score;
	public static int score1;
	public static Text text;
	[HideInInspector]
	public bool isTechnicalDefeat;

	void Awake ()
	{
		text = GetComponent <Text> ();
		score = 0;
		score1 = 0;
		SetScore ();
	}

	public void SetScore()
	{
		playerScore.text = "" + score;
		enemyScore.text = "" + score1;
	}

	public void TechnicalDefeat()
	{
		Time.timeScale = 1;

		if (!scr.congrPan.congrPanel.activeSelf)
			scr.congrPan.congrPanel.SetActive(true);
		
		score = 0;
		score1 = 3;
		scr.tM.time0 = -1;
		isTechnicalDefeat = true;
		TimeManager.resultOfGame = 3;
		SetScore ();
	}
}

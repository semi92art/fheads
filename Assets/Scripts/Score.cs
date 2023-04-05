using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    [SerializeField]
	private Scripts scr;

	public Text enemyScore, enSc1;
	public Text playerScore, plSc1;

	public static int score;
	public static int score1;

	void Awake ()
	{
        score1 = 0;
		score = 0;

		SetScore ();
	}

	public void SetScore()
	{
		playerScore.text = "" + score;
		enemyScore.text = "" + score1;
        
        plSc1.text = "" + score;
        enSc1.text = "" + score1;

        System.GC.Collect();
	}
}

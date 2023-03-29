using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChampPlayer : MonoBehaviour {

	public Image showcase;
	public Image playerImage;
	public Text playerName;
	public Image flag;
	public Text playerGames;
	public Text playerWins;
	public Text playerTies;
	public Text playerLoses;
	public Text playerGoalsDiff;
	public Text playerPoints;
	public Image pGamesImage;
	public Image pWinsImage;
	public Image pTiesImage;
	public Image pLosesImage;
	public Image pGoalsDiffImage;
	public Image pPointsImage;
	public Animator pAnim;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
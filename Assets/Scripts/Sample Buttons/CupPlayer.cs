using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CupPlayer : MonoBehaviour {
	public Image flag;
	public Image showcase;
	public Image playerImage;
	public Text playerName, playerGames, playerWins, playerTies, playerLoses, playerGoalsDiff, playerPoints;
	public Image pGamesImage, pWinsImage, pTiesImage, pLosesImage, pGoalsDiffImage, pPointsImage;
	public Animator playerAnim;

	void Start(){
		transform.localScale = new Vector3 (1, 1, 1);
	}
}
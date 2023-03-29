using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GoalPanelScript : MonoBehaviour {

	public Scripts scr;

	public Animator goalPanelAnimator;

	public Text enemyScore;
	public Text playerScore;
	
	private int timer;
	private float timer1;

	void Awake(){

	}

	void Update () {
		if (scr.tM.isBetweenTimes){
			timer1+=Time.deltaTime;
			if (timer1 > PlayerMovement.restartDelay1 + 1){
				scr.tM.isBetweenTimes = false;
				timer1 = 0;
			}
		}
		if (scr.pMov.restart && !scr.tM.isBetweenTimes) {
			timer++;
			if (!TimeManager.isEndOfTime){
				if (timer == 1) {
					goalPanelAnimator.SetTrigger ("call");
					enemyScore.text = "" + Score.score1;
					playerScore.text = "" + Score.score;
				}
			}
		} else {
			timer = 0;
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallengesManager : MonoBehaviour {

	public Scripts scr;

	public RectTransform contPanel;
	public ScrollRect challScrollRect;
	public string lockString;
	public int timerEnd = 150;
	public int timer;
	public Sprite prevSpriteDefault;
	public string[] prStr;
	public Sprite[] prSpr;
	public int[] challNum;

	private GameObject[] challButtons;

	void Awake(){

	}
	


	public void ChallEasy(){
		challButtons = scr.chScrL.challButtons;
		for (int i = 0; i < challButtons.Length; i++){
			if (i>=10){
				challButtons[i].SetActive(false);
			} else {
				challButtons[i].SetActive(true);
			}
		}
		contPanel.anchoredPosition = Vector3.zero;
	}

	public void ChallMedium(){
		challButtons = scr.chScrL.challButtons;
		for (int i = 0; i < challButtons.Length; i++){
			if (i<10 || i>=20){
				challButtons[i].SetActive(false);
			} else {
				challButtons[i].SetActive(true);
			}
		}
		contPanel.anchoredPosition = Vector3.zero;
	}

	public void ChallHard(){
		challButtons = scr.chScrL.challButtons;
		for (int i = 0; i < challButtons.Length; i++){
			if (i<20){
				challButtons[i].SetActive(false);
			} else {
				challButtons[i].SetActive(true);
			}
		}
		contPanel.anchoredPosition = Vector3.zero;
	}
}

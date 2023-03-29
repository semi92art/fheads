using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChallPreviewManager : MonoBehaviour {
	public GameObject playButton, backButton;
	public Animator prevAnim;
	private float timer;

	void Awake(){
		playButton.SetActive (false);
		backButton.SetActive (false);
	}

	void Update(){
		if (prevAnim.GetBool ("call").Equals (true)) {
			timer += Time.deltaTime;
			if (timer > 0.66f) {
				playButton.SetActive (true);
				backButton.SetActive (true);
			}
		} else {
			playButton.SetActive (false);
			backButton.SetActive (false);
			timer = 0;
		}
	}


}

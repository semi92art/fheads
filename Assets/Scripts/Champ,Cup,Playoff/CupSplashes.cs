using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CupSplashes : MonoBehaviour {
	[Header("If left, = true, if right, = false")]
	public bool leftOrRight;
	private int LOR;
	public float cosEdge, period, sdv, randAmplX, randAmplY;
	private int timer, timer1;
	private Image splash;
	private float cosVal;
	private float xPos, yPos;
	void Start(){
		if (leftOrRight) {
			LOR = -1;
		} else {
			LOR = 1;
		}

		if (SceneManager.GetActiveScene().name != "Level"){
			splash = GetComponent<Image> ();
			xPos = gameObject.GetComponent<RectTransform>().anchoredPosition.x;
			yPos = gameObject.GetComponent<RectTransform>().anchoredPosition.y;
		} else {
			xPos = transform.localPosition.x;
			yPos = transform.localPosition.y;
		}


	}

	void Update(){
		timer++;
		cosVal = Mathf.Cos (timer / period + sdv);
		if (cosVal>cosEdge) {
			timer1++;

			if (SceneManager.GetActiveScene().name != "Level"){
				splash.color = new Vector4 (1, 1, 1, 0.6f);
			} else {
				gameObject.GetComponent<SpriteRenderer>().color = new Vector4 (1, 1, 1, 0.6f);
			}


			if(timer1==1){
				if (SceneManager.GetActiveScene().name != "Level"){
					gameObject.GetComponent<RectTransform> ().anchoredPosition =
						new Vector3 (xPos + Random.value * randAmplX * LOR, yPos + Random.value * randAmplY, 0);
				} else {
					transform.localPosition = 
						new Vector3 (xPos + Random.value * randAmplX * LOR, yPos + Random.value * randAmplY, 0);
				}
			}

		} else {
			timer1 = 0;
			if (SceneManager.GetActiveScene().name != "Level"){
				splash.color = Color.clear;
			} else {
				gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
			}
		}
	}
}

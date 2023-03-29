using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPContPanelScript : MonoBehaviour {
	public RectTransform contPanelRect;
	public float sdvigX, sdvigY;
	public bool doIt;

	private int timer;

	void Update(){
		timer++;
		if (timer.Equals (2)) {
			if (doIt) {
				contPanelRect.anchoredPosition = new Vector3(sdvigX, sdvigY, 0);
			}
			gameObject.GetComponent<FPContPanelScript>().enabled = false;
		}
	}
}

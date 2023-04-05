using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayButtonScript : MonoBehaviour {
	public Sprite spr1Down, spr1Up;
	public Image img;

	public void ButtonDown(){
		img.sprite = spr1Down;
	}

	public void ButtonUp(){
		img.sprite = spr1Up;
	}
}

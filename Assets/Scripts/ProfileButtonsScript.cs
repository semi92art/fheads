using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ProfileButtonsScript : MonoBehaviour {

	public Scripts scr;

	public Sprite spriteUp, spriteDown;
	public GameObject topPanelShadow;

	public void ButtonUp(Image buttonIm)
	{
		buttonIm.sprite = spriteUp;
	}
	public void ButtonDown(Image buttonIm)
	{
		buttonIm.sprite = spriteDown;
	}
}

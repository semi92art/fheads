using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveLeftButtonScr : MonoBehaviour 
{
	public Scripts scr;

	public GameObject stateGO;
	public Image butLeftIm, butRightIm;
	public Sprite arrow, arrowPushed;

	private int timer;

	void Update()
	{
		if (stateGO.activeSelf) 
		{
			butLeftIm.sprite = arrowPushed;
			butRightIm.sprite = arrow;
			scr.pMov.MoveLeft ();

			timer = 0;
		} 
		else 
		{
			timer++;

			if (timer == 1)
			{
				butLeftIm.sprite = arrow;
				scr.pMov.MoveLeftEnd ();
			}
		}
	}
}

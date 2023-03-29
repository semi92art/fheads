using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MoveRightButtonScr : MonoBehaviour 
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
			butLeftIm.sprite = arrow;
			butRightIm.sprite = arrowPushed;
			scr.pMov.MoveRight ();

			timer = 0;
		} 
		else 
		{
			timer++;

			if (timer == 1)
			{
				butRightIm.sprite = arrow;
				scr.pMov.MoveRightEnd ();
			}
		}
	}
}

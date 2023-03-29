using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GrassAnimation : MonoBehaviour {

	public int fixedStep;
	public SpriteRenderer[] grassImage;
	public Sprite[] grassSprite;

	private int 
		timer,
		timer1;

	void FixedUpdate() 
	{
		timer++;

		if (timer == fixedStep) 
		{
			timer1++;
			timer = 0;
			
			if (timer1 == 2) timer1 = 0;
		}

		float rand0 = Random.value;
		float rand1 = Random.value;
		float rand2 = Random.value;
		float rand3 = Random.value;

		grassImage[(int)(rand0 * grassImage.Length)].sprite = grassSprite[timer1];
		grassImage[(int)(rand1 * grassImage.Length)].sprite = grassSprite[timer1];
		grassImage[(int)(rand2 * grassImage.Length)].sprite = grassSprite[timer1];
		grassImage[(int)(rand3 * grassImage.Length)].sprite = grassSprite[timer1];
	}

}

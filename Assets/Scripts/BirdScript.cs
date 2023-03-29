using UnityEngine;
using System.Collections;

public class BirdScript : MonoBehaviour 
{
	public Scripts scr;

	public bool isRight;
	public float speedX, speedY;

	public int[] period;
	public int[] spriteState;
	public SpriteRenderer[] birdSprR;
	public Sprite[] birdSpr;

	private float startPosX, startPosY;
	private float side;
	private int timer;
	
	private bool isBirds;
	private float startBirdsTime;
	private Transform thisTr;

	void Awake()
	{
		thisTr = transform;

		float rand1 = Random.value;

		if (rand1 <= 0.33f)
			startPosY = 4 + 2 * Random.value;
		else if (rand1 > 0.33f && rand1 <= 0.66f)
			startPosY = 6 + 2 * Random.value;
		else
			startPosY = 8 + 2 * Random.value;

		if (Random.value > 0.5f)
			isRight = true;
		else
			isRight = false;


		if (isRight)
		{
			startPosX = -50 + Random.value * 30 - 15;
			side = 1;
			transform.localScale = new Vector3(1, 1, 1);
		}
		else
		{
			startPosX = 10 - Random.value * 30 + 15;
			side = -1;
			transform.localScale = new Vector3(-1, 1, 1);
		}

		thisTr.position = new Vector3(startPosX, startPosY, thisTr.position.z);

		if (Random.value > 0.7f)
			isBirds = true;
		else
			isBirds = false;

		//startBirdsTime = 150 * Random.value;

		if (!isBirds)
			DestroyImmediate(gameObject);
	}

	void Update()
	{
		if (Time.timeScale > 0 && Time.deltaTime > 0)
		{
			timer ++;

			thisTr.position = new Vector3(
				thisTr.position.x + side * speedX * 0.007f,
				thisTr.position.y + speedY * 0.01f,
				thisTr.position.z);
			
			for (int i = 0; i < birdSprR.Length; i++)
			{
				if (timer % period[i] == 0)
				{
					if (spriteState[i] < birdSpr.Length - 1)
						spriteState[i] ++;
					else
						spriteState[i] = 0;

					birdSprR[i].sprite = birdSpr[spriteState[i]];
				}
			}
		}
	}
}

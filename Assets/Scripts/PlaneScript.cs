using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour
{
	public Scripts scr;

	public LineRenderer lineRend1, lineRend2;
	public bool isTail;
	public bool isRight;
	public bool isUp;
	[Range(1,3)]
	public int startPosVar;
	public float speedX, speedY;
	public Sprite planeTraceSpr0, planeTraceSpr1;
	public int tracePeriod;
	private int timer;
	private float side, verticalSide;
	private float startPosX, startPosY;
	private Transform thisTr;
	private Transform[] planeTracesTr = new Transform[300];
	private SpriteRenderer[] planeTracesSprR = new SpriteRenderer[300];

	void Awake()
	{
		thisTr = transform;
		GameObject[] planeTraces = GameObject.FindGameObjectsWithTag ("PlaneTail");
		Debug.Log ("PlaneTail Length = " + planeTraces.Length);

		for (int i = 0; i < planeTracesTr.Length; i++)
		{
			planeTracesTr[i] = planeTraces [i].transform;
			planeTracesSprR[i] = planeTraces [i].GetComponent<SpriteRenderer> ();
			planeTraces [i].SetActive (false);
		}

		if (Random.value > 0.5f)
			isRight = true;
		else
			isRight = false;

		if (Random.value > 0.5f)
			isUp = true;
		else
			isUp = false;

		if (Random.value <= 0.33f)
			startPosY = 5;
		else if (Random.value > 0.33f && Random.value <= 0.66f)
			startPosY = 7;
		else
			startPosY = 9;

		if (isRight) 
		{
			side = -1;
			transform.localScale = new Vector3 (-1, 1, 1);
			startPosX = 10;
		}
		else 
		{
			side = 1;
			transform.localScale = new Vector3 (1, 1, 1);
			startPosX = -50;
		}

		thisTr.position = new Vector3(startPosX, startPosY, thisTr.position.z);

		if (isUp)
			verticalSide = 1;
		else
			verticalSide = 0;

		/*float rand1 = Random.value;
		Debug.Log ("Random value = " + rand1);

		if (rand1 > 0.2f) 
		{
			scr.skyScr.isPlaneDestroyed = true;
			gameObject.SetActive (false);
		}*/

		lineRend1.SetPosition(0, new Vector3(startPosX, startPosY - 0.1f, thisTr.position.z));
		lineRend2.SetPosition(0, new Vector3(startPosX, startPosY + 0.1f, thisTr.position.z));
	}

	private int j = -1;

	void Update () 
	{
		if (Time.timeScale != 0)
		{
			speedY += 5e-8f;

			thisTr.position = new Vector3 (
				thisTr.position.x + side * speedX,
				thisTr.position.y + verticalSide * speedY,
				thisTr.position.z);

			if (isTail) 
				TailOfSprites ();
		}
	}

	private void TailOfSprites()
	{
		timer ++;

		if (timer % tracePeriod == 0)
		{
			j += 2;

			if (j >= planeTracesTr.Length)
				j = 1;

			int rand0 = Mathf.RoundToInt(Random.value);
			int rand1 = Mathf.RoundToInt(Random.value);

			Transform newTrace0 = planeTracesTr [j];
			Transform newTrace1 = planeTracesTr [j - 1];

			SpriteRenderer newTrace0SprR = planeTracesSprR [j];
			SpriteRenderer newTrace1SprR = planeTracesSprR [j - 1];

			newTrace0.gameObject.SetActive (true);
			newTrace1.gameObject.SetActive (true);

			newTrace0.position = 
				new Vector3(
					thisTr.position.x,
					thisTr.position.y - 0.1f,
					thisTr.position.z);

			newTrace0.rotation = 
				Quaternion.Euler(0, 0, 360 * Random.value);
			newTrace0.localScale = Vector3.one;

			newTrace1.position = 
				new Vector3(
					thisTr.position.x,
					thisTr.position.y + 0.1f,
					thisTr.position.z);

			newTrace1.rotation = 
				Quaternion.Euler(0, 0, 360 * Random.value);
			newTrace1.localScale = Vector3.one;

			if (rand0 == 1)
				newTrace0SprR.sprite = planeTraceSpr0;
			else
				newTrace0SprR.sprite = planeTraceSpr1;

			if (rand1 == 1)
				newTrace1SprR.sprite = planeTraceSpr0;
			else
				newTrace1SprR.sprite = planeTraceSpr1;

			newTrace0SprR.color = scr.skyScr.currPlaneTraceColor;
			newTrace1SprR.color = scr.skyScr.currPlaneTraceColor;
		}
	}

	private void TailOfRenderers()
	{
		lineRend1.SetPosition(1, new Vector3(thisTr.position.x, thisTr.position.y - 0.1f, thisTr.position.z));
		lineRend2.SetPosition(1, new Vector3(thisTr.position.x, thisTr.position.y + 0.1f, thisTr.position.z));
	}
}

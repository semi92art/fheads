using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScrollScript : MonoBehaviour 
{
	public bool isToMiddle;
	public float maxScale, minScale;
	public string tag1;
	public int count;
	public RectTransform contPanel;
	public float toMiddleSpeed;
	public float minScrVel;
	private GameObject[] button;
	public float screenWidth;
	private bool changeRes;
	private int timer;
	public int timer1;
	public float vel;
	private Vector3 goal;
	
	void Update()
	{
		vel = gameObject.GetComponent<ScrollRect>().velocity.x;
		timer++;

		if (timer > 1) 
		{
			button = GameObject.FindGameObjectsWithTag (tag1);
			count = button.Length;
		}

		if (timer > 2)
		{
			#if UNITY_EDITOR
			//screenWidth = GameManager.GetMainGameViewSize().x;

			if (!Input.GetKey(KeyCode.Mouse0))
				ButtonToMiddle();
			else 
				timer1 = 0;
			

			#else
			screenWidth = Screen.width;

			if (Input.touchCount == 0)
				ButtonToMiddle();
			else 
				timer = 0;

			#endif

			foreach (var item in button) 
			{
				float posX_1 = item.GetComponent<RectTransform>().position.x;
				float scale = maxScale - Mathf.Abs(posX_1/Screen.width - 0.5f);
				if (scale < minScale)
					scale = minScale;

				item.GetComponent<RectTransform>().localScale = new Vector3(scale, scale, 1);
			}
		}
	}

	private void ButtonToMiddle()
	{
		if (isToMiddle)
		{
			Vector3 anchPos = contPanel.anchoredPosition;
			float posY = anchPos.y;

			if (Mathf.Abs(vel) <= minScrVel)
			{
				timer1++;
				float index = 0;
				
				if (timer1 == 1)
				{
					float width = button[0].GetComponent<RectTransform>().rect.width;
					index = Mathf.CeilToInt(-(contPanel.anchoredPosition.x + width/2)/width);
					float goalX = width * index;
					goal = new Vector3(-goalX, posY, 0);
				} else if (timer1 > 1 && timer1 < 80)
					contPanel.anchoredPosition = Vector3.Slerp(
						anchPos, goal, Time.deltaTime * toMiddleSpeed);
			}
		}
	}
}

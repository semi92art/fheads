using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CanvasNotActiveOnStart : MonoBehaviour {
	public GameObject[] menues;
	public GameObject[] menuesInUpdateDisable;
	public GameObject[] transparentMenues;

	private int timer;

	void Awake()
	{
		foreach (var item in transparentMenues)
		{
			if (item != null)
				item.GetComponent<Image>().color = Color.clear;
		}
	}

	void Start()
	{
		foreach (var item in menues)
		{
			if (item != null)
				item.SetActive(false);
		}
		//gameObject.GetComponent<CanvasNotActiveOnStart>().enabled = false;
	}

	void Update()
	{
		timer++;

		if (timer == 2)
		{
			foreach (var item in menuesInUpdateDisable)
			{
				item.SetActive(false);
			}
			gameObject.GetComponent<CanvasNotActiveOnStart>().enabled = false;
		}
	}
}

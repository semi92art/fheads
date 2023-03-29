using UnityEngine;
using System.Collections;

public class BallSoundOnScript : MonoBehaviour
{
	public BallTouchScript ballTScr;
	private Transform ballTr;

	void Awake()
	{
		ballTr = transform;
	}

	void Update()
	{
		if (ballTr.position.y < -8)
		{
			ballTScr.enabled = true;
			gameObject.GetComponent<BallSoundOnScript>().enabled = false;
		}
	}
}

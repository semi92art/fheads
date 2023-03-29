using UnityEngine;
using System.Collections;

public class BallTail : MonoBehaviour 
{
	public Scripts scr;

	public Sprite
		tailSpr1,
		tailSpr2,
		tailSpr3;

	public Transform ballTr;
	public Rigidbody2D ballRb;
	public Transform tailBoxTr;
	public SpriteRenderer tailSpr;

	public float ballVelX, ballVelY;
	public float tailAngle;
	public float ballRotZ;

	private int timer;
	private int tailTimer;

	void FixedUpdate()
	{
		//if ((scr.tM.time0 < 15 && scr.tM.matchPeriods == 1 && scr.tM.time0 > 1) || scr.tM.periodsText.text == "golden goal")
		//{
			timer ++;
			
			if (timer%5 == 0)
			{
				tailTimer++;
				
				switch (tailTimer)
				{
				case 1:
					tailSpr.sprite = tailSpr1;
					break;
				case 2:
					tailSpr.sprite = tailSpr2;
					break;
				case 3:
					tailSpr.sprite = tailSpr3;
					tailTimer = 0;
					break;
				}
			}
		//}
	}


	void Update()
	{
		//if ((scr.tM.time0 < 15 && scr.tM.matchPeriods == 1 && scr.tM.time0 > 1) || scr.tM.periodsText.text == "golden goal")
		//{
			ballVelX = ballRb.velocity.x;
			ballVelY = ballRb.velocity.y;
			
			if (ballVelY != 0)
			{
				tailAngle = Mathf.Acos(ballVelX / Mathf.Sqrt(ballVelX * ballVelX + ballVelY * ballVelY))
					* 180 / Mathf.PI * Mathf.Sign(ballVelY);
			}
			else
			{
				tailAngle = 0;
			}
			
			ballRotZ = ballTr.rotation.z * 180 / Mathf.PI;
			
			tailBoxTr.rotation = Quaternion.Euler(0, 0, tailAngle);
			tailBoxTr.localScale = new Vector3(ballRb.velocity.magnitude / 15, 0.1f, 1);
		/*}
		else
		{
			tailBoxTr.localScale = new Vector3(0, 0, 0);
		}*/
	}
}

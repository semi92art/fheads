using UnityEngine;
using System.Collections;
using System;

public class JumpScript : MonoBehaviour 
{
	public Scripts scr;

	public float jumpTime;
	public CircleCollider2D ballColl1;
	public int checkTime;
	public bool jump;

	public bool isRestart;
	public float timer;
	public bool secondJump;
	public bool isFirstJump;

	private bool isOffGround;
	private int offGroundTimer;
	

	void Update()
	{
		if (scr.pMov.restart) 
			isRestart = true;
		
		if (isRestart) 
		{
			timer += Time.fixedDeltaTime;

			if (timer > 3)
			{
				isRestart = false;
				timer = 0;
			}
		}

		if (isOffGround)
		{
			offGroundTimer ++;
			if (offGroundTimer < 10)
			{
				jump = false;
			}
			else
			{
				offGroundTimer = 0;
				isOffGround = false;
			}
		}
	}

	void FixedUpdate()
	{
		if (jump)
		{
			scr.objLev.enemy0Rb.velocity = new Vector2 (
				scr.objLev.enemy0Rb.velocity.x,
				scr.enAlg.maxSpeed * 1.2f);

			isOffGround = true;
		}
	}

	void OnTriggerStay2D (Collider2D other)
	{
		if (!isRestart) 
		{
			if (other == ballColl1) 
			{
				checkTime++;
				if (scr.grTr.gameObject.activeSelf)
				{
					if (scr.grTr.isEnemyGrounded) 
					{
						if (checkTime > 0) 
						{
							if (secondJump) 
							{
								if (checkTime > jumpTime * 3) 
								{
									checkTime = 0;
									jump = true;
								}
							} 
							else 
							{
								if (scr.objLev.ballTr.position.x < scr.enAlg.transform.position.x)
								{
									secondJump = true;
									checkTime = 0;
								}

								jump = true;
							}
						}
					} 
					else
					{
						checkTime = 0;
						jump = false;
					}
				}
			}
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (!isRestart)
		{
			if (other == ballColl1)
			{
				secondJump = false;
				checkTime = 0;
				jump = false;
			}
		}
	}
}
	


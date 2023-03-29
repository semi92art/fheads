using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class EnemyAlgoritm : MonoBehaviour
{
	public Scripts scr;

	public GameObject bar4Enemy;
	public Transform gkStateTr;
	public bool attacker;

	[HideInInspector]
	public float
	moveForce,
	maxSpeed,
	jumpForce,
	kickTorque, 
	kickSpeed;
	[Range(0, 5)]
	public float whenKick;
	[Range(0, 5)]
	public float distToBall;
	[HideInInspector]
	public bool gameStop;
	public Collider2D headColl1;
	public HingeJoint2D legHJ;
	public SpriteRenderer enemySprite;
	private Transform enSprTr;
	public GameObject legSprite;

	private int timer;
	public float Force;

	private bool chDir2;
	public bool isKick;

	private Transform middleTr;

	private float skill;
	private int jumpTimer;

	private float 
	posY,
	posX,
	bPosX,
	bPosY,
	plPosX,
	midPosX,
	Vx,
	signF,
	signFCh,
	absF;

	private bool isSprAnim;
	private float timerSprAnim;
	private int sprAnimVal;

	public float boredTimer;
	public int boredTimer1;
	public int 
	boredMove = 200, 
	boredJump = 200;

	public float gkStPosX;

	[HideInInspector]
	public bool kickMP;

	private int gameModeTimer;
	private bool gameMode;

	void Awake()
	{
		enSprTr = enemySprite.transform;

		if (scr.alPrScr.isLeaderboardGame == 0)
			enemySprite.sprite = scr.alScr.enemySprite;

		for (int i = 0; i < scr.prScrL.itemList.Count; i++)
		{
			if (scr.alScr.enemyName == scr.prScrL.itemList[i].name)
			{
				skill = scr.prScrL.itemList[i].summSkill;
				scr.jScr.jumpTime = 1 + 7 * (1.5f - skill);
				break;
			}
		}

		maxSpeed = scr.pMov.maxSpeed0 * Mathf.Pow(1.2f + skill/10, 2);
		jumpForce = scr.pMov.jumpForce0 * Mathf.Pow(1.05f + skill/10, 2);

		kickTorque = scr.pMov.kickTorque0 * Mathf.Pow(1 + skill/4, 1);
		kickSpeed = scr.pMov.kickSpeed0 * Mathf.Pow(1 + skill/4, 1);
		middleTr = GameObject.Find ("Ground Sprite").transform;
		midPosX = middleTr.position.x;

		JointAngleLimits2D limits = legHJ.limits;
		JointMotor2D motor = legHJ.motor;
		limits.max = 0;
		limits.min = -90;
		legHJ.limits = limits;
		motor.maxMotorTorque = 0;
		legHJ.motor = motor;
		distToBall = 1.5f;
		gkStPosX = gkStateTr.position.x;
		/*for (int i = 6; i <= 10; i++)
		{
			if (scr.alPrScr.enemyIndex%i == 0)
				attacker = true;
		}*/
	}

	void Start()
	{
		legHJ.transform.rotation = Quaternion.Euler (0, 0, 90);
	}

	private float startTime;
	private bool isKickControl;

	void Update()
	{
		if (scr.pMov.startGame && !scr.pMov.startGameCheck)
			startTime = Time.timeSinceLevelLoad;
		else if (scr.pMov.startGame && scr.pMov.startGameCheck) 
		{
			if (Time.timeSinceLevelLoad > startTime + PlayerMovement.restartDelay1 &&
				!isKickControl)
				isKickControl = true;
		}

		if (sprAnimVal != -1) 
		{
			if (Force == -1) 
			{
				if (scr.objLev.enemy0Tr.position.x > gkStateTr.position.x + 1)
					sprAnimVal = 1;
				else 
					sprAnimVal = 3;
			} 
			else if (Force == 1) 
				sprAnimVal = 2;
			else
				sprAnimVal = 3;
		}

		switch (sprAnimVal) 
		{
		case 1:
			enSprTr.localRotation = Quaternion.Euler (0, 0, 7);
			sprAnimVal = -1;
			break;
		case 2:
			enSprTr.transform.localRotation = Quaternion.Euler (0, 0, -7);
			sprAnimVal = -1;
			break;
		case 3:
			enSprTr.transform.localRotation = Quaternion.Euler(0, 0, 0);
			sprAnimVal = -1;
			break;
		}

		if (sprAnimVal == -1) 
		{
			timerSprAnim += Time.deltaTime;

			if (timerSprAnim > 0.2f)
			{
				sprAnimVal = 0;
				timerSprAnim = 0;
			}
		}

		/*if (Time.frameCount % 500 == 0)
		{
			readyToKickOverHead ++;

			if (readyToKickOverHead == 10)
				readyToKickOverHead = 0;
		}*/
	}

	private int readyToKickOverHead;

	void FixedUpdate ()
	{
		KickOverHeadFcn ();
		EnemyMovementControl ();
		MaxSpeedControl ();

		if (isKickControl)
			KickControl ();
	}

	private float randKick;

	private void KickControl() 
	{
		if (!gameStop)
		{
			if (Mathf.Abs (bPosY - posY) < 2) 
			{
				//kickOverHead = false;
				randKick = Time.timeSinceLevelLoad - Mathf.Ceil (Time.timeSinceLevelLoad);

				if (bPosX - posX > 1 && bPosX - posX < whenKick + randKick * 2 + skill * 1.3f && legHJ.jointAngle >= -1) 
				{
					if (attacker) 
						isKick = true;
					else 
					{
						if (scr.grTr.isEnemyGrounded)
							isKick = true;
					}
				} 
				else 
				{
					if (isKick && legHJ.jointAngle <= -90f) 
						isKick = false;
				}
			} 
			else 
			{
				/*if (readyToKickOverHead > 6)
				{
					if (!kickOverHead)
						//kickOverHead = true;
				}
				else
				{*/
					isKick = false;
					//kickOverHead = false;
				//}
			}

		
			if (isKick)
			{
				JointMotor2D motor = legHJ.motor;
				motor.motorSpeed = -kickSpeed;
				motor.maxMotorTorque = kickTorque;
				legHJ.motor = motor;
			} 
			else 
			{
				JointMotor2D motor = legHJ.motor;
				motor.motorSpeed = kickSpeed * 0.9f;
				motor.maxMotorTorque = kickTorque;
				legHJ.motor = motor;
			}
		}
	}

	private void EnemyBored()
	{
		boredTimer += Time.deltaTime;
		boredTimer1 ++;

		if (boredTimer1 % 10 == 0)
		{
			boredMove = Mathf.CeilToInt(Random.value * 10 + 10);
			boredJump = Mathf.CeilToInt(Random.value * 100 + 100);
		}

		if (boredTimer1 % boredMove == 0)
		{
			if (scr.grTr.gameObject.activeSelf) 
				Force = -Force;
		}

		if (boredTimer1 % boredJump == 0)
		{
			if (scr.grTr.isEnemyGrounded)
				scr.jScr.jump = true;
		}

		if (boredTimer >= 200) 
			boredTimer = 0;
	}

	private void Defend()
	{
		if (bPosX < midPosX) 
		{ 
			boredTimer = 0;

			if (posX > bPosX - distToBall) 
			{
				if (posX < gkStateTr.position.x)
				{
					if (posX < gkStateTr.position.x - 0.5f)
						Force = 1;
					else 
						Force = 0;
				} 
				else 
					Force = -1;
			} 
			else 
			{
				if (posY > bPosY) 
				{
					if (posX > bPosX - distToBall - 1)
						Force = -0.5f;
					else 
						Force = 1;
				} 
				else 
					Force = 1;
			}
		} 
		else 
		{
			boredTimer += Time.deltaTime;
			
			if (boredTimer < 0.5f)
			{
				if (posX < gkStateTr.position.x)
				{
					if (posX < gkStateTr.position.x - 0.5f)
						Force = 1;
					else 
						Force = 0;
				} 
				else 
				{
					if (posX > gkStateTr.position.x + 2) 
						boredTimer = 0;	
					
					Force = -1;
				}
			}
			else
			{
				if (posX < gkStateTr.position.x)
					Force = 1;
				else if (posX > gkStateTr.position.x + 3)
					Force = -1;
				else
					EnemyBored();
			}
		}
	}

	private void Attack()
	{
		if (posX > bPosX - distToBall) 
		{
			if (posX < gkStateTr.position.x)
			{
				if (posX < gkStateTr.position.x - 0.5f)
					Force = 1;
				else
					Force = 0;
			} 
			else 
				Force = -1;
		} 
		else
		{
			if (posY > bPosY + 1) 
			{
				if (posX > bPosX - distToBall - 1) 
					Force = -0.5f;
				else
					Force = 1;
			} 
			else 
				Force = 1;
		}
	}



	private void EnemyMovementControl() 
	{
		posY = scr.objLev.enemy0Tr.position.y;
		posX = scr.objLev.enemy0Tr.position.x;
		bPosX = scr.objLev.ballTr.position.x;
		bPosY = scr.objLev.ballTr.position.y;
		plPosX = scr.objLev.playerTr.position.x;
	
		if (!gameStop && !scr.pMov.restart && !scr.pMov.freezeOnStart) 
		{
			if (posX > plPosX && posX < bPosX)
				Force = 1;
			else
			{
				if (Score.score <= Score.score1) 
				{
					// if (scr.alPrScr.multiplayer == 0)
						Defend();
					// else
					// 	Attack();
				}
				else if (Score.score > Score.score1)
				{
					if (scr.tM.matchPeriods == 2)
					{
						if (Score.score - Score.score1 >= 3)
							Attack();
						else
						{
							// if (scr.alPrScr.multiplayer == 0)
								Defend();
							// else
							// 	Attack();
						}
					} 
					else 
						Attack();
				} 
				else 																																								
					Attack();
			}
		}

		if (scr.enCollScr.isPlayerColl) 
		{
			jumpTimer++;

			if (jumpTimer == 1 && scr.grTr.isEnemyGrounded) 
				scr.jScr.jump = true;
			else 
				scr.jScr.jump = false;
		} 
		else 
			jumpTimer = 0;
	}

	[HideInInspector]
	public Vector2 fVectEn;
	private bool chDir1;


	private void MaxSpeedControl() 
	{
		if (!scr.pMov.restart && !gameStop && !scr.pMov.freezeOnStart)
		{
			if (Mathf.Abs (scr.objLev.enemy0Rb.velocity.x) < maxSpeed)
			{
				if (Force != 0)
					scr.objLev.enemy0Rb.AddForce (new Vector2 (Force * moveForce, 0));
				else 
				{
					if (scr.objLev.enemy0Tr.position.x > -20) 
					{
						if (scr.tM.matchPeriods == 2) 
						{
							if (Score.score1 - Score.score - 3 >= 0)
								scr.objLev.enemy0Rb.AddForce (new Vector2 (-moveForce, 0));
						}
						else if (scr.tM.matchPeriods == 1)
						{
							if (Score.score1 - Score.score >= 0)
								scr.objLev.enemy0Rb.AddForce (new Vector2 (-moveForce, 0));
						}

					}
						
				}
			} 
			else 
			{
				scr.objLev.enemy0Rb.velocity = new Vector2 (
					Mathf.Sign (scr.objLev.enemy0Rb.velocity.x) * 0.98f * maxSpeed,
					scr.objLev.enemy0Rb.velocity.y);
			}


		}
	}

	private bool kickOverHead, kickOverHead_1;
	public float timer_1;

	private void KickOverHeadFcn()
	{
		if (kickOverHead)
		{
			if (!kickOverHead_1)
			{
				timer_1 += Time.deltaTime;

				if (!scr.grTr.isEnemyGrounded) 
				{
					if (scr.objLev.enemy0Rb.freezeRotation)
						scr.objLev.enemy0Rb.freezeRotation = false;

					scr.objLev.enemy0Rb.AddTorque (-scr.pMov.kickOverHeadTorque);

					if (scr.objLev.enemy0Tr.localScale.x > 0) 
					{
						scr.objLev.enemy0Tr.localScale = new Vector3 (
							-scr.objLev.enemy0Tr.localScale.x,
							scr.objLev.enemy0Tr.localScale.y,
							scr.objLev.enemy0Tr.localScale.z);
					}

					if (timer_1 > 0.5f) 
					{
						timer_1 = 0.0f;
						kickOverHead_1 = true;
						scr.objLev.enemy0Rb.freezeRotation = true;
						scr.objLev.enemy0Tr.rotation = Quaternion.Euler (0, 0, 0);

						if (timer_1 > 2.0f)
						{
							if (scr.objLev.enemy0Tr.localScale.x < 0)
							{
								scr.objLev.enemy0Tr.localScale = new Vector3 (
									-scr.objLev.enemy0Tr.localScale.x,
									scr.objLev.enemy0Tr.localScale.y,
									scr.objLev.enemy0Tr.localScale.z);
							}
						}
					}
				}
			}
			else
			{
				if (scr.objLev.enemy0Tr.localScale.x < 0)
				{
					scr.objLev.enemy0Tr.localScale = new Vector3 (
						-scr.objLev.enemy0Tr.localScale.x,
						scr.objLev.enemy0Tr.localScale.y,
						scr.objLev.enemy0Tr.localScale.z);
				}

				kickOverHead = false;
			}
		}
		else
		{
			kickOverHead_1 = false;
			timer_1 = 0.0f;

			if (!scr.objLev.enemy0Rb.freezeRotation) 
				scr.objLev.enemy0Rb.freezeRotation = true;

			if (scr.objLev.enemy0Tr.rotation.z > 0.0f)
				scr.objLev.enemy0Tr.rotation = Quaternion.Euler (0, 0, 0);

			if (scr.objLev.enemy0Tr.localScale.x < 0)
			{
				scr.objLev.enemy0Tr.localScale = new Vector3 (
					-scr.objLev.enemy0Tr.localScale.x,
					scr.objLev.enemy0Tr.localScale.y,
					scr.objLev.enemy0Tr.localScale.z);
			}
		}
	}
}

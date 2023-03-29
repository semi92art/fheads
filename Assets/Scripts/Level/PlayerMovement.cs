using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{
	public Scripts scr;

	public Animator goalImAnim;
	private Color cPl1, cPl2, cEn1, cEn2;
	public SpriteRenderer plLegSprR, enLegSprR;
	private Transform plLegFlSpr2Tr, enLegFlSpr2Tr;

	[HideInInspector]
	public Vector2 
	forceVector,
	jForceVector;

	public Sprite kickSprite;
	public Sprite kickPushedSprite;
	
	[Header("Player start position:")]
	public float xPlStart;
	public float yPlStart;
	[Header("Enemy start position:")]
	public float xEnStart;
	public float yEnStart;
	[Header("Player leg start position:")]
	public float xPlLegSt;
	public float yPlLegSt;
	[Header("Enemy leg start position:")]
	public float xEnLegSt;
	public float yEnLegSt;
	[Header("Other:")]
	public float maxSpeed0;
	public float jumpForce0;
	public float kickSpeed0;
	public float kickTorque0;

	[HideInInspector]
	public float 
	maxSpeed,
	jumpForce,
	kickSpeed,
	kickTorque;

	[Range(0, 20)]
	public float ballStartSpeedX;
	[Range(-20, 20)]
	public float ballStartSpeedY;

	public GameObject playerGates, enemyGates;
	public Transform enGatesTrigg, plGatesTrigg;
	public HingeJoint2D HJPlayerLeg;
	private Transform HJPlayerLegTr, HJEnemyLegTr;
	public SpriteRenderer plSpr;
	private Transform plSprTr;
	public Sprite moveLeftButton, moveLeftButtonPush;
	public Sprite moveRightButton, moveRightButtonPush;
	public Sprite jumpButton, jumpButtonPush;
	public Sprite kickButtonLeft, kickButtonLeftPush;
	public int Force, ForceCh;
	public bool freezeOnStart;
	public bool restart, restartCheck;

	private float restartTimer;
	private int scoreDiff;
	private int goalCheck, goalCheck1, goalCheck2;
	private string angleTag;
	private float skill;
	private bool check, check1;
	private bool chDir1, chDir2;
	[HideInInspector]
	public bool isMoveLeft, isMoveRight;
	public bool moveDown;

	private float moveDownTimer;
	private float signF;
	private float signFCh;
	private float Vx;
	private float absF;
	private float absFCh;

	private float
		barPosX,
		barPosY,
		barPosZ;

	private float ballPosX;

	public static float restartDelay1 = 1;
	public static float restartDelay2 = 0.5f;
	public static Transform MiddleTr;

	[HideInInspector]
	public bool jump;	
	[HideInInspector]
	public bool goal ,goal1;
	[HideInInspector]
	public bool startGame;
	[HideInInspector]
	public bool startGameCheck;
	//If button "Kick" is pushed, kick1 = true.
	[HideInInspector]
	public bool kick1;

	private int moveTimer;

	public float airResistForce;
	private int tailTimer;
	public int legPeriod;
	public float playerTailScaleTimer;
	public float enemyTailScaleTimer;

	private Vector3[] plNewTailPos = new Vector3[10];
	private Vector3[] enNewTailPos = new Vector3[10];

	private bool legColBool;
	private Transform plTr;
	public LineRenderer legLineR, legLineR_En;
	private Transform plLegLineRTr, enLegLineRTr;

	void Awake()
	{
		plLegFlSpr2Tr = plLegSprR.transform;
		enLegFlSpr2Tr = enLegSprR.transform;

		plLegLineRTr = legLineR.transform;
		enLegLineRTr = legLineR_En.transform;

		plTr = transform;
		HJPlayerLegTr = HJPlayerLeg.transform;
		HJEnemyLegTr = scr.enAlg.legHJ.transform;
		plSprTr = plSpr.transform;


		goalImAnim.gameObject.SetActive(true);

		for (int i = 0; i < scr.prScrL.itemList.Count; i++)
		{
			if (scr.alScr.playerName == scr.prScrL.itemList[i].name)
			{
				skill = scr.prScrL.itemList[i].summSkill;
				break;
			}
		}
			
		maxSpeed = maxSpeed0 * Mathf.Pow(1.2f + skill/10, 2);
		jumpForce = jumpForce0 * Mathf.Pow(1.05f + skill/10, 2);
		kickTorque = kickTorque0 * Mathf.Pow(1 + skill/4, 1);
		kickSpeed = kickSpeed0 * Mathf.Pow(1 + skill/4, 1);

		MiddleTr = GameObject.Find ("Ground Sprite").transform;
		freezeOnStart = true;
		plSpr.sprite = scr.alScr.playerSprite;
		NewStartPositions();

		for (int i = 0; i < scr.plLegCol.FlagColorsList.Count; i++)
		{
			if (scr.alScr.playerFlag == scr.plLegCol.FlagColorsList[i].flagSpr)
			{
				plLegSprR.sprite = scr.plLegCol.FlagColorsList [i].legSpr;
				cPl1 = scr.plLegCol.FlagColorsList[i].flagColor1;
				cPl2 = scr.plLegCol.FlagColorsList[i].flagColor2;
			}

			if (scr.alPrScr.isLeaderboardGame == 0)
			{
				if (scr.alScr.enemyFlag == scr.plLegCol.FlagColorsList[i].flagSpr)
				{
					enLegSprR.sprite = scr.plLegCol.FlagColorsList [i].legSpr;
					cEn1 = scr.plLegCol.FlagColorsList[i].flagColor1;
					cEn2 = scr.plLegCol.FlagColorsList[i].flagColor2;
				}
			}
		}

		legLineR.SetColors (cPl1, cPl2);
		legLineR_En.SetColors (cEn1, cEn2);

		for (int i = 0; i < plNewTailPos.Length; i++)
		{
			plNewTailPos[i] = plLegFlSpr2Tr.position;
			enNewTailPos[i] = enLegFlSpr2Tr.position;

			legLineR.SetPosition(i, plLegFlSpr2Tr.position);
			legLineR_En.SetPosition(i, enLegFlSpr2Tr.position);
		}
	}
		
	void Update()
	{
		if (!startGame) 
		{
			plTr.position = new Vector3(
				xPlStart,
				yPlStart, 
				plTr.position.z);
			scr.objLev.enemy0Tr.position = new Vector3(
				xEnStart, 
				yEnStart, 
				scr.objLev.enemy0Tr.position.z);
			HJPlayerLegTr.localPosition = new Vector3(
				xPlLegSt, 
				yPlLegSt, 
				HJPlayerLegTr.localPosition.z);
			HJEnemyLegTr.transform.localPosition = new Vector3(
				xEnLegSt, 
				yEnLegSt, 
				HJEnemyLegTr.transform.localPosition.z);
			
			Time.timeScale = 0;

			scr.objLev.ballTr.position = new Vector3 (MiddleTr.position.x, -6, scr.objLev.ballTr.position.z);
			scr.objLev.ballRb.velocity = new Vector2 (0, 0);
		} 
		else 
		{
			Controls ();
			FreezeOnStart ();
			RestartFunction ();

			if (!restart)
			{
				if (scr.objLev.ballTr.position.x <= enGatesTrigg.position.x
				    && scr.objLev.ballTr.position.y <= enGatesTrigg.position.y)
					goal = true;
				else
				{
					goalCheck = 0;
					goal = false;
				}

				if (scr.objLev.ballTr.position.x >= plGatesTrigg.position.x
				    && scr.objLev.ballTr.position.y <= plGatesTrigg.position.y)
				{
					goal1 = true;
				}
				else 
				{
					goalCheck1 = 0;
					goal1 = false;
				}
				
				if (goal1) 
				{
					goalCheck1++;
					goalCheck2++;
					restart = true;
				}
				
				if (goal) 
				{
					goalCheck++;
					goalCheck2++;
					restart = true;
				}

				if (goalCheck == 1 && goalCheck2 == 1) 
				{
					goalImAnim.SetTrigger ("call");
					scr.levAudScr.goalSource.Play();
					Score.score++;
					scr.scoreScr.SetScore();
				}
				if (goalCheck1 == 1 && goalCheck2 == 1) 
				{
					goalImAnim.SetTrigger ("call");
					scr.levAudScr.goalSource.Play();
					Score.score1++;
					scr.scoreScr.SetScore();
				}
			}
		}

		if (!restart && !freezeOnStart && Time.timeScale > 0)
		{
			if (Force == -1)
				plSprTr.localRotation = Quaternion.Euler (0, 0, 7);
			else if (Force == 1)
				plSprTr.localRotation = Quaternion.Euler (0, 0, -7);
			else
				plSprTr.localRotation = Quaternion.Euler (0, 0, 0);	
		}
	}

	private float k = 0.0f;

	void FixedUpdate()
	{
		KickOverHeadFcn ();
		LegTail ();

		if (startGame) 
		{
			if (!restart && !freezeOnStart) 
			{
				if (Force != 0)
					scr.objLev.playerRb.velocity = new Vector2 (
						Force * maxSpeed,
						scr.objLev.playerRb.velocity.y);
				
				scoreDiff = Score.score - Score.score1;

				if (jump) 
				{
					jForceVector = Vector2.up * jumpForce;
					scr.objLev.playerRb.velocity = new Vector2 (
						scr.objLev.playerRb.velocity.x,
						maxSpeed * 1.2f);
					jump = false;
				} 
			} 

			if (!kickOverHead)
				k = 1.0f;
			else
				k = -1.0f;

			switch (kick1)
			{
			case true:
				float dev = .2f;
				if (HJPlayerLeg.useLimits)
				{
					JointAngleLimits2D limits = HJPlayerLeg.limits;
					limits.min = 0;
					limits.max = 90;
					HJPlayerLeg.limits = limits;
				}
				JointMotor2D motor = HJPlayerLeg.motor;
				motor.motorSpeed = kickSpeed * (1 + dev * Random.value - dev/2) * k;
				motor.maxMotorTorque = kickTorque;
				HJPlayerLeg.motor = motor;
				break;
			case false:
				if (HJPlayerLeg.useLimits)
				{
					JointAngleLimits2D limits = HJPlayerLeg.limits;
					limits.min = 0;
					limits.max = 1;
					HJPlayerLeg.limits = limits;
				}
				JointMotor2D motor1 = HJPlayerLeg.motor;
				motor1.motorSpeed = -kickSpeed * .9f * k;
				motor1.maxMotorTorque = kickTorque;
				HJPlayerLeg.motor = motor1;
				break;
			}

			if (Mathf.Abs(scr.objLev.playerRb.velocity.x) > 5)
				scr.objLev.playerRb.AddForce (
					new Vector2(-Mathf.Sign(scr.objLev.playerRb.velocity.x) * airResistForce,0));
		}

		startGameCheck = startGame;
		restartCheck = restart;
	}
		
	private void LegTail()
	{
		if (!startGame)
		{

		}
		else
		{
			legLineR.SetPosition (0, plLegLineRTr.position);
			legLineR_En.SetPosition (0, enLegLineRTr.position);

			tailTimer++;

			if (tailTimer % legPeriod == 0)
			{
				for (int i = plNewTailPos.Length - 1; i > 0; i--)
					plNewTailPos [i] = plNewTailPos [i - 1];

				plNewTailPos [0] = plLegLineRTr.position;

				for (int i = 1; i < plNewTailPos.Length; i++)
					legLineR.SetPosition (i, plNewTailPos [i]);

			}

			if (restart)
				legLineR.SetColors (Color.clear, Color.clear);
			else
			{
				if (restartCheck) 
					legLineR.SetColors (cPl1, cPl2);
			}


			if (tailTimer % legPeriod == 0)
			{
				for (int i = enNewTailPos.Length - 1; i > 0; i--)
					enNewTailPos [i] = enNewTailPos [i - 1];
				
				enNewTailPos [0] = enLegLineRTr.position;

				for (int i = 1; i < enNewTailPos.Length; i++)
					legLineR_En.SetPosition (i, enNewTailPos [i]);
			}

			if (restart)
				legLineR_En.SetColors (Color.clear, Color.clear);
			else 
			{
				if (restartCheck) 
					legLineR_En.SetColors (cEn1, cEn2);
			}
		}
	}

	private void StartPositions()
	{
		scr.objLev.playerRb.velocity = new Vector2 (0, 0);
		scr.objLev.ballTr.position = new Vector3 (MiddleTr.position.x, -6, scr.objLev.ballTr.position.z);
		scr.objLev.ballRb.velocity = new Vector2 (0, 0);
		scr.objLev.ballRb.constraints = RigidbodyConstraints2D.FreezeRotation;
		plTr.position = new Vector3 (-3, -1.9f, plTr.position.z);
		scr.objLev.enemy0Tr.position = new Vector3 (-37, -1.9f, scr.objLev.enemy0Tr.position.z);

	}

	private void NewStartPositions()
	{
		scr.objLev.playerRb.velocity = new Vector2 (0, 0);
		scr.objLev.ballTr.position = new Vector3 (MiddleTr.position.x, -6, scr.objLev.ballTr.position.z);
		scr.objLev.ballRb.velocity = new Vector2 (0, 0);
		scr.objLev.ballRb.constraints = RigidbodyConstraints2D.FreezeRotation;
		scr.objLev.enemy0Rb.velocity = new Vector2 (0, 0);

		plTr.position = new Vector3(xPlStart, yPlStart, plTr.position.z) ;
		scr.objLev.enemy0Tr.position = new Vector3(xEnStart, yEnStart, scr.objLev.enemy0Tr.position.z);
	}
	
	private void Controls()
	{
		#if UNITY_EDITOR || UNITY_STANDALONE
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			MoveLeft();
		else if (Input.GetKeyUp (KeyCode.LeftArrow))
			MoveLeftEnd ();

		if (Input.GetKeyDown (KeyCode.RightArrow)) 
			MoveRight ();
		else if (Input.GetKeyUp (KeyCode.RightArrow)) 
			MoveRightEnd ();

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
			Jump ();
		else if (Input.GetKeyUp (KeyCode.UpArrow))
			JumpEnd ();

		if (Input.GetKeyDown (KeyCode.LeftControl)) 
			Kick ();
		else if (Input.GetKeyUp (KeyCode.LeftControl)) 
			KickEnd ();

		if (Input.GetKeyDown (KeyCode.RightControl)) 
			Kick ();
		else if (Input.GetKeyUp (KeyCode.RightControl)) 
			KickEnd ();

		if (Input.GetKeyDown (KeyCode.R))
			scr.gM.LevelRestartInLevel();

		if (Input.GetKeyDown (KeyCode.W))
		{
			scr.gM.WinGame1();

			if (scr.alPrScr.freePlay == 0) 
				scr.tM.matchPeriods = 1;
		}

		if (Input.GetKeyDown (KeyCode.LeftShift))
			KickOverHead();
		else if (Input.GetKeyUp (KeyCode.LeftShift))
			KickOverHeadEnd();

		if (Input.GetKeyDown (KeyCode.RightShift))
			KickOverHead();
		else if (Input.GetKeyUp (KeyCode.RightShift))
			KickOverHeadEnd();

		if (Input.GetKeyDown (KeyCode.L))
		{
			scr.gM.LooseGame();

			if (scr.alPrScr.freePlay == 0) 
				scr.tM.matchPeriods = 1;
		}

		if (Input.GetKeyDown (KeyCode.T)) 
		{
			scr.gM.TieGame();

			if (scr.alPrScr.freePlay == 0) 
				scr.tM.matchPeriods = 1;
		}

		if (Input.GetKeyDown(KeyCode.K)) 
			HJPlayerLeg.useLimits = !HJPlayerLeg.useLimits;

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			Score.score++;
			scr.scoreScr.SetScore();
		}

		if (Input.GetKeyDown(KeyCode.Alpha1)) 
		{
			Score.score1++;
			scr.scoreScr.SetScore();
		}

		#endif
	}

	private void RestartFunction()
	{
		if (restart)
		{
			GameObject[] ballTails = GameObject.FindGameObjectsWithTag("BallTail");

			if (ballTails.Length > 0)
			{
				for (int i = 0; i < ballTails.Length; i++)
					DestroyImmediate(ballTails[i]);
			}

			if (scr.grTr.gameObject.activeSelf) 
			{
				scr.grTr.isBallGrounded = false;
			}

			jump = false;

			restartTimer += Time.deltaTime;
			if (restartTimer < restartDelay1)
			{
				if (restartTimer < restartDelay2)
				{
					scr.objLev.playerRb.velocity = new Vector2 (0, scr.objLev.playerRb.velocity.y);
					scr.objLev.enemy0Rb.velocity = new Vector2 (0, scr.objLev.enemy0Rb.velocity.y);
				} 
				else 
					NewStartPositions();
			} 
			else 
			{
				BallStartImpulse();
				restart = false;
				restartTimer = 0;
				goalCheck2 = 0;
			}
		}
	}

	private void BallStartImpulse()
	{
		scr.objLev.ballTr.position = new Vector3 (MiddleTr.position.x, -6, scr.objLev.ballTr.position.z);

		float rand = ballStartSpeedX * 0.2f * Mathf.Cos(Time.timeSinceLevelLoad);

		if (scr.tM.time0 > 87) {
			int side = 0;
			if (scr.tM.matchPeriods%2 == 0)
				side = -1;
			else side = 1;

			scr.objLev.ballRb.velocity = new Vector2 (-ballStartSpeedX * side + rand, ballStartSpeedY);
		} else {
			if (Score.score - Score.score1 < scoreDiff) {
				scr.objLev.ballRb.velocity = new Vector2 (ballStartSpeedX + rand, ballStartSpeedY);
			} else {
				scr.objLev.ballRb.velocity = new Vector2 (-ballStartSpeedX + rand, ballStartSpeedY);
			}
		}
		scr.objLev.ballRb.constraints = RigidbodyConstraints2D.None;
	}

	private void FreezeOnStart()
	{
		if (freezeOnStart) 
		{
			if (scr.grTr.gameObject.activeSelf) 
				scr.grTr.isBallGrounded = false;

			jump = false;

			restartTimer = Time.timeSinceLevelLoad;

			if (restartTimer < restartDelay1)
				NewStartPositions();	
			else 
			{
				BallStartImpulse();
				goalCheck2 = 0;
				restartTimer = 0;
				freezeOnStart = false;
			}
		}
	}
		
	public void MoveLeft()
	{
		isMoveLeft = true;
		Force = -1;
	}

	public void MoveRight()
	{
		isMoveRight = true;
		Force = 1;
	}

	public void MoveLeftEnd()
	{
		isMoveLeft = false;

		if (!isMoveRight) 
			Force = 0;
	}
		
	public void MoveRightEnd()
	{
		isMoveRight = false;

		if (!isMoveLeft) 
			Force = 0;	
	}
		
	public void Jump ()
	{
		if (scr.grTr.gameObject.activeSelf) 
		{
			if (scr.grTr.isPlayerGrounded)
				jump = true;
		} 
		else 
			jump = true;
	}

	public void JumpEnd ()
	{
		jump = false;
	}


	public void Kick ()
	{
		if (!restart && !freezeOnStart) 
			kick1 = true;
	}

	public void KickEnd ()
	{
		kick1 = false;
	}

	public void KickLeft ()
	{
		Kick();
	}
	
	public void KickLeftEnd ()
	{
		KickEnd();
	}

	public void StartGame()
	{
		Time.timeScale = 1;
		startGame = true;
		scr.objLev.startPanelAnim.gameObject.SetActive(false);
	}

	public void KickOverHead()
	{
		if (scr.grTr.isPlayerGrounded)
			jump = true;

		kickOverHead = true;
	}

	public void KickOverHeadEnd()
	{
		kickOverHead = false;
		jump = false;
	}

	private bool kickOverHead, kickOverHead_1;
	public float kickOverHeadTorque;
	private float timer_1;

	private void KickOverHeadFcn()
	{
		if (kickOverHead)
		{
			if (!kickOverHead_1)
			{
				timer_1 += Time.deltaTime;

				if (!scr.grTr.isPlayerGrounded) 
				{
					if (scr.objLev.playerRb.freezeRotation)
						scr.objLev.playerRb.freezeRotation = false;

					scr.objLev.playerRb.AddTorque (kickOverHeadTorque);

					if (plTr.localScale.x > 0)
					{
						plTr.localScale = new Vector3 (
							-plTr.localScale.x,
							plTr.localScale.y,
							plTr.localScale.z);
					}

					if (timer_1 > 0.5f)
					{
						timer_1 = 0.0f;
						kickOverHead_1 = true;
						scr.objLev.playerRb.freezeRotation = true;
						scr.objLev.playerTr.rotation = Quaternion.Euler (0, 0, 0);
					}
				}
			}
		}
		else
		{
			//if (kickOverHead_1)
			//{
				kickOverHead_1 = false;
				timer_1 = 0.0f;
			//}

			if (!scr.objLev.playerRb.freezeRotation) 
				scr.objLev.playerRb.freezeRotation = true;

			if (scr.objLev.playerTr.rotation.z > 0.0f)
				scr.objLev.playerTr.rotation = Quaternion.Euler (0, 0, 0);

			if (plTr.localScale.x < 0)
			{
				plTr.localScale = new Vector3 (
					-plTr.localScale.x,
					plTr.localScale.y,
					plTr.localScale.z);
			}
		}
	}
}
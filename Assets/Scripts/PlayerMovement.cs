using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour 
{
    public Scripts scr;

    [Header("Kick Overhead Objects:")]
    public EdgeCollider2D col_EdgeSlide;
    public CircleCollider2D col_CircSlide;
    public float genTr_x;
    public float genTr_y;
    public float genTr_rot;
    public Transform tr_GeneralLeg;
    public float kickOvHTorq_0;
    public float torqTime;
    public float timOvH;
    public float timH;
    [Header("Default values of Player and Enemy movement:")]
	public float maxSpeed0;
	public float jumpForce0;
	public float kickSpeed0;
	public float kickTorque0;
    [Header("Real values of Player and Enemy movement:")]
	public float maxSpeed;
	public float jumpForce;
	public float kickSpeed;
	public float kickTorque;

    [Header("Other:")]
    public Animator goalImAnim;

    [HideInInspector]
    public Vector2
    forceVector,
    jForceVector;

    private Vector2 plLegSt = new Vector2(0, 0.3f);
    private Vector2 enLegSt = new Vector2(0, 0.3f);
	[Range(0, 40)]
	public float ballStartSpeedX;
	[Range(-20, 20)]
	public float ballStartSpeedY;

    public CircleCollider2D plHead, enHead;
	public GameObject playerGates, enemyGates;
	public Transform enGatesTrigg, plGatesTrigg;
	public HingeJoint2D HJPlayerLeg;
	private Transform HJPlayerLegTr;
	public SpriteRenderer plSpr;
	private Transform plSprTr;
	public Sprite moveLeftButton, moveLeftButtonPush;
	public Sprite moveRightButton, moveRightButtonPush;
	public Sprite jumpButton, jumpButtonPush;
	public Sprite kickButtonLeft, kickButtonLeftPush;
	public int Force, ForceCh;
    [HideInInspector]
    public bool freezeOnStart = true;
	public bool restart, restartCheck;
    [HideInInspector]
	public float restartTimer;
	private int scoreDiff;
	private int goalCheck, goalCheck1;
	private string angleTag;
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

	public static float restartDelay1 = 2;
	public static float restartDelay2 = 0.5f;

	[HideInInspector]
	public bool jump;
    [HideInInspector]
    public bool kick1;

    private int goal;
	[HideInInspector]
	public bool startGame;
	[HideInInspector]
	public bool startGameCheck;
	
	private int moveTimer;
	public float airResistForce;
	private bool legColBool;

    public float delta_Pl_Ball;
    public float delta_En_Ball;
    public float delta_Pl_En;
    [HideInInspector]
    public Rigidbody2D _rb;
    [Header("Money Bar:")]
    public Animator anim_PlusMoney;

    public Text text_Bank;
    public Text text_Money;
    private float jumpForceDef;

	void Awake()
    {
        text_Bank.text = scr.univFunc.moneyString(scr.alPrScr.moneyCount);
        _rb = GetComponent<Rigidbody2D>();
		HJPlayerLegTr = HJPlayerLeg.transform;
		plSprTr = plSpr.transform;

		goalImAnim.gameObject.SetActive(false);
        goalImAnim.enabled = false;

        maxSpeed = maxSpeed0 * scr.buf.plSkillSpeed / 100f;
        jumpForce = jumpForce0 * scr.buf.plSkillJump / 100f;
        kickTorque = kickTorque0 * scr.buf.plSkillKick / 100f;
        kickSpeed = kickSpeed0;
        SKJ_Upgrades();

        jumpForceDef = jumpForce;
		freezeOnStart = true;
		NewStartPositions();

        if (scr.buf != null)
        {
            plSpr.sprite = scr.buf.plSpr;
            scr.objLev.plLegSprR.sprite = scr.buf.plBoot;
            scr.objLev.enLegSprR.sprite = scr.buf.enBoot;
            scr.objLev.enLegSprR_1.sprite = scr.buf.enBoot_1;
        }
	}

    private void SKJ_Upgrades()
    {
        maxSpeed = maxSpeed * (100f + (float)scr.alPrScr.upgrSpeed * 1.5f) / 100f;
        jumpForce = jumpForce * (100f + (float)scr.alPrScr.upgrJump * 2f) / 100f;
        kickTorque = kickTorque * (100f + (float)scr.alPrScr.upgrKick * 2f) / 100f;
        kickSpeed = kickSpeed * (100f + (float)scr.alPrScr.upgrKick * 2f) / 100f;
    }

	private void Update()
	{
		if (!startGame) 
		{
            transform.position = scr.marks.plStartTr.position;

			HJPlayerLegTr.localPosition = new Vector3(
                plLegSt.x, 
                plLegSt.y, 
				HJPlayerLegTr.localPosition.z);
            scr.enAlg.legHJ.transform.localPosition = new Vector3(
                enLegSt.x,
                enLegSt.y,
                scr.enAlg.legHJ.transform.localPosition.z);
            scr.enAlg_1.legHJ.transform.localPosition = new Vector3(
                enLegSt.x,
                enLegSt.y,
                scr.enAlg_1.legHJ.transform.localPosition.z);
			
			Time.timeScale = 0f;

			scr.ballScr.transform.position = new Vector3 (scr.marks.midTr.position.x, -4, scr.ballScr.transform.position.z);
            scr.ballScr._rb.velocity = new Vector2(0, 0);
		} 
		else 
		{
            delta_Pl_Ball = plHead.transform.position.x - plHead.radius * 
                transform.localScale.x - scr.ballScr.transform.position.x;
            delta_En_Ball = -enHead.transform.position.x - enHead.radius *
                scr.enAlg.transform.localScale.x + scr.ballScr.transform.position.x;
            delta_Pl_En = plHead.transform.position.x - plHead.radius * transform.localScale.x -
                enHead.transform.position.x - enHead.radius * scr.enAlg.transform.localScale.x;

            if (delta_Pl_En < 0.3f && delta_Pl_En > 0f)
                scr.jScr.jump = true;

			Controls ();
			RestartFunction ();

            GoalCheck();
		}

		if (!restart && !freezeOnStart && Time.timeScale > 0.1f)
		{
            if (scr.bonObjMan.isPlayerFast != 0)
            {
                Force = 0;
                kick1 = false;
                kOvH = false;
                jump = false;
            }

            switch (Force)
            {
                case -1:
                    plSprTr.localRotation = Quaternion.Euler(0, 0, 9);
                    break;
                case 1:
                    plSprTr.localRotation = Quaternion.Euler(0, 0, -9);
                    break;
                case 0:
                    plSprTr.localRotation = Quaternion.Euler(0, 0, 0);	
                    break;
                default:
                    plSprTr.localRotation = Quaternion.Euler(0, 0, 0);	
                    break;
            }
		}
	}

    private float ballImpulseSide = 1;

    private void GoalCheck()
    {
        if (!restart)
        {
            if (scr.ballScr.transform.position.x <= enGatesTrigg.position.x
                && scr.ballScr.transform.position.y <= enGatesTrigg.position.y)
                goal = -1;
            else if (scr.ballScr.transform.position.x >= plGatesTrigg.position.x
                && scr.ballScr.transform.position.y <= plGatesTrigg.position.y)
                goal = 1;
            else
            {
                goalCheck = 0;
                goal = 0;
            }

            if (Mathf.Abs(goal) == 1) 
            {
                goalCheck += goal;
                goalCheck1++;
                restart = true;
            }

            if (goalCheck1 == 1)
            {
                scr.camSize.tim = 0;
                scr.skyScr.isGoal = true;
                scr.fwScr.SetActiveGoalFirework();

                if (goalCheck == -1)
                {
                    
                    text_Bank.text = scr.univFunc.moneyString(scr.alPrScr.moneyCount);
                    //text_Money.text = "500$";
                    anim_PlusMoney.SetTrigger(Animator.StringToHash("0"));
                    scr.goalPanScr.RefereeAnimRight();
                    Score.score++;

                    if (!scr.practScr.isPractice)
                    {
                        PlayerPrefs.SetInt("TotalGoals",
                            PlayerPrefs.GetInt("TotalGoals") + 1);
                    }

                    ballImpulseSide = -1f;
                }
                else if (goalCheck == 1)
                {
                    Score.score1++;
                    scr.goalPanScr.RefereeAnimLeft();
                    ballImpulseSide = 1f;
                }

                goalImAnim.gameObject.SetActive(true);
                goalImAnim.enabled = true;
                scr.goalPanScr.PlayStarsAnim();

                if (scr.levAudScr.isSoundOn)
                {
                    scr.levAudScr.goalSource.Play();
                    scr.levAudScr.refereeWhistleSource.Play();
                }
                
                scr.scoreScr.SetScore();
            }
        }
    }

	void FixedUpdate()
	{
        KickOverHeadFcn ();

		if (startGame) 
		{
			if (!restart && !freezeOnStart) 
			{
                scoreDiff = Score.score - Score.score1;
                float velX = Force != 0 ? Force * maxSpeed : _rb.velocity.x;
                float velY = jump ? jumpForce : _rb.velocity.y;

                if (jump) jump = false;

                scr.pMov._rb.velocity = new Vector2(velX, velY);
			} 

            if (kick1)
            {
                if (!kOvH)
                {
                    JointAngleLimits2D limits = HJPlayerLeg.limits;
                    limits.min = 270f;
                    limits.max = 360f;
                    HJPlayerLeg.limits = limits;

                    JointMotor2D motor = HJPlayerLeg.motor;
                    motor.motorSpeed = kickSpeed * (1f + .2f * Random.value - .2f / 2f);
                    motor.maxMotorTorque = kickTorque;
                    HJPlayerLeg.motor = motor;
                }
            }
            else
            {
                if (!kOvH)
                {
                    JointAngleLimits2D limits = HJPlayerLeg.limits;
                    limits.min = 270;
                    limits.max = 271;
                    HJPlayerLeg.limits = limits;

                    JointMotor2D motor1 = HJPlayerLeg.motor;
                    motor1.motorSpeed = -kickSpeed * .9f;
                    motor1.maxMotorTorque = kickTorque;
                    HJPlayerLeg.motor = motor1;
                }
            }

            if (Mathf.Abs(scr.pMov._rb.velocity.x) > 5)
                scr.pMov._rb.AddForce(
                    new Vector2(-Mathf.Sign(scr.pMov._rb.velocity.x) * airResistForce, 0));
		}

		startGameCheck = startGame;
		restartCheck = restart;
	}

	private void NewStartPositions()
	{
        scr.pMov._rb.velocity = new Vector2(0, 0);
		scr.ballScr.transform.position = new Vector3 (scr.marks.midTr.position.x, -4f, scr.ballScr.transform.position.z);
        scr.ballScr._rb.velocity = new Vector2(0, 0);
        scr.ballScr._rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        scr.enAlg._rb.velocity = new Vector2(0, 0);
        

        transform.position = scr.marks.plStartTr.position;
        scr.enAlg.transform.position = scr.marks.enStartTr.position;
        scr.enAlg_1.transform.position = new Vector3(
            scr.marks.enStartTr.transform.position.x - 10f,
            scr.marks.enStartTr.transform.position.y,
            scr.marks.enStartTr.transform.position.z);

        if (scr.tM.time0 > scr.tM.beginTime - 1f && scr.tM.time0 < scr.tM.beginTime)
        {
            if (scr.levAudScr.isSoundOn)
                scr.levAudScr.refereeWhistleSource.Play();
        }
	}
	
    private bool isNewStartPos_0;

	private void RestartFunction()
	{
        if (freezeOnStart)
        {
            if (Time.timeSinceLevelLoad < restartDelay1)
                NewStartPositions();
            else
            {
                BallStartImpulse();
                freezeOnStart = false;
            }
        }

		if (restart)
		{
            plSprTr.localRotation = Quaternion.Euler(0, 0, 0);	
            jump = false;
			scr.grTr.isBallGrounded = false;
			restartTimer += Time.deltaTime;

			if (restartTimer < restartDelay1)
			{
                if (restartTimer < restartDelay2)
                {
                    scr.pMov._rb.velocity = new Vector2(0, scr.pMov._rb.velocity.y);
                    scr.enAlg._rb.velocity = new Vector2(0, scr.enAlg._rb.velocity.y);
                }
                else
                    NewStartPositions();	
			} 
			else 
			{
				BallStartImpulse();
				restart = false;
				restartTimer = 0;
				goalCheck1 = 0;
			}
		}
	}

	private void BallStartImpulse()
	{
		scr.ballScr.transform.position = new Vector3 (scr.marks.midTr.position.x, -4, scr.ballScr.transform.position.z);
		float rand = ballStartSpeedX * 0.3f * Mathf.Cos(Time.timeSinceLevelLoad);

		/*if (scr.tM.time0 > 57) 
        {
            int side = scr.tM.matchPeriods == 2 ? -1 : 1;
            scr.ballScr._rb.velocity = new Vector2(-ballStartSpeedX * side + rand, ballStartSpeedY);
		} 
        else */
        //{
        float bVx = ballStartSpeedX * ballImpulseSide;
            scr.ballScr._rb.velocity = new Vector2(bVx + rand, ballStartSpeedY);
		//}

        scr.ballScr._rb.constraints = RigidbodyConstraints2D.None;
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
		
	public void Left_JK_Button()
	{
		if (scr.alPrScr.controls == 1)
		{
			Jump();
			scr.objLev.leftButSprR.sprite = scr.objLev.jump2Spr;
		}
		else if (scr.alPrScr.controls == 2)
		{
			Kick();
			scr.objLev.leftButSprR.sprite = scr.objLev.kick2Spr;
		}
	}

	public void Left_JK_EndButton()
	{
		if (scr.alPrScr.controls == 1)
		{
			JumpEnd();
			scr.objLev.leftButSprR.sprite = scr.objLev.jump1Spr;
		}
		else if (scr.alPrScr.controls == 2)
		{
			KickEnd();
			scr.objLev.leftButSprR.sprite = scr.objLev.kick1Spr;
		}
	}

	public void Right_JK_Button()
	{
		if (scr.alPrScr.controls == 1)
		{
			Kick();
			scr.objLev.rightButSprR.sprite = scr.objLev.kick2Spr;
		}
		else if (scr.alPrScr.controls == 2)
		{
			Jump();
			scr.objLev.rightButSprR.sprite = scr.objLev.jump2Spr;
		}
	}

	public void Right_JK_EndButton()
	{
		if (scr.alPrScr.controls == 1)
		{
			KickEnd();
			scr.objLev.rightButSprR.sprite = scr.objLev.kick1Spr;
		}
		else if (scr.alPrScr.controls == 2)
		{
			JumpEnd();
			scr.objLev.rightButSprR.sprite = scr.objLev.jump1Spr;
		}
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
            scr.objLev.LevelRestartInLevel();

		if (Input.GetKeyDown (KeyCode.W))
		{
			scr.gM.WinGame1();
			scr.tM.matchPeriods = 1;
		}

		if (Input.GetKeyDown (KeyCode.L))
		{
			scr.gM.LooseGame();
			scr.tM.matchPeriods = 1;
		}

		if (Input.GetKeyDown (KeyCode.T)) 
		{
			scr.gM.TieGame();
			scr.tM.matchPeriods = 1;
		}

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

        if (Input.GetKeyDown (KeyCode.LeftShift))
            KickOverHead();
        else if (Input.GetKeyUp (KeyCode.LeftShift))
            KickOverHeadEnd();
		#endif
	}

    public void KickOverHead()
    {
        kOvH = true;
    }

    public void KickOverHeadEnd()
    {
        kOvH = false;
        jump = false;
    }

    [HideInInspector]
    public bool kOvH;
    private float kOvHTorque;
   

    private void KickOverHeadFcn()
    {
        if (kOvH)
        {
            col_CircSlide.enabled = false;
            col_EdgeSlide.enabled = false;

            tr_GeneralLeg.localPosition = new Vector3 (genTr_x, genTr_y, 0f);
            tr_GeneralLeg.localRotation = Quaternion.Euler (0f, 0f, genTr_rot);

            scr.pMov._rb.freezeRotation = false;
            timH += Time.deltaTime;

            transform.localScale = new Vector3(
                -Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);

            kOvHTorque = timH < torqTime ? kickOvHTorq_0 : kOvHTorque / 2f;
            scr.pMov._rb.AddTorque(kOvHTorque);

            if (scr.grTr.isPlayerGrounded)
            {
                jump = true;
                kOvH = timH > torqTime ? false : true;
            }

            JointAngleLimits2D limits = HJPlayerLeg.limits;
            limits.min = timH < timOvH ? 179f : 90f;
            limits.max = timH < timOvH ? 180f : 91f;
            HJPlayerLeg.limits = limits;
        }
        else
        {
            col_CircSlide.enabled = true;
            col_EdgeSlide.enabled = true;

            tr_GeneralLeg.localPosition = new Vector3 (0f, 0f, 0f);
            tr_GeneralLeg.localRotation = Quaternion.Euler (0f, 0f, 0f);

            timH = 0.0f;
            kOvHTorque = kickOvHTorq_0;
            scr.pMov._rb.freezeRotation = true;
            scr.pMov.transform.rotation = Quaternion.Euler(0, 0, 0);

            transform.localScale = new Vector3(
                Mathf.Abs(transform.localScale.x),
                transform.localScale.y,
                transform.localScale.z);
        }

        jumpForce = kOvH ? jumpForceDef * 0.85f : jumpForceDef;
    }
}
	

